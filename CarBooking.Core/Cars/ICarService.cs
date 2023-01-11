using CarBooking.Entities.Cars;

namespace CarBooking.Core.Cars
{
    public interface ICarService
    {
        Task<List<Car>> ListCar();

        Task<Car?> GetCar(decimal x, decimal y);

        Task<bool> UpdateCar(Car car);
    }
}
