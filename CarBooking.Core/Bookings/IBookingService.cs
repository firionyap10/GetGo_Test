using CarBooking.Entities.Bookings;
using CarBooking.Entities.Cars;

namespace CarBooking.Core.Bookings
{
    public interface IBookingService
    {
        Task<Booking?> CreateBooking(BookingRequest bookingRequest);

        Task<bool> ReachHomeLot(BookingUpdateRequest bookingUpdateRequest);
    }
}
