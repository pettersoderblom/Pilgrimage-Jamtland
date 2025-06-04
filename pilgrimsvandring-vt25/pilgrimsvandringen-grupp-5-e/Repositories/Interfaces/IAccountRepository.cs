using pilgrimsvandringen_grupp_5_e.Models;

namespace pilgrimsvandringen_grupp_5_e.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<bool> CheckRoleExistensAsync(string role);
        Task SeedRolesAsync(Role role);
        Task CreateUserInDbAsync(User user);
    }
}
