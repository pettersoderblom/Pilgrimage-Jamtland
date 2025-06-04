using pilgrimsvandringen_grupp_5_e.Models;

namespace pilgrimsvandringen_grupp_5_e.Services.Interfaces
{
    public interface IAccountService
    {
        Task SeedRolesAsync(Role role);
        Task CreateUserAsync(User user);
        Task<bool> CheckRoleExistensAsync(string role);
    }
}
