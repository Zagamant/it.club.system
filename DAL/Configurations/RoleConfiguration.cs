using System.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace System.DAL.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        private const int main_adminId = 100;
        private const int adminId = 101;
        private const int userId = 2;

        public void Configure(EntityTypeBuilder<Role> builder)
        {

            builder.HasData(
                new Role
                {
                    Id = main_adminId,
                    Name = "main_admin",
                    NormalizedName = "MAIN_ADMIN"
                },
                new Role
                {
                    Id = adminId,
                    Name = "admin",
                    NormalizedName = "ADMIN"
                },
                new Role
                {
                    Id = userId,
                    Name = "user",
                    NormalizedName = "USER"
                }
            );
        }
    }
}