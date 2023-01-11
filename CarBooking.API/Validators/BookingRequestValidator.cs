using CarBooking.Entities.Bookings;
using FluentValidation;

namespace CarBooking.API.Validators
{
    public class BookingRequestValidator : AbstractValidator<BookingRequest>
    {
        public BookingRequestValidator()
        {
            RuleFor(x => x.X).GreaterThanOrEqualTo(-180).LessThanOrEqualTo(180).NotEmpty();
            RuleFor(x => x.Y).GreaterThanOrEqualTo(-90).LessThanOrEqualTo(90).NotEmpty();
        }
    }
}
