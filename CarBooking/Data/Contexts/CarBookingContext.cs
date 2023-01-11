    using CarBooking.Domain.Cars;
    using Microsoft.EntityFrameworkCore;

    namespace CarBooking.API.Data.Contexts
{
    public class CarBookingContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }

        public CarBookingContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(c =>
            {
                c.HasKey(p => p.Id);
                c.Property(p => p.Name).HasMaxLength(100);
                c.Property(p => p.PlateNumber).HasMaxLength(20);
            });
        }
    }
}
