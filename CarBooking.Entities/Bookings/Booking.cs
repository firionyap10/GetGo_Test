using CarBooking.Entities.Cars;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarBooking.Entities.Bookings
{
    public class Booking
    {
        /// <summary>
        /// Unique id of the booking
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Car Object
        /// </summary>
        public Car Car { get; set; }

        /// <summary>
        /// Car Id refer to car object
        /// </summary>
        public int CarId { get; set; }

        /// <summary>
        /// The record of this booking date
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
