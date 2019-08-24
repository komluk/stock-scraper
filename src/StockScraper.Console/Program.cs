using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StockScraper
{
    public class Program
    {
        private static string[] _companies = new string[]
        {
            "PLAYWAY",
            "Wirtualna",
            "Echo",
            "Cdprojekt",
            "Vrg",
            "Amrest",
            "Forte",
            "Dinopl",
            "Ciech",
            "Orbis",
            "Assecopol",
            "11bit",
            "Boryszew",
            "Cyfrplsat",
            "Kernel",
            "Famur",
            "Lccorp",
            "Budimex",
            "Amica",
            "Orangepl"
        };

        private static readonly HttpClient _client = new HttpClient();

        public static async Task Main(string[] args)
        {
            DateTime.TryParse("2018-01-03", out DateTime start);
            DateTime.TryParse("2018-03-29", out DateTime end);

            var dates = Enumerable.Range(0, 1 + end.Subtract(start).Days)
                  .Select(offset => start.AddDays(offset))
                  .ToArray();

            var workdays = dates
                    .Where(x => x.Date.DayOfWeek != DayOfWeek.Saturday &&
                                x.Date.DayOfWeek != DayOfWeek.Sunday)
                    .ToArray();

            foreach (var company in _companies)
            {
                Console.WriteLine("result for: {0}", company);
                try
                {
                    HttpResponseMessage response = await _client.GetAsync(
                        $"https://www.bankier.pl/new-charts/get-data?" +
                        $"date_from=1514934000000&" +
                        $"date_to=1522360799000&" +
                        $"symbol={company}&" +
                        $"intraday=false&" +
                        $"type=area");

                    response.EnsureSuccessStatusCode();
                    var data = Company.FromJson(await response.Content.ReadAsStringAsync());

                    using (var writer = new StreamWriter($"../files/{company}.csv"))
                    {
                        for (int i = 0; i < workdays.Length; i++)
                        {
                            var day = workdays[i].ToString("MM/dd/yyyy");
                            var value = data.Main[i][1];

                            Console.WriteLine($"time: {day}, value: {value}");
                            var line = string.Format("{0},{1}", day, value);
                            writer.WriteLine(line);
                            writer.Flush();
                        }
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("Exception Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }
            }
            Console.WriteLine("Success!");
            Console.ReadKey();
        }
    }
}
