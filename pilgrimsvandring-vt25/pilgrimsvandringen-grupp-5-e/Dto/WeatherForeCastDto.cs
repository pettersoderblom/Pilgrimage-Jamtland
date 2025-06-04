using System.Text.Json.Serialization;

namespace pilgrimsvandringen_grupp_5_e.Dto
{
    public class WeatherForeCastDto
    {
        [JsonPropertyName("elevation")]
        public float Elevation { get; set; }
        [JsonPropertyName("hourly")]
        public Hourly Hourly { get; set; }
        [JsonPropertyName("hourly_units")]
        public HourlyUnits HourlyUnits { get; set; }

        [JsonPropertyName("current_weather")]
        public CurrentWeatherDto CurrentWeather { get; set; }
    }
}
