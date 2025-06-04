using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using pilgrimsvandringen_grupp_5_e.Models;
using Microsoft.AspNetCore.Identity;

namespace pilgrimsvandringen_grupp_5_e.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Trail> Trails { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<PointOfInterest> PointsOfInterests { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<MessageType> MessageTypes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// <summary>
        /// conects identityUser with user
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasOne(u => u.IdentityUser).WithMany().HasForeignKey(u =>
            u.IdentityUserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
