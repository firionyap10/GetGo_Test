using CarBooking.Data.Bookings;
using CarBooking.Data.Cars;
using CarBooking.Entities.Bookings;
using CarBooking.Entities.Cars;

namespace CarBooking.Core.Bookings
{
    public class BookingService : IBookingService
    {
        private readonly IBookingData _bookingData;
        private readonly ICarData _carData;

        public BookingService(IBookingData bookingData, ICarData carData)
        {
            _bookingData = bookingData;
            _carData = carData;
        }

        public async Task<Booking?> CreateBooking(BookingRequest bookingRequest)
        {
            var nearestCar = await _carData.GetCar(bookingRequest.X, bookingRequest.Y); ;

            if (nearestCar == null ||
                Math.Abs(nearestCar.X - bookingRequest.X) + Math.Abs(nearestCar.Y - bookingRequest.Y) > 2) return null;

            var booking = await _bookingData.CreateBooking(new Booking
            {
                CarId = nearestCar.Id,
                CreatedDate = DateTime.UtcNow
            });

            return booking;

        }

        public async Task<bool> ReachHomeLot(BookingUpdateRequest bookingUpdateRequest)
        {
            var booking = await _bookingData.GetBooking(bookingUpdateRequest.BookingId);

            if (booking == null) return false;
            await _carData.UpdateCar(new Car
            {
                Id = booking.CarId,
                X = bookingUpdateRequest.X,
                Y = bookingUpdateRequest.Y
            });
            return true;

        }
    }
}
