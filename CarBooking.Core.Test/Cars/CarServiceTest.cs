using CarBooking.Core.Cars;
using CarBooking.Data.Cars;
using CarBooking.Entities.Cars;
using FluentAssertions;
using Moq;

namespace CarBooking.Core.Test.Cars
{
    public class CarServiceTest
    {
        private readonly Mock<ICarData> _carData;
        private readonly ICarService _carService;

        public CarServiceTest()
        {
            _carData = new Mock<ICarData>();
            _carService = new CarService(_carData.Object);
        }

        [Fact]
        public async Task ListCar_Success()
        {
            _carData.Setup(x => x.ListCar())
                .ReturnsAsync(() => new List<Car>
                {
                    new() { Id = 1, Name = "CarA", PlateNumber = "AAAA", X = 1, Y = 2 },
                    new() { Id = 2, Name = "CarB", PlateNumber = "BBBB", X = 2, Y = 3 },
                    new() { Id = 3, Name = "CarC", PlateNumber = "CCCC", X = 3, Y = 4 },
                    new() { Id = 4, Name = "CarD", PlateNumber = "DDDD", X = 4, Y = 5 },
                    new() { Id = 5, Name = "CarE", PlateNumber = "EEEE", X = 5, Y = 6 }
                });

            var result = await _carService.ListCar();

            result.Should().HaveCountGreaterThanOrEqualTo(1);
        }

        [Fact]
        public async Task ListCar_fail()
        {
            _carData.Setup(x => x.ListCar())
                .ReturnsAsync(() => null);

            var result = await _carService.ListCar();

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetCar_Success()
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

            var result = await _carService.GetCar(1,2);

            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GetCar_Fail()
        {
            _carData.Setup(x => x.GetCar(It.IsAny<decimal>(), It.IsAny<decimal>()))
                .ReturnsAsync(() => null);

            var result = await _carService.GetCar(1, 2);

            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateCar_Success()
        {
            _carData.Setup(x => x.UpdateCar(It.IsAny<Car>()))
                .ReturnsAsync(() => true);

            var result = await _carService.UpdateCar(new Car
            {
                Id = 1,
                Name = "CarA",
                PlateNumber = "AAAA",
                X = 1,
                Y = 2
            });

            result.Should().Be(true);
        }

        [Fact]
        public async Task UpdateCar_Fail()
        {
            _carData.Setup(x => x.UpdateCar(It.IsAny<Car>()))
                .ReturnsAsync(() => false);

            var result = await _carService.UpdateCar(new Car
            {
                Id = 1,
                Name = "CarA",
                PlateNumber = "AAAA",
                X = 1,
                Y = 2
            });

            result.Should().Be(false);
        }
    }
}
