using pilgrimsvandringen_grupp_5_e.Repositories.Interfaces;
using pilgrimsvandringen_grupp_5_e.Services.Interfaces;
using pilgrimsvandringen_grupp_5_e.ViewModels;

namespace pilgrimsvandringen_grupp_5_e.Services
{
    public class StageInformationService : IStageInformationService
    {

        private readonly IStageRepository _stageRepository;

        public StageInformationService(IStageRepository stageRepository)
        { 
            _stageRepository = stageRepository; 
        }

        public async Task<IEnumerable<StageViewModel>> GetAllStagesByTrailId(int trailId)
        {
            var stages = await _stageRepository.GetAllStagesByTrailIdAsync(trailId);


            List<StageViewModel> stageVMs = new List<StageViewModel>();
            foreach (var stage in stages)
            {
                var stageVM = new StageViewModel
                {
                    Id = stage.Id,
                    Name = stage.Name,
                    Description = stage.Description,
                    StartPoint = stage.StartPoint,
                    EndPoint = stage.EndPoint,
                    Terrain = stage.Terrain,
                    Difficulty = stage.Difficulty,
                    Color = stage.Color,
                    Length = stage.Length,
                    Height = stage.Height,
                    Surface = stage.Surface,
                    TrailId = stage.TrailId
                };
                stageVMs.Add(stageVM);

            }
            return stageVMs;
        }

        public async Task<IEnumerable<StageViewModel>> GetAllStages()
        {
            var stages = await _stageRepository.GetAllStagesAsync();


            List<StageViewModel> stageVMs = new List<StageViewModel>();
            foreach (var stage in stages)
            {
                var stageVM = new StageViewModel
                {
                    Id = stage.Id,
                    Name = stage.Name,
                    Description = stage.Description,
                    StartPoint = stage.StartPoint,
                    EndPoint = stage.EndPoint,
                    Terrain = stage.Terrain,
                    Difficulty = stage.Difficulty,
                    Color = stage.Color,
                    Length = stage.Length,
                    Height = stage.Height,
                    Surface = stage.Surface,
                    TrailId = stage.TrailId
                };
                stageVMs.Add(stageVM);

            }
            return stageVMs;
        }

        public async Task<StageViewModel> GetStageById(int id)
        {
            var stage = await _stageRepository.GetStageByIdAsync(id);
            var stageVM = new StageViewModel
            {
                Id = stage.Id,
                Name = stage.Name,
                Description = stage.Description,
                StartPoint = stage.StartPoint,
                EndPoint = stage.EndPoint,
                Terrain = stage.Terrain,
                Difficulty = stage.Difficulty,
                Color = stage.Color,
                Length = stage.Length,
                Height = stage.Height,
                Surface = stage.Surface,
            };

            return stageVM;
        }
    }
}
