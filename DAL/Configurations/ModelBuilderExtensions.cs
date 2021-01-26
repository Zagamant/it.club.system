using System.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace System.DAL.Configurations
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasData(
                    new User
                    {
                        AdditionalInfo = "Bruh test 1",
                        Address = new Address()
                        {
                            AddressLine = "Naberezhnaya",
                            City = "Moscow",
                            Country = "Russia"
                        },
                        BirthDay = new DateTime(1999, 5, 14),
                        Email = "danik53@ya.ru",
                        MiddleName = "ADS",
                        Name = "Danya",
                        Surname = "ASD",
                        UserName = "Zagamant"
                    },
                    new User
                    {
                        AdditionalInfo = "Bruh test 2",
                        Address = new Address()
                        {
                            AddressLine = "Naberezhnaya",
                            City = "Moscow",
                            Country = "Russia"
                        },
                        BirthDay = new DateTime(1999, 5, 14),
                        Email = "danik53@ya.ru",
                        MiddleName = "ADS test 2",
                        Name = "Danya test 2",
                        Surname = "ASD test 2",
                        UserName = "Zagamant2"
                    },
                    new User
                    {
                        AdditionalInfo = "Bruh test 3",
                        Address = new Address()
                        {
                            AddressLine = "Naberezhnaya test 3",
                            City = "Moscow 2",
                            Country = "Russia 2"
                        },
                        BirthDay = new DateTime(1999, 5, 14),
                        Email = "danik531@ya.ru",
                        MiddleName = "ADS test 3",
                        Name = "Danya test 3",
                        Surname = "ASD test 3",
                        UserName = "Zagamant3"
                    }
                );
        }
    }
}