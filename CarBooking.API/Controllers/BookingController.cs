using System.Text.Json.Serialization;
using CarBooking.Core.Bookings;
using CarBooking.Core.Cars;
using CarBooking.Entities.Bookings;
using CarBooking.Entities.Cars;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarBooking.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BookingController : ControllerBase
{
    private readonly ICarService _carService;
    private readonly IBookingService _bookingService;

    public BookingController(ICarService carService, IBookingService bookingService)
    {
        _carService = carService;
        _bookingService = bookingService;
    }


    [HttpGet]
    [Route("cars")]
    public async Task<ActionResult<List<Car>>> ListCar()
    {
        return Ok(await _carService.ListCar());
    }

    [HttpGet]
    [Route("car")]
    public async Task<ActionResult<Car?>> SearchCar([FromQuery] LocationGetRequest locationGetRequest)
    {
        var nearestCar = await _carService.GetCar(locationGetRequest.X, locationGetRequest.Y);

        if (nearestCar != null) 
            return Ok(nearestCar);

        return NotFound(new
        {
            Message = "Unable to find car nearby"
        });
    }

    [HttpPost]
    [Route("car")]
    public async Task<ActionResult<Booking?>> BookCar([FromBody] BookingRequest bookingRequest)
    {
        var booking = await _bookingService.CreateBooking(bookingRequest);
        if (booking != null)
            return Ok(booking);

        return NotFound(new
        {
            Message = "Unable to book a car"
        });
    }

    [HttpPut]
    [Route("car/reach")]
    public async Task<ActionResult<Booking?>> ReachHomeLot([FromBody] BookingUpdateRequest bookingUpdateRequest)
    {
        var booking = await _bookingService.ReachHomeLot(bookingUpdateRequest);
        if (booking)
            return Ok(new
            {
                Message = "Update car lot successfully"
            });

        return BadRequest("Unable to reach car lot");
    }
}