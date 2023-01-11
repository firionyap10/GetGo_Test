using CarBooking.Data.Cars;
using CarBooking.Entities.Cars;

namespace CarBooking.Core.Cars
{
    public class CarService : ICarService
    {
        private readonly ICarData _carData;

        public CarService(ICarData carData)
        {
            _carData = carData;
        }

        public Task<List<Car>> ListCar()
        {
            return _carData.ListCar();
        }

        public Task<Car?> GetCar(decimal x, decimal y)
        {
            return _carData.GetCar(x, y);
        }

        public Task<bool> UpdateCar(Car car)
        {
            return _carData.UpdateCar(car);
        }
    }
}
