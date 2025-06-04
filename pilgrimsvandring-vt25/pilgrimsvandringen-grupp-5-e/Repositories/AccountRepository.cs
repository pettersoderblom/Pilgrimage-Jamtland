using Microsoft.EntityFrameworkCore;
using pilgrimsvandringen_grupp_5_e.Data;
using pilgrimsvandringen_grupp_5_e.Models;
using pilgrimsvandringen_grupp_5_e.Repositories.Interfaces;

namespace pilgrimsvandringen_grupp_5_e.Repositories
{
    public class AccountRepository : Repository<User>, IAccountRepository
    {
        private readonly AppDbContext _context;
        public AccountRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateUserInDbAsync(User user)
        {
            var result = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// creates roles if they do not exists in database
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task SeedRolesAsync(Role role)
        {
            var result = await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// checks if role exists in database
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<bool> CheckRoleExistensAsync(string role)
        {
            var result = await _context.Roles.FirstOrDefaultAsync(x => x.RoleName == role);
            if (result != null)
            {
                return true;
            }
            return false;
        }
    }
}
