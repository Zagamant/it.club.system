using System.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace System.DAL.Configurations
{
    public class CostsConfiguration : IEntityTypeConfiguration<Costs>
    {
        public void Configure(EntityTypeBuilder<Costs> builder)
        {
            builder
                .HasOne<Club>()
                .WithMany()
                .HasForeignKey(c => c.ClubId);
        }
    }
}