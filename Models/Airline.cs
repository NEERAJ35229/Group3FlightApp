using System.ComponentModel.DataAnnotations;

namespace Group3Flight.Models
{
    public class Airline
    {
        public int AirlineId { get; set; }

        [Required(ErrorMessage = "Enter a Name.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Enter a ImageName.")]
        public string ImageName { get; set; } = string.Empty;
    }
}
