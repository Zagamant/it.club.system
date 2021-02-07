using Microsoft.EntityFrameworkCore;
using System.DAL.Entities;
using System.DAL.Entities.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace System.DAL.Configurations
{
	internal class RoomConfiguration : IEntityTypeConfiguration<Room>
	{
		public void Configure(EntityTypeBuilder<Room> builder)
		{
			builder
				.HasMany<Group>()
				.WithOne(group => group.Room)
				.HasForeignKey(r => r.RoomId);


			builder
				.Property(r => r.Status)
				.HasConversion(
					s => s.ToString(),
					str => (RoomStatus)Enum.Parse(typeof(RoomStatus), str));

		}
	}
}
