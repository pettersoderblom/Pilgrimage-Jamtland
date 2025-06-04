using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace pilgrimsvandringen_grupp_5_e.Data
{
    public class CustomIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public CustomIdentityDbContext(DbContextOptions<CustomIdentityDbContext> options)
        : base(options)
        {
        }
    }
}
