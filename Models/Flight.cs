using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Group3Flight.Models
{
    public class Flight
    {
        public int FlightId { get; set; }

        // FlightCode: 2 letters + 1–4 digits
        [Required(ErrorMessage = "Enter a FlightCode.")]
        [RegularExpression(@"^[A-Za-z]{2}\d{1,4}$",
            ErrorMessage = "FlightCode must start with 2 letters followed by 1-4 digits.")]
        [Remote(action: "CheckFlight", controller: "Validation", areaName: "",
            AdditionalFields = nameof(Date),
            ErrorMessage = "Flight already exists for this date.")]
        public string FlightCode { get; set; } = string.Empty;

        // From: letters only, max 50
        [Required(ErrorMessage = "Enter a From.")]
        [RegularExpression(@"^[A-Za-z\s]{1,50}$",
            ErrorMessage = "From must contain only letters and max 50 characters.")]
        public string From { get; set; } = string.Empty;

        // To: letters only, max 50
        [Required(ErrorMessage = "Enter a To.")]
        [RegularExpression(@"^[A-Za-z\s]{1,50}$",
            ErrorMessage = "To must contain only letters and max 50 characters.")]
        public string To { get; set; } = string.Empty;

        // Date: Custom validation
        [Required(ErrorMessage = "Enter a Date.")]
        [FutureDateValidation(3, ErrorMessage = "Entered Date must be a legitimate date that is larger than today’s date but not over 3 years")]
        public DateTime Date { get; set; }

        // Time (TimeSpan already ensures valid format)
        [Required(ErrorMessage = "Enter a DepartureTime.")]
        public TimeSpan DepartureTime { get; set; }

        [Required(ErrorMessage = "Enter an ArrivalTime.")]
        public TimeSpan ArrivalTime { get; set; }

        // Dropdown (no validation required)
        [Required(ErrorMessage = "Enter a CabinType.")]
        public string CabinType { get; set; } = string.Empty;

        // Emission <= 5000
        [Required(ErrorMessage = "Enter an Emission.")]
        [Range(0, 5000, ErrorMessage = "Emission cannot exceed 5000 kg CO2e.")]
        public double Emission { get; set; }

        // Dropdown
        [Required(ErrorMessage = "Enter an AircraftType.")]
        public string AircraftType { get; set; } = string.Empty;

        // Price: 0 to 50000
        [Required(ErrorMessage = "Enter a Price.")]
        [Range(0, 50000, ErrorMessage = "Price must be between 0 and 50,000 USD.")]
        public int Price { get; set; }

        public int AirlineId { get; set; }

        [ValidateNever]
        public Airline Airline { get; set; } = null!;
    }
}