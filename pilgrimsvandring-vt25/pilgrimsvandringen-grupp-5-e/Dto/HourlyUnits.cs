using System.Text.Json.Serialization;

namespace pilgrimsvandringen_grupp_5_e.Dto
{
    public class HourlyUnits
    {
        [JsonPropertyName("time")]
        public string Time { get; set; }
        [JsonPropertyName("temperature_2m")]
        public string Temperature { get; set; }
        [JsonPropertyName("precipitation_probability")]
        public string PrecipitationProbability { get; set; }
        [JsonPropertyName("precipitation")]
        public string Precipitation { get; set; }
        [JsonPropertyName("rain")]
        public string Rain { get; set; }
        [JsonPropertyName("showers")]
        public string Showers { get; set; }
        [JsonPropertyName("snowfall")]
        public string Snowfall { get; set; }
        [JsonPropertyName("snow_depth")]
        public string SnowDepth { get; set; }
        [JsonPropertyName("visibility")]
        public string Visibility { get; set; }
        [JsonPropertyName("wind_speed_10m")]
        public string WindSpeed { get; set; }
        [JsonPropertyName("wind_direction_10m")]
        public string WindDirection { get; set; }
        [JsonPropertyName("wind_gusts_10m")]
        public string WindGusts { get; set; }
    }
}
