using System.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace System.DAL.Helpers
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 0,
                    Name = "admin",
                    NormalizedName =  "admin".ToUpper()
                    
                });

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    AccessFailedCount = 0,
                    ConcurrencyStamp = null,
                    Id = 777,
                    LockoutEnabled = false,
                    LockoutEnd = null,
                    NormalizedEmail = null,
                    NormalizedUserName = null,
                    PasswordHash = null,
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    SecurityStamp = null,
                    TwoFactorEnabled = false,
                    BirthDay = new DateTime(1999,
                        05,
                        14),
                    Email = "danik53@ya.ru",
                    EmailConfirmed = true,
                    UserName = "Zagamant",
                    Name = "Daneil",
                    Surname = "Istomin",
                }
            );
        }
    }
}