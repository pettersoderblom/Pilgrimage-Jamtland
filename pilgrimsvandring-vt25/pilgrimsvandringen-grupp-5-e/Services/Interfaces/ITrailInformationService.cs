using pilgrimsvandringen_grupp_5_e.ViewModels;

namespace pilgrimsvandringen_grupp_5_e.Services.Interfaces
{
    public interface ITrailInformationService
    {
        Task<TrailViewModel> GetTrailById(int id);
        Task<IEnumerable<TrailViewModel>> GetAllTrails();

    }
}
