using CarBooking.API.Controllers;
using CarBooking.Core.Bookings;
using CarBooking.Core.Cars;
using CarBooking.Entities.Bookings;
using CarBooking.Entities.Cars;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CarBooking.Test
{
    public class BookingControllerTest
    {
        private readonly Mock<ICarService> _carService;
        private readonly Mock<IBookingService> _bookingService;
        private readonly BookingController _controller;


        public BookingControllerTest()
        {
            _carService = new Mock<ICarService>();
            _bookingService = new Mock<IBookingService>();
            _controller = new BookingController(_carService.Object, _bookingService.Object);
        }

        [Fact]
        public async Task ListCarTest_WithResult()
        {
            _carService.Setup(x => x.ListCar()).ReturnsAsync(() => new List<Car>
            {
                new() { Id = 1, Name = "CarA", PlateNumber = "AAAA", X = 1, Y = 2 },
                new() { Id = 2, Name = "CarB", PlateNumber = "BBBB", X = 2, Y = 3 },
                new() { Id = 3, Name = "CarC", PlateNumber = "CCCC", X = 3, Y = 4 },
                new() { Id = 4, Name = "CarD", PlateNumber = "DDDD", X = 4, Y = 5 },
                new() { Id = 5, Name = "CarE", PlateNumber = "EEEE", X = 5, Y = 6 }
            });

            var cars = await _controller.ListCar();

            var result = (OkObjectResult)cars.Result;
            result.Value.Should().NotBeNull();
            result.Value.Should().BeOfType<List<Car>>();

            var item = result.Value as List<Car>;
            item.Should().HaveCountGreaterThan(1);
        }

        [Fact]
        public async Task ListCarTest_WithoutResult()
        {
            _carService.Setup(x => x.ListCar()).ReturnsAsync(() => null);

            var cars = await _controller.ListCar();

            var result = (OkObjectResult)cars.Result;
            result.Value.Should().BeNull();
        }

        [Fact]
        public async Task SearchCarTest_WithResult()
        {
            _carService.Setup(x => x.GetCar(It.IsAny<decimal>(), It.IsAny<decimal>()))
                .ReturnsAsync(
                () => new Car { Id = 1, Name = "CarA", PlateNumber = "AAAA", X = 1, Y = 2 }
                );

            var cars = await _controller.SearchCar(new LocationGetRequest
            {
                X = 1,
                Y = 2
            });

            var result = (OkObjectResult)cars.Result;
            result.Value.Should().NotBeNull();
            result.Value.Should().BeOfType<Car>();
        }

        [Fact]
        public async Task SearchCarTest_404Result()
        {
            _carService.Setup(x => x.GetCar(It.IsAny<decimal>(), It.IsAny<decimal>()))
                .ReturnsAsync(
                    () => null
                );

            var cars = await _controller.SearchCar(new LocationGetRequest
            {
                X = 1,
                Y = 2
            });

            var result = (NotFoundObjectResult)cars.Result;

            var resultObject = result.Value as dynamic;

            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task BookCarTest_Success()
        {
            _bookingService.Setup(x => x.CreateBooking(It.IsAny<BookingRequest>()))
                .ReturnsAsync(
                    () => new Booking { Id = 1, CarId = 1, CreatedDate = DateTime.UtcNow }
                );

            _carService.Setup(x => x.GetCar(It.IsAny<decimal>(), It.IsAny<decimal>()))
                .ReturnsAsync(
                    () => new Car { Id = 1, Name = "CarA", PlateNumber = "AAAA", X = 1, Y = 2 }
                );

            var cars = await _controller.BookCar(new BookingRequest
            {
                X = 1,
                Y = 2
            });

            var result = (OkObjectResult)cars.Result;
            result.Value.Should().NotBeNull();
            result.Value.Should().BeOfType<Booking>();

            var booking = result.Value as Booking;

            booking.Id.Should().BeGreaterThanOrEqualTo(1);
        }

        [Fact]
        public async Task BookCarTest_Fail()
        {
            _bookingService.Setup(x => x.CreateBooking(It.IsAny<BookingRequest>()))
                .ReturnsAsync(
                    () => null
                );

            _carService.Setup(x => x.GetCar(It.IsAny<decimal>(), It.IsAny<decimal>()))
                .ReturnsAsync(
                    () => null
                );

            var cars = await _controller.BookCar(new BookingRequest
            {
                X = 1,
                Y = 2
            });

            var result = (NotFoundObjectResult)cars.Result;

            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task ReachHomeLotTest_Success()
        {
            _bookingService.Setup(x => x.ReachHomeLot(It.IsAny<BookingUpdateRequest>()))
                .ReturnsAsync(
                    () => true
                );

            var cars = await _controller.ReachHomeLot(new BookingUpdateRequest
            {
                X = 1,
                Y = 2,
                BookingId = 1
            });

            var result = (OkObjectResult)cars.Result;
            result.Value.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task ReachHomeLotTest_Fail()
        {
            _bookingService.Setup(x => x.ReachHomeLot(It.IsAny<BookingUpdateRequest>()))
                .ReturnsAsync(
                    () => false
                );

            var cars = await _controller.ReachHomeLot(new BookingUpdateRequest
            {
                X = 1,
                Y = 2,
                BookingId = 1
            });

            var result = (BadRequestObjectResult)cars.Result;
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}