using pilgrimsvandringen_grupp_5_e.Data;
using pilgrimsvandringen_grupp_5_e.Models;
using pilgrimsvandringen_grupp_5_e.Repositories.Interfaces;

namespace pilgrimsvandringen_grupp_5_e.Repositories
{
    public class TrailRepository : Repository<Trail>, ITrailRepository 
    {
        private readonly AppDbContext _context;

        public TrailRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Trail> GetTrailByIdAsync(int id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<IEnumerable<Trail>> GetAllTrailsAsync()
        {
            return await GetAllAsync();
        }
    }
}
