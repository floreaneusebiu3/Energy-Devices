using DevicesManagementDomain;
using Microsoft.EntityFrameworkCore;

namespace UserManagementData.Utils
{
    public class DataSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(GetUsers());
            modelBuilder.Entity<Device>().HasData(GetDevices());
        }

        private static List<User> GetUsers()
        {
            return new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    Name = "Eusebiu",
                    Email = "floreaneusebiu@gmail.com"
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Name = "Razvan",
                    Email = "razvanb@yahoo.com"
                }
            };
        }

        private static List<Device> GetDevices()
        {
            return new List<Device>
            {
                new Device
                {
                    Id = Guid.NewGuid(),
                    Name = "Air Conditioner",
                    Description = "eco-friendly",
                    MaximuHourlyEnergyConsumption = 10
                },
                new Device
                {
                    Id = Guid.NewGuid(),
                    Name = "Dishwasher",
                    Description = "An energy-efficient dishwasher",
                    MaximuHourlyEnergyConsumption = 6
                },
                new Device
                {
                    Id = Guid.NewGuid(),
                    Name = " Smart sprinkler",
                    Description = "just an inch of water a week",
                    MaximuHourlyEnergyConsumption = 1
                },
                new Device
                {
                    Id = Guid.NewGuid(),
                    Name = "Rain barrels",
                    Description = "store rainwater.",
                    MaximuHourlyEnergyConsumption = 3
                }
            };
        }
    }
}
