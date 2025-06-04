using pilgrimsvandringen_grupp_5_e.Models;

namespace pilgrimsvandringen_grupp_5_e.Repositories.Interfaces
{
    public interface IStageRepository
    {
        Task<Stage> GetStageByIdAsync(int id);
        Task<IEnumerable<Stage>> GetAllStagesByTrailIdAsync(int trailId);
        Task<IEnumerable<Stage>> GetAllStagesAsync();
    }
}
