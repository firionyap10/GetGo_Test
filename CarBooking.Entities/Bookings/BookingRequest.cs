using System.ComponentModel.DataAnnotations;

namespace CarBooking.Entities.Bookings
{
    public class BookingRequest
    {
        /// <summary>
        /// x coordinate
        /// </summary>
        ///
        [Range(-180, 180, ErrorMessage = "Range for x coordinate between -90 to 90 ")]
        public decimal X { get; set; }

        /// <summary>
        /// y coordinate
        /// </summary>
        [Range(-90, 90, ErrorMessage = "Range for x coordinate between -90 to 90 ")]
        public decimal Y { get; set; }
    }
}
