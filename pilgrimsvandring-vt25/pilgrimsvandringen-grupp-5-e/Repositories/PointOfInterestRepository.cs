using Microsoft.EntityFrameworkCore;
using pilgrimsvandringen_grupp_5_e.Data;
using pilgrimsvandringen_grupp_5_e.Models;
using pilgrimsvandringen_grupp_5_e.Repositories.Interfaces;

namespace pilgrimsvandringen_grupp_5_e.Repositories
{
    public class PointOfInterestRepository : Repository<PointOfInterest>, IPointOfInterestRepository
    {
        private readonly AppDbContext _context;

        public PointOfInterestRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PointOfInterest> GetPointOfInterestAsync(int id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<IEnumerable<PointOfInterest>> GetAllPointsOfInterestsAsync()
        {
            return await GetAllAsync();
        }

        public async Task<IEnumerable<PointOfInterest>> GetAllPointsOfInterestsByTrailIdAsync(int trailId)
        {
            var pointsOfInterests = await _context.PointsOfInterests.FromSql($"SELECT * FROM \"PointsOfInterests\"").Where(p => p.TrailId.Equals(trailId)).ToListAsync();
            return pointsOfInterests;
        }
    }
}
