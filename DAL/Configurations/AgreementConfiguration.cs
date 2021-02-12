using System.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace System.DAL.Configurations
{
    internal class AgreementConfiguration : IEntityTypeConfiguration<Agreement>
    {
        public void Configure(EntityTypeBuilder<Agreement> builder)
        {
            builder
                .HasOne(a => a.Course)
                .WithMany()
                .HasForeignKey(a => a.CourseId);

            builder
                .HasOne(a => a.User)
                .WithOne();
        }
    }
}