using System.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace System.DAL.Helpers
{
    public static class ApplicationDbInitializer
    {
        public static void SeedUsers(UserManager<User> userManager)
        {
            if (userManager.FindByEmailAsync("danik53@ya.ru").Result==null)
            {
                var user = new User
                {
                    UserName = "zagamant",
                    Email = "danik53@ya.ru"
                };

                IdentityResult result = userManager.CreateAsync(user, "123890dD").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "admin").Wait();
                }
            }       
        }   
    }
}