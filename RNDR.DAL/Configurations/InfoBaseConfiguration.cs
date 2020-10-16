using System.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace System.DAL.Configurations
{
	public class InfoBaseConfiguration : IEntityTypeConfiguration<InfoBase>
	{
		public void Configure(EntityTypeBuilder<InfoBase> builder)
		{
			builder
				.HasKey(ib => ib.Id);

			builder
				.HasOne(e => e.Member)
				.WithOne(e => e.AboutUserInfo)
				.HasForeignKey<InfoBase>(ib => ib.MemberId);
				

			builder
				.ToTable("Infos")
				.HasDiscriminator<string>("Type")
				.HasValue<TeacherInfo>("Teacher")
				.HasValue<StudentInfo>("Student");

		}
	}
}
