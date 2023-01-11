using CarBooking.Data.Context;
using CarBooking.Entities.Bookings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarBooking.Data.Bookings
{
    public class BookingData : IBookingData
    {
        private readonly CarBookingContext _dbContext;
        public readonly ILogger<BookingData> _logger;

        public BookingData(CarBookingContext dbContext, ILogger<BookingData> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Booking> CreateBooking(Booking booking)
        {
            _dbContext.Add(booking);
            await _dbContext.SaveChangesAsync();
            return booking;
        }

        public async Task<Booking?> GetBooking(int id)
        {
            try
            {
                return await _dbContext.Bookings.FirstOrDefaultAsync(x => x.Id == id);

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }
    }
}
