using UserManagementDomain;
using Microsoft.EntityFrameworkCore;
using UserManagementData.Utils;
using Microsoft.Extensions.Configuration;

namespace UserManagementData
{
    public class UserManagementContext : DbContext
    {
        private readonly IConfiguration _config;

        public UserManagementContext(IConfiguration config)
        {
            _config = config;
        }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _config["Url:ConnectionString"];
            optionsBuilder.UseSqlServer(
                connectionString
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            addUniqueConstraintToUserEmail(modelBuilder);
            //DataSeeder.Seed(modelBuilder);
        }

        private void addUniqueConstraintToUserEmail(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
        }
    }
}