using System.Text.Json.Serialization;

namespace pilgrimsvandringen_grupp_5_e.ViewModels
{
    public class FireRiskViewModel
    {
        public DateTime Date { get; set; }        
        public string GrassMessage { get; set; }        
        public string RiskMessage { get; set; }
    }
}
