using System.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace System.DAL.Configurations
{
	public class PhotoConfiguration : IEntityTypeConfiguration<Image>
	{
		public void Configure(EntityTypeBuilder<Image> builder)
		{
			builder
				.HasOne<User>()
				.WithMany(user => user.Images)
				.HasForeignKey(photo => photo.UserId)
				;
		}
	}
}
