using pilgrimsvandringen_grupp_5_e.Dto;
using pilgrimsvandringen_grupp_5_e.Repositories.Interfaces;
using pilgrimsvandringen_grupp_5_e.Services.Interfaces;
using pilgrimsvandringen_grupp_5_e.ViewModels;

namespace pilgrimsvandringen_grupp_5_e.Services
{
    public class PointOfInterestService : IPointOfinterestService
    {
        private readonly IPointOfInterestRepository _pointOfInterestRepository;

        public PointOfInterestService(IPointOfInterestRepository pointOfInterestRepository)
        {
            _pointOfInterestRepository = pointOfInterestRepository;
        }
        public async Task<PointOfInterestViewModel> GetPointOfInterest(int id)
        {
            var point = await _pointOfInterestRepository.GetPointOfInterestAsync(id);
            var pointVM = new PointOfInterestViewModel
            {
                Id = point.Id,
                Name = point.Name,
                Description = point.Description,
                Longitude = point.Longitude,
                Latitude = point.Latitude,
                Category = point.Category,
                TrailId = point.TrailId,
            };
            
            return pointVM;
        }

        public async Task<IEnumerable<PointOfInterestViewModel>> GetAllPointsOfInterests()
        {
            var points = await _pointOfInterestRepository.GetAllPointsOfInterestsAsync();
            List<PointOfInterestViewModel> pointOfInterestVMs = new List<PointOfInterestViewModel>();
            foreach (var point in points)
            {
                var pointVM = new PointOfInterestViewModel
                {
                    Id = point.Id,
                    Name = point.Name,
                    Description = point.Description,
                    Longitude = point.Longitude,
                    Latitude = point.Latitude,
                    Category = point.Category,
                    TrailId = point.TrailId,
                };
                pointOfInterestVMs.Add(pointVM);
                
            }
            return pointOfInterestVMs;
        }

        public async Task<IEnumerable<PointOfInterestViewModel>> GetAllPointsOfInterestsByTrailId(int trailId)
        {
            var points = await _pointOfInterestRepository.GetAllPointsOfInterestsByTrailIdAsync(trailId);
            List<PointOfInterestViewModel> pointOfInterestVMs = new List<PointOfInterestViewModel>();
            foreach (var point in points)
            {
                var pointVM = new PointOfInterestViewModel
                {
                    Id = point.Id,
                    Name = point.Name,
                    Description = point.Description,
                    Longitude = point.Longitude,
                    Latitude = point.Latitude,
                    Category = point.Category,
                    TrailId = point.TrailId,
                };
                pointOfInterestVMs.Add(pointVM);

            }
            return pointOfInterestVMs;

        }
    }
}
