using pilgrimsvandringen_grupp_5_e.Dto;

namespace pilgrimsvandringen_grupp_5_e.ViewModels
{
    public class WeatherForeCastDetailsViewModel
    {
        public TrailViewModel TrailVM { get; set; }
        public FireRiskViewModel? FireRiskVM { get; set; }
        public WeatherForeCastDto WeatherStart { get; set; }
        public WeatherForeCastDto WeatherEnd { get; set; }

        public WeatherForeCastDetailsViewModel(TrailViewModel trailsViewModel, WeatherForeCastDto weatherForeCastDtoStart, WeatherForeCastDto weatherForeCastDtoEnd)
        {
            TrailVM = trailsViewModel;            
            WeatherStart = weatherForeCastDtoStart;
            WeatherEnd = weatherForeCastDtoEnd;
        }

        public WeatherForeCastDetailsViewModel(TrailViewModel trailsViewModel, WeatherForeCastDto weatherForeCastDtoStart, WeatherForeCastDto weatherForeCastDtoEnd, FireRiskViewModel fireRiskViewModel)
        {
            TrailVM = trailsViewModel;
            FireRiskVM = fireRiskViewModel;
            WeatherStart = weatherForeCastDtoStart;
            WeatherEnd = weatherForeCastDtoEnd;
        }
    }
}
