using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Models
{
    public class ExamenDbContext : DbContext
    {
        public ExamenDbContext(DbContextOptions<ExamenDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(entity => {
                entity.HasIndex(u => u.Username).IsUnique();
            });

            builder.Entity<Package>(entity => {
                entity.HasIndex(u => u.TrackingCode).IsUnique();
            });

        }

        public DbSet<Package> Packages { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
