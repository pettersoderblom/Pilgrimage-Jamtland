using Microsoft.AspNetCore.Mvc;
using pilgrimsvandringen_grupp_5_e.Services;
using pilgrimsvandringen_grupp_5_e.Services.Interfaces;
using pilgrimsvandringen_grupp_5_e.ViewModels;
using System.Diagnostics;

namespace pilgrimsvandringen_grupp_5_e.Controllers
{
    public class WeatherController : Controller
    {
        private readonly ITrailInformationService _trailInformationService;
        private readonly IWeatherService _weatherService;

        public WeatherController(ITrailInformationService trailInformationService, IWeatherService weatherService)
        {
            _trailInformationService = trailInformationService;
            _weatherService = weatherService;
        }
        // TODO develop a structured weatherdetails view
        public async Task<IActionResult> WeatherForecastDetails(int id)
        {
            var trailVM = await _trailInformationService.GetTrailById(id);
            var startCoords = trailVM.StartPointCoords.Replace(" ", string.Empty).Split(',');
            var endCoords = trailVM.EndPointCoords.Replace(" ", string.Empty).Split(',');

            if (startCoords.Length > 1 && startCoords.Length < 3 || endCoords.Length > 1 && endCoords.Length < 3)
            {
                var startTrailWeather = await _weatherService.GetWeatherByCoordinatesAsync(startCoords[0], startCoords[1]);
                var endTrailWeather = await _weatherService.GetWeatherByCoordinatesAsync(endCoords[0], endCoords[1]);
                WeatherForeCastDetailsViewModel WFCVM = new WeatherForeCastDetailsViewModel(trailVM, startTrailWeather, endTrailWeather);

                return View(WFCVM);
            }

            return NotFound();
        }

        public async Task<IActionResult> WeatherForecastSummary()
        {
            var trailsVM = await _trailInformationService.GetAllTrails();

            if (trailsVM != null)
            {
                List<WeatherForeCastDetailsViewModel> ViewModels = new List<WeatherForeCastDetailsViewModel>();
                foreach (TrailViewModel trail in trailsVM)
                {
                    var startCoords = trail.StartPointCoords.Replace(" ", string.Empty).Split(',');
                    var endCoords = trail.EndPointCoords.Replace(" ", string.Empty).Split(',');

                    if (startCoords.Length > 1 && startCoords.Length < 3 || endCoords.Length > 1 && endCoords.Length < 3)
                    {
                        var startTrailWeather = await _weatherService.GetWeatherByCoordinatesAsync(startCoords[0], startCoords[1]);
                        var endTrailWeather = await _weatherService.GetWeatherByCoordinatesAsync(endCoords[0], endCoords[1]);
                        var startTrailFireRisk = await _weatherService.GetFireRiskForecastAsync(startCoords[0], startCoords[1]);

                        WeatherForeCastDetailsViewModel WFCVM = new WeatherForeCastDetailsViewModel(trail, startTrailWeather, endTrailWeather, startTrailFireRisk);
                        ViewModels.Add(WFCVM);
                    }
                }
                return View(ViewModels);

            }
            return NotFound();
        }
        
        public async Task<IActionResult> WeatherForeCastCurrentPosition(string latitude, string longitude)
        {
            var currentPosition = await _weatherService.GetWeatherByCoordinatesAsync(latitude, longitude);

            return View("~/Views/Weather/WeatherForecastDetails.cshtml", currentPosition);
        }

        public async Task<IActionResult> GetWeatherByCords(string latitude, string longitude)
        {
            var weatherData = await _weatherService.GetCurrentWeatherAsync(latitude, longitude);

            return Json(weatherData);
        }
    }
}
