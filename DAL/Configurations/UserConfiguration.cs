using System.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace System.DAL.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		private const int adminId = 777;
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder
				.HasMany<Contact>(user => user.Contacts)
				.WithOne();

			var admin = new User
			{
				Id = adminId,
				UserName = "zagamant",
				NormalizedUserName = "ZAGAMANT",
				Name = "Daniel",
				Surname = "Istomin",
				Email = "danik5311@gmail.com",
				NormalizedEmail = "DANIK5311@GMAIL.COM",
				PhoneNumber = "375291376955",
				EmailConfirmed = true,
				PhoneNumberConfirmed = true,
				BirthDay = new DateTime(1980,1,1),
				SecurityStamp = new Guid().ToString("D")
			};

			admin.PasswordHash = PassGenerate(admin);

			builder.HasData(admin);
		}

		public string PassGenerate(User user)
		{
			var passHash = new PasswordHasher<User>();
			return passHash.HashPassword(user, "123890dD");
		}
	}
}
