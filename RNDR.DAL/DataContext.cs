using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RNDR.DAL.Enums;
using RNDR.DAL.Models;

namespace RNDR.DAL
{
    /// <summary>
    /// Represents an application database context.
    /// </summary>
    public sealed class DataContext : IdentityDbContext<User, Role, int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataContext"/> class.
        /// </summary>
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<Address> Addresses{ get; set; }
        public DbSet<StudentInfo> StudentInfos { get; set; }
        public DbSet<TeacherInfo> TeacherInfos { get; set; }

        public DbSet<Agreement> Agreements { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public DbSet<Club> Clubs { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Group> Groups { get; set; }

        public DbSet<Costs> Costs{ get; set; }
        public DbSet<Event> Events{ get; set; }

        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
	        base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
	        modelBuilder
		        .Entity<Agreement>()
		        .HasOne<User>(agr => agr.User)
		        .WithMany(user => user.Agreements);

	        modelBuilder
		        .Entity<Club>()
		        .HasMany<Room>(club => club.Rooms)
		        .WithOne(r => r.Club)
		        .OnDelete(DeleteBehavior.Cascade);

	        modelBuilder
		        .Entity<Club>()
		        .HasOne<Address>()
		        .WithOne();

	        modelBuilder
		        .Entity<Group>()
		        .HasOne(group => group.Room)
		        .WithMany(room => room.Groups);

	        modelBuilder
		        .Entity<Group>()
		        .HasOne(group => group.Course)
		        .WithMany(course => course.Groups);

	        modelBuilder
		        .Entity<Room>()
		        .HasMany<Group>()
		        .WithOne(group => group.Room);


			modelBuilder
		        .Entity<Group>()
		        .Property(e => e.Status)
		        .HasConversion(
			        v => v.ToString(),
			        v => (GroupStatus)Enum.Parse(typeof(GroupStatus), v));



	        base.OnModelCreating(modelBuilder);
        }
    }

    
}
