using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Caching.Memory;
using pilgrimsvandringen_grupp_5_e.Dto;
using pilgrimsvandringen_grupp_5_e.Services.Interfaces;
using pilgrimsvandringen_grupp_5_e.ViewModels;
using System.Globalization;

namespace pilgrimsvandringen_grupp_5_e.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly ApiEngine _apiEngine;
        private readonly IMemoryCache _memoryCache;
        private readonly TimeSpan CacheDuration = TimeSpan.FromHours(1); // Cache for 1 Hour
        public WeatherService(ApiEngine apiEngine)
        {
            _apiEngine = apiEngine;
        }

        public WeatherService(ApiEngine apiEngine, IMemoryCache memoryCache)
        {
            _apiEngine = apiEngine;
            _memoryCache = memoryCache;
        }

        public async Task<WeatherForeCastDto> GetWeatherByCoordinatesAsync(string latitude, string longitude)
        {  
            
            var currentPositionWeather = await _apiEngine.GetAsync<WeatherForeCastDto>($"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&hourly=temperature_2m,weather_code,precipitation,rain,showers,snowfall,snow_depth,visibility,wind_speed_10m,wind_direction_10m,wind_gusts_10m");
            
            return currentPositionWeather;
        }

        public async Task<WeatherForeCastDto> GetCurrentWeatherAsync(string latitude, string longitude)
        {
            string cacheKey = $"CurrentWeather_{latitude}_{longitude}";

            if (!_memoryCache.TryGetValue(cacheKey, out WeatherForeCastDto cachedWeather))
            {
                cachedWeather = await _apiEngine.GetAsync<WeatherForeCastDto>(
                    $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current_weather=true"
                );

                if (cachedWeather != null)
                {
                    _memoryCache.Set(cacheKey, cachedWeather, CacheDuration);
                }
            }

            return cachedWeather;
        }

        public async Task<FireRiskViewModel> GetFireRiskForecastAsync(string latitude, string longitude)
        {
            try
            {
                var currentPositionFireRiskDto = await _apiEngine.GetAsync<FireRiskDto>($"https://api.msb.se/brandrisk/v2/CurrentRisk/sv/{latitude}/{longitude}");

                if (currentPositionFireRiskDto != null)
                {
                    FireRiskViewModel fireRiskVM = new FireRiskViewModel
                    {
                        Date = currentPositionFireRiskDto.FireRiskForecastDto.Date,
                        GrassMessage = currentPositionFireRiskDto.FireRiskForecastDto.GrassMessage,
                        RiskMessage = currentPositionFireRiskDto.FireRiskForecastDto.RiskMessage
                    };
                    return fireRiskVM;
                }

                return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
