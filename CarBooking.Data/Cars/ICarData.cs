
using CarBooking.Entities.Cars;

namespace CarBooking.Data.Cars
{
    public interface ICarData
    {
        Task<List<Car>> ListCar();

        Task<Car?> GetCar(decimal x, decimal y);

        Task<bool> UpdateCar(Car car);
    }
}
