using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CarBooking.Data.Context
{
    internal class CarBookingContextFactory : IDesignTimeDbContextFactory<CarBookingContext>
    {
        public CarBookingContext CreateDbContext(string[] args)
        {
            var dbContextBuilder = new DbContextOptionsBuilder<CarBookingContext>();
            var connString = "Server=localhost;Database=CarBooking;Persist Security Info=True;TrustServerCertificate=True;Connection Timeout=60;User ID=myadmin;Password=CarBooking@123;";
            dbContextBuilder.UseSqlServer(connString);
            return new CarBookingContext(dbContextBuilder.Options);
        }
    }
}
