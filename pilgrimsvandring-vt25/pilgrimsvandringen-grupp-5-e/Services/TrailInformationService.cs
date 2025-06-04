using pilgrimsvandringen_grupp_5_e.Dto;
using pilgrimsvandringen_grupp_5_e.Repositories.Interfaces;
using pilgrimsvandringen_grupp_5_e.Services.Interfaces;
using pilgrimsvandringen_grupp_5_e.ViewModels;

namespace pilgrimsvandringen_grupp_5_e.Services
{
    public class TrailInformationService : ITrailInformationService
    {

        private readonly ITrailRepository _trailRepository;

        public TrailInformationService(ITrailRepository trailRepository) 
        
        {  _trailRepository = trailRepository; }

        public async Task<IEnumerable<TrailViewModel>> GetAllTrails()
        {
            var trails = await _trailRepository.GetAllTrailsAsync();


            List<TrailViewModel> trailsVMs = new List<TrailViewModel>();
            foreach (var trail in trails)
            {
                var trailVM = new TrailViewModel
                {
                   Id = trail.Id,
                   Name = trail.Name,
                   Description = trail.Description,
                   StartPoint = trail.StartPoint,
                   EndPoint = trail.EndPoint,
                   Terrain = trail.Terrain,
                   Difficulty = trail.Difficulty,
                   Color = trail.Color,
                   Length = trail.Length,
                   Height = trail.Height,
                   Surface = trail.Surface,
                   StartPointCoords = trail.StartPointCoords,
                   EndPointCoords = trail.EndPointCoords
                };
                trailsVMs.Add(trailVM);

            }
            return trailsVMs;
        }

        public async Task<TrailViewModel> GetTrailById(int id)
        {
            var trail = await _trailRepository.GetTrailByIdAsync(id);
            var trailVM = new TrailViewModel
            {
                Id = trail.Id,
                Name = trail.Name,
                Description = trail.Description,
                StartPoint = trail.StartPoint,
                EndPoint = trail.EndPoint,
                Terrain = trail.Terrain,
                Difficulty = trail.Difficulty,
                Color = trail.Color,
                Length = trail.Length,
                Height = trail.Height,
                Surface = trail.Surface,
                StartPointCoords = trail.StartPointCoords,
                EndPointCoords = trail.EndPointCoords
            };

            return trailVM;
        }
    }
}
