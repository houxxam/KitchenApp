using Microsoft.EntityFrameworkCore;
using Repas.Models;
namespace Repas.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<DateForniture> DateFornitures { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<TypeRepas> TypeRepas { get; set; }
        public DbSet<RepasService> RepasServices { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DateForniture>()
                .HasIndex(df => df.FornitureDate)
                .IsUnique();
        }
    }
}
