using System.DAL.Entities;
using System.DAL.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace System.DAL.Configurations
{
	internal class ClubConfiguration : IEntityTypeConfiguration<Club>
	{
		public void Configure(EntityTypeBuilder<Club> builder)
		{
			builder
				.HasOne<Address>(club => club.Address)
				.WithMany();

			builder
				.HasMany<Room>(club => club.Rooms)
				.WithOne(r => r.Club)
				.OnDelete(DeleteBehavior.Cascade);

			builder
				.Property(c => c.Status)
				.HasConversion(
					s => s.ToString(),
					str => (ClubStatus)Enum.Parse(typeof(ClubStatus), str, true));

		}
	}
}
