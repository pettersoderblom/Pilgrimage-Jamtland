using System.Text.Json.Serialization;

namespace pilgrimsvandringen_grupp_5_e.Dto
{
    public class CurrentWeatherDto
    {
        [JsonPropertyName("temperature")]
        public double Temperature { get; set; }

        [JsonPropertyName("time")]
        public string Time { get; set; }

        [JsonPropertyName("weathercode")]
        public int WeatherCode { get; set; }
    }
}
