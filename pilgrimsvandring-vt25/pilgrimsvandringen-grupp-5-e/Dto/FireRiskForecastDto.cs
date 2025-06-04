using System.Text.Json.Serialization;

namespace pilgrimsvandringen_grupp_5_e.Dto
{
    public class FireRiskForecastDto
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
        [JsonPropertyName("grassMessage")]
        public string GrassMessage { get; set; }
        [JsonPropertyName("riskMessage")]
        public string RiskMessage { get; set; }
    }
}
