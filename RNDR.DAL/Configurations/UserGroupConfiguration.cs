using System.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace System.DAL.Configurations
{
	public class UserGroupConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder
				.HasMany(u => u.Groups)
				.WithMany(g => g.Users)
				.UsingEntity<UserGroup>(
					ug => ug
						.HasOne(userGroup => userGroup.Group)
						.WithMany()
						.HasForeignKey(userGroup => userGroup.GroupId),
					ug => ug
						.HasOne(userGroup => userGroup.User)
						.WithMany()
						.HasForeignKey(userGroup => userGroup.UserId)
				)
				.ToTable("UserGroup")
				.HasKey(ug => new {ug.UserId, ug.GroupId});
		}
	}
}
