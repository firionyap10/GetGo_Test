using System.ComponentModel.DataAnnotations;

namespace CarBooking.Entities.Cars
{
    public class LocationGetRequest
    {
        /// <summary>
        /// x coordinate
        /// </summary>
        public decimal X { get; set; }

        /// <summary>
        /// y coordinate
        /// </summary>
        public decimal Y { get; set; }
    }
}
