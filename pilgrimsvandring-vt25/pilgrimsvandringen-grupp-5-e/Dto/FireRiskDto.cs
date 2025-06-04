using System.Text.Json.Serialization;

namespace pilgrimsvandringen_grupp_5_e.Dto
{
    public class FireRiskDto
    {
        [JsonPropertyName("forecast")]
        public FireRiskForecastDto FireRiskForecastDto { get; set; }

    }
}
