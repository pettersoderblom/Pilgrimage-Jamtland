using System.Text.Json.Serialization;

namespace pilgrimsvandringen_grupp_5_e.Dto
{
    public class Hourly
    {
        [JsonPropertyName("time")]
        public IEnumerable<string> Time { get; set; }
        [JsonPropertyName("temperature_2m")]
        public IEnumerable<double> Temperature { get; set; }
        [JsonPropertyName("weather_code")]
        public IEnumerable<int> WeatherCode { get; set; }
        [JsonPropertyName("precipitation")]
        public IEnumerable<double> Precipitation { get; set; }
        [JsonPropertyName("rain")]
        public IEnumerable<double> Rain { get; set; }
        [JsonPropertyName("showers")]
        public IEnumerable<double> Showers { get; set; }
        [JsonPropertyName("snowfall")]
        public IEnumerable<double> Snowfall { get; set; }
        [JsonPropertyName("snow_depth")]
        public IEnumerable<double> SnowDepth { get; set; }
        [JsonPropertyName("visibility")]
        public IEnumerable<double> Visibility { get; set; }
        [JsonPropertyName("wind_speed_10m")]
        public IEnumerable<double> WindSpeed { get; set; }
        [JsonPropertyName("wind_direction_10m")]
        public IEnumerable<double> WindDirection { get; set; }
        [JsonPropertyName("wind_gusts_10m")]
        public IEnumerable<double> WindGusts { get; set; }
    }
}
