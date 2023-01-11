using System.ComponentModel.DataAnnotations.Schema;

namespace CarBooking.Entities.Cars
{
    public class Car
    {
        /// <summary>
        /// Unique id of the car
        /// </summary>
        ///
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Friendly name of the car
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Car plate number
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// X Coordinate
        /// </summary>
        public decimal X { get; set; }

        /// <summary>
        /// Y Coordinate
        /// </summary>
        public decimal Y { get; set; }
    }
}
