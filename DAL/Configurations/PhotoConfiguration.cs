using System.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace System.DAL.Configurations
{
	public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
	{
		public void Configure(EntityTypeBuilder<Photo> builder)
		{
			builder
				.HasOne<User>()
				.WithMany(user => user.Photos)
				.HasForeignKey(photo => photo.UserId)
				;
		}
	}
}
