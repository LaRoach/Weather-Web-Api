using Microsoft.EntityFrameworkCore;
using WeatherWebApi.Models.Users;

namespace WeatherWebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<User>();

            entity.ToTable("User");

            entity.HasKey(e => e.Id);

            entity.Property(p => p.FirstName).HasColumnName("FirstName").HasMaxLength(200).IsRequired();

            entity.Property(p => p.LastName).HasColumnName("LastName").HasMaxLength(200).IsRequired();

            entity.Property(p => p.Email).HasColumnName("Email").HasMaxLength(100).IsRequired();

            entity.Property(p => p.ImageLocation).HasColumnName("ImageLocation").HasMaxLength(200);
        }
    }
}