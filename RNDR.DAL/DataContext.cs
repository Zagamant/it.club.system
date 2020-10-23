﻿using System.DAL.Configurations;
using System.DAL.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.DAL.Models;

namespace System.DAL
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
		        .ApplyConfiguration(new InfoBaseConfiguration());

	        modelBuilder
		        .Entity<Club>()
		        .HasOne<Address>(club => club.Address)
		        .WithMany();

	        modelBuilder
		        .Entity<Club>()
		        .HasMany<Room>(club => club.Rooms)
		        .WithOne(r => r.Club)
		        .OnDelete(DeleteBehavior.Cascade);

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
					s => s.ToString(),
					str => (GroupStatus)Enum.Parse(typeof(GroupStatus), str));

			modelBuilder
				.Entity<Club>()
				.Property(c => c.Status)
				.HasConversion(
					s => s.ToString(),
					str => (ClubStatus)Enum.Parse(typeof(ClubStatus), str));

			modelBuilder
				.Entity<Room>()
				.Property(r => r.Status)
				.HasConversion(
					s => s.ToString(),
					str => (RoomStatus)Enum.Parse(typeof(RoomStatus), str));


			base.OnModelCreating(modelBuilder);
        }
    }

    
}
