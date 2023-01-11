using CarBooking.Entities.Bookings;

namespace CarBooking.Data.Bookings
{
    public interface IBookingData
    {
        Task<Booking> CreateBooking(Booking booking);

        Task<Booking?> GetBooking(int id);
    }
}
