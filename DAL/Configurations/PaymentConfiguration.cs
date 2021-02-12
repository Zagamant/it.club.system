using System.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace System.DAL.Configurations
{
	public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
	{
		public void Configure(EntityTypeBuilder<Payment> builder)
		{
			builder
				.HasOne<User>(model => model.User)
				.WithMany()
				.HasForeignKey(p => p.ClubId);

			builder
				.HasOne<Club>(c => c.Club)
				.WithOne();
		}
	}
}
