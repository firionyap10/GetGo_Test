using CarBooking.Core.Bookings;
using CarBooking.Data.Bookings;
using CarBooking.Data.Cars;
using CarBooking.Entities.Bookings;
using CarBooking.Entities.Cars;
using FluentAssertions;
using Moq;

namespace CarBooking.Core.Test.Bookings
{
    public class BookingServiceTest
    {
        private readonly Mock<ICarData> _carData;
        private readonly Mock<IBookingData> _bookingData;
        private readonly IBookingService _bookingService;

        public BookingServiceTest()
        {
            _carData = new Mock<ICarData>();
            _bookingData = new Mock<IBookingData>();
            _bookingService = new BookingService(_bookingData.Object, _carData.Object);
        }

        [Fact]
        public async Task CreateBookingTest_Success()
        {
            _carData.Setup(x => x.GetCar(It.IsAny<decimal>(), It.IsAny<decimal>()))
                .ReturnsAsync(() => new Car
                {
                    Id = 1,
                    Name = "CarA",
                    PlateNumber = "AAAA",
                    X = 1,
                    Y = 2
                });
            _bookingData.Setup(x => x.CreateBooking(It.IsAny<Booking>()))
                .ReturnsAsync(() => new Booking
                {
                    Id = 1,
                    CarId = 2,
                    CreatedDate = DateTime.UtcNow
                });
            var result = await _bookingService.CreateBooking(new BookingRequest
            {
                X = 1,
                Y = 2
            });

            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThanOrEqualTo(1);
        }

        [Fact]
        public async Task CreateBookingTest_Fail()
        {
            _carData.Setup(x => x.GetCar(It.IsAny<decimal>(), It.IsAny<decimal>()))
                .ReturnsAsync(() => new Car
                {
                    Id = 1,
                    Name = "CarA",
                    PlateNumber = "AAAA",
                    X = 1,
                    Y = 2
                });
            _bookingData.Setup(x => x.CreateBooking(It.IsAny<Booking>()))
                .ReturnsAsync(() => new Booking
                {
                    Id = 1,
                    CarId = 2,
                    CreatedDate = DateTime.UtcNow
                });
            var result = await _bookingService.CreateBooking(new BookingRequest());

            result.Should().BeNull();
        }

        [Fact]
        public async Task ReachHomeLotTest_Success()
        {
            _carData.Setup(x => x.UpdateCar(It.IsAny<Car>()))
                .ReturnsAsync(() => true);
            _bookingData.Setup(x => x.GetBooking(It.IsAny<int>()))
                .ReturnsAsync(() => new Booking
                {
                    Id = 1,
                    CarId = 2,
                    CreatedDate = DateTime.UtcNow
                });

            var result = await _bookingService.ReachHomeLot(new BookingUpdateRequest
            {
                BookingId = 1,
                X = 1,
                Y = 2
            });

            result.Should().Be(true);
        }

        [Fact]
        public async Task ReachHomeLotTest_Fail()
        {
            _bookingData.Setup(x => x.GetBooking(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            var result = await _bookingService.ReachHomeLot(new BookingUpdateRequest
            {
                BookingId = 1,
                X = 1,
                Y = 2
            });

            result.Should().Be(false);
        }
    }
}