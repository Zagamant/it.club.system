using System.DAL.Configurations;
using System.DAL.Entities;
using System.DAL.Entities.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<Photo> Photos { get; set; }
        
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
		        .ApplyConfiguration(new ClubConfiguration())
		        .ApplyConfiguration(new GroupConfiguration())
		        .ApplyConfiguration(new RoomConfiguration())
		        .ApplyConfiguration(new PaymentConfiguration())
		        .ApplyConfiguration(new UserConfiguration())
		        .ApplyConfiguration(new PhotoConfiguration())
		        ;

	        
			base.OnModelCreating(modelBuilder);
        }
    }

    
}
