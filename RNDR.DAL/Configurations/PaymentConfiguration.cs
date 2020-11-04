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
				.HasIndex(p => p.User)
				.IsUnique();
		}
	}
}
