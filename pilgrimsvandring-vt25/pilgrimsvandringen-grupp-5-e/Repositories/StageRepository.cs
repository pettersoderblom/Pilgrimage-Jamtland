using Microsoft.EntityFrameworkCore;
using Npgsql;
using pilgrimsvandringen_grupp_5_e.Data;
using pilgrimsvandringen_grupp_5_e.Models;
using pilgrimsvandringen_grupp_5_e.Repositories.Interfaces;

namespace pilgrimsvandringen_grupp_5_e.Repositories
{
    public class StageRepository : Repository<Stage>, IStageRepository
    {
        private readonly AppDbContext _context;

        public StageRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Stage> GetStageByIdAsync(int id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<IEnumerable<Stage>> GetAllStagesByTrailIdAsync(int trailId)
        {            
            var stages = await _context.Stages.FromSql($"SELECT * FROM \"Stages\"").Where(s => s.TrailId.Equals(trailId)).ToListAsync();
            return stages;
        }

        public async Task<IEnumerable<Stage>> GetAllStagesAsync()
        {
            return await GetAllAsync();
        }
    }
}
