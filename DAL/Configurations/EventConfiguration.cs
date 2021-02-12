using System.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace System.DAL.Configurations
{
    public class EventConfiguration: IEntityTypeConfiguration<Event>

    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder
                .HasOne<Club>()
                .WithMany()
                .HasForeignKey(e => e.ClubId);
        }
    }
}