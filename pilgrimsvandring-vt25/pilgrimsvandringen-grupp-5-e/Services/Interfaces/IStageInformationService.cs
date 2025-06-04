using pilgrimsvandringen_grupp_5_e.ViewModels;

namespace pilgrimsvandringen_grupp_5_e.Services.Interfaces
{
    public interface IStageInformationService
    {
        Task<StageViewModel> GetStageById(int id);
        Task<IEnumerable<StageViewModel>> GetAllStagesByTrailId(int trailId);
        Task<IEnumerable<StageViewModel>> GetAllStages();
    }
}
