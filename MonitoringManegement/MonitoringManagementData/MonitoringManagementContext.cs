using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MonitoringManagementDomain;

namespace MonitoringManagementData
{
    public class MonitoringManagementContext : DbContext
    {
        private readonly IConfiguration _config;

        public MonitoringManagementContext(IConfiguration config)
        {
            _config = config;
        }

        public DbSet<UserDevice> UserDevices { get; set; }
        public DbSet<Measurement> Measurements { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _config["Url:ConnectionString"];
            optionsBuilder.UseSqlServer(
                connectionString);
        }
    }
}