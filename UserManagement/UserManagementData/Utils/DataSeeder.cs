using UserManagementDomain;
using Microsoft.EntityFrameworkCore;

namespace UserManagementData.Utils
{
    public class DataSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(GetUsers());
        }

        private static List<User> GetUsers()
        {
            return new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    Name = "Eusebiu",
                    Email = "floreaneusebiu@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("pass"),
                    Role = "Admin"
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Name = "Razvan",
                    Email = "razvanb@yahoo.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("pass"),
                    Role = "Client"  
                }
            };
        }
    }
}
