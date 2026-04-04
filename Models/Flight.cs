using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Group3Flight.Models
{
    public class Flight
    {
        public int FlightId { get; set; }

        [Required(ErrorMessage = "Enter a FlightCode.")]
        public string FlightCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Enter a From.")]
        public string From { get; set; } = string.Empty;

        [Required(ErrorMessage = "Enter a To.")]
        public string To { get; set; } = string.Empty;

        [Required(ErrorMessage = "Enter a Date.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Enter a DepartureTime.")]
        public TimeSpan DepartureTime { get; set; }

        [Required(ErrorMessage = "Enter a ArrivalTime.")]
        public TimeSpan ArrivalTime { get; set; }

        [Required(ErrorMessage = "Enter a CabinType.")]
        public string CabinType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Enter a Emission.")]
        public double Emission { get; set; }

        [Required(ErrorMessage = "Enter a AircraftType.")]
        public string AircraftType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Enter a Price.")]
        public int Price { get; set; }
        public int AirlineId { get; set; }
        [ValidateNever]
        public Airline Airline { get; set; } = null!;
    }
}
