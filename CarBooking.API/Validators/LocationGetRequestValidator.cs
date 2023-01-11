using CarBooking.Entities.Cars;
using FluentValidation;
using System.Net;

namespace CarBooking.API.Validators
{
    public class LocationGetRequestValidator : AbstractValidator<LocationGetRequest>
    {
        public LocationGetRequestValidator()
        {
            RuleFor(x => x.X).GreaterThanOrEqualTo(-180).LessThanOrEqualTo(180).NotEmpty();
            RuleFor(x => x.Y).GreaterThanOrEqualTo(-90).LessThanOrEqualTo(90).NotEmpty();
        }
    }
}
