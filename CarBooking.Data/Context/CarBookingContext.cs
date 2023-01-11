using System.Runtime.CompilerServices;
using CarBooking.Entities.Bookings;
using CarBooking.Entities.Cars;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CarBooking.Data.Context
{
    public class CarBookingContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public CarBookingContext(DbContextOptions options) : base(options)
        {

        }

        public CarBookingContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                var connectionString = configuration.GetConnectionString("ConnectionStrings:CarBookingDb");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(c =>
            {
                c.HasKey(p => p.Id);
                c.Property(p => p.Name).HasMaxLength(100).IsRequired();
                c.Property(p => p.PlateNumber).HasMaxLength(20).IsRequired();
                c.Property(p => p.X).HasPrecision(11, 8);
                c.Property(p => p.Y).HasPrecision(11, 8);
            });

            modelBuilder.Entity<Car>().HasData(
                new Car { Id = 1, Name = "CarA", PlateNumber = "AAAA", X = 1, Y = 2 },
                new Car { Id = 2, Name = "CarB", PlateNumber = "BBBB", X = 2, Y = 3 },
                new Car { Id = 3, Name = "CarC", PlateNumber = "CCCC", X = 3, Y = 4 },
                new Car { Id = 4, Name = "CarD", PlateNumber = "DDDD", X = 4, Y = 5 },
                new Car { Id = 5, Name = "CarE", PlateNumber = "EEEE", X = 5, Y = 6 }
            );

            modelBuilder.Entity<Booking>(c =>
            {
                c.HasKey(p => p.Id);
                c.Property(p => p.CreatedDate).IsRequired();
            });
        }
    }
}
