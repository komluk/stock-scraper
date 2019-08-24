using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StockScraper
{
    public partial class Company
    {
        [JsonProperty("navigator")]
        public double[][] Navigator { get; set; }

        [JsonProperty("profileData")]
        public ProfileData ProfileData { get; set; }

        [JsonProperty("volume")]
        public long[][] Volume { get; set; }

        [JsonProperty("interval")]
        public long Interval { get; set; }

        [JsonProperty("main")]
        public double[][] Main { get; set; }
    }

    public partial class ProfileData
    {
        [JsonProperty("prevCloseDate")]
        public string PrevCloseDate { get; set; }

        [JsonProperty("volumeAverageValue")]
        public string VolumeAverageValue { get; set; }

        [JsonProperty("max")]
        public long Max { get; set; }

        [JsonProperty("valueAverage")]
        public string ValueAverage { get; set; }

        [JsonProperty("prevClose")]
        public string PrevClose { get; set; }

        [JsonProperty("changeValue")]
        public string ChangeValue { get; set; }

        [JsonProperty("maxValueDate")]
        public string MaxValueDate { get; set; }

        [JsonProperty("minValueDate")]
        public string MinValueDate { get; set; }

        [JsonProperty("min")]
        public double Min { get; set; }

        [JsonProperty("maxValue")]
        public string MaxValue { get; set; }

        [JsonProperty("volumeSumValue")]
        public string VolumeSumValue { get; set; }

        [JsonProperty("interval")]
        public long Interval { get; set; }

        [JsonProperty("turnoverSumValue")]
        public string TurnoverSumValue { get; set; }

        [JsonProperty("valueSum")]
        public long ValueSum { get; set; }

        [JsonProperty("turnoverAverageValue")]
        public string TurnoverAverageValue { get; set; }

        [JsonProperty("minValue")]
        public string MinValue { get; set; }

        [JsonProperty("endDate")]
        public DateTimeOffset EndDate { get; set; }

        [JsonProperty("changePercentValue")]
        public string ChangePercentValue { get; set; }

        [JsonProperty("startDate")]
        public DateTimeOffset StartDate { get; set; }
    }

    public partial class Company
    {
        public static Company FromJson(string json) =>
            JsonConvert.DeserializeObject<Company>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Company self) =>
            JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}