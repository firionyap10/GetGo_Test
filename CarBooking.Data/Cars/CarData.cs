using CarBooking.Data.Context;
using CarBooking.Entities.Cars;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarBooking.Data.Cars
{
    public class CarData : ICarData
    {
        private readonly CarBookingContext _dbContext;
        public readonly ILogger<CarData> _logger;

        public CarData(CarBookingContext dbContext, ILogger<CarData> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<List<Car>> ListCar()
        {
            try
            {
                return await _dbContext.Cars.ToListAsync();

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<Car?> GetCar(decimal x, decimal y)
        {
            try
            {
                return await _dbContext.Cars.FirstOrDefaultAsync(a => a.X == x && a.Y == y);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<bool> UpdateCar(Car car)
        {
            try
            {
                var result = await _dbContext.Cars.FirstOrDefaultAsync(x => x.Id == car.Id);
                result.X = car.X;
                result.Y = car.Y;
                await _dbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }
    }
}
