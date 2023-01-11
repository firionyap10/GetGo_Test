using CarBooking.API.Controllers;
using CarBooking.Core.Bookings;
using CarBooking.Core.Cars;
using CarBooking.Data.Bookings;
using CarBooking.Data.Cars;
using Microsoft.Extensions.DependencyInjection;

namespace CarBooking.API.Services
{
    public static class ServiceCollections
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<ICarData, CarData>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IBookingData, BookingData>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton(typeof(ILogger<BookingController>), (typeof(Logger<BookingController>)));
        }
    }
}
