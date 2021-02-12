using System.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace System.DAL.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder
				.HasMany<Contact>(user => user.Contacts)
				.WithOne();

			// builder
			// 	.HasMany<Payment>()
			// 	.WithOne(p => p.User)
			// 	.HasForeignKey(i => i.ClubId);

		}
	}
}
