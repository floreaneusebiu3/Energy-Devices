using Microsoft.EntityFrameworkCore;
using DevicesManagementDomain;
using UserManagementData.Utils;
using Microsoft.Extensions.Configuration;


namespace DevicesManagementData
{
    public class DeviceManagementContext : DbContext
    {
        private readonly IConfiguration _config;

        public DeviceManagementContext(IConfiguration config)
        {
            _config = config;
        }

        public DbSet<Device> Devices { get; set; }
        public DbSet<UserDevice> UserDevices { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _config["Url:connectionString"];
            optionsBuilder.UseSqlServer(
                connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            DataSeeder.Seed(modelBuilder);
        }
    }
}