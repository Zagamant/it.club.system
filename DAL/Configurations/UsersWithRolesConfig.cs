using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace System.DAL.Configurations
{
    public class UsersWithRolesConfig : IEntityTypeConfiguration<IdentityUserRole<int>>
    {
        private const int adminUserId = 777;
        private const int main_adminRoleId = 100;
        private const int adminRoleId = 101;

        public void Configure(EntityTypeBuilder<IdentityUserRole<int>> builder)
        {
            builder.HasData(
                new IdentityUserRole<int>
                {
                    RoleId = adminRoleId,
                    UserId = adminUserId,
                },
                new IdentityUserRole<int>
                {
                    RoleId = main_adminRoleId,
                    UserId = adminUserId,
                }
            );
        }
    }
}