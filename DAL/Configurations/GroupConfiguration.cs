using System.DAL.Entities;
using System.DAL.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace System.DAL.Configurations
{
	internal class GroupConfiguration : IEntityTypeConfiguration<Group>
	{
		public void Configure(EntityTypeBuilder<Group> builder)
		{
			builder
				.HasOne(group => group.Room)
				.WithMany(room => room.Groups);

			builder
				.HasOne(group => group.Course)
				.WithMany(course => course.Groups);

			builder
				.Property(e => e.Status)
				.HasConversion(
					s => s.ToString(),
					str => (GroupStatus)Enum.Parse(typeof(GroupStatus), str));
		}
	}
}
