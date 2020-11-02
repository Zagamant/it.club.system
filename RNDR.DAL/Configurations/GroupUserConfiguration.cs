using System.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace System.DAL.Configurations
{
	public class GroupUserConfiguration : IEntityTypeConfiguration<UserGroup>
	{
		public void Configure(EntityTypeBuilder<UserGroup> builder)
		{
			builder
				.HasKey(ug => new { ug.UserId, ug.GroupId });

			builder
				.HasOne<User>(sc => sc.User)
				.WithMany(s => s.UserGroups)
				.HasForeignKey(sc => sc.UserId);


			builder
				.HasOne<Group>(sc => sc.Group)
				.WithMany(s => s.UserGroups)
				.HasForeignKey(sc => sc.GroupId);
		}
	}
}
