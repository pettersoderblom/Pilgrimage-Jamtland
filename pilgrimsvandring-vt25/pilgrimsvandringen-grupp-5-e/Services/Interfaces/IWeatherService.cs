using pilgrimsvandringen_grupp_5_e.Dto;
using pilgrimsvandringen_grupp_5_e.ViewModels;

namespace pilgrimsvandringen_grupp_5_e.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherForeCastDto> GetCurrentWeatherAsync(string latitude, string longitude);
        Task<FireRiskViewModel> GetFireRiskForecastAsync(string latitude, string longitude);
        Task<WeatherForeCastDto> GetWeatherByCoordinatesAsync(string latitude, string longitude);
    }
}
