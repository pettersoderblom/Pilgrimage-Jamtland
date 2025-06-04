using pilgrimsvandringen_grupp_5_e.Dto;
using pilgrimsvandringen_grupp_5_e.ViewModels;

namespace pilgrimsvandringen_grupp_5_e.Services.Interfaces
{
    public interface IPointOfinterestService
    {
        Task<PointOfInterestViewModel> GetPointOfInterest(int id);
        Task<IEnumerable<PointOfInterestViewModel>> GetAllPointsOfInterests();
        Task<IEnumerable<PointOfInterestViewModel>>GetAllPointsOfInterestsByTrailId(int id);
    }
}
