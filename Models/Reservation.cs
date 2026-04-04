using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Group3Flight.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public int FlightId { get; set; }
        [ValidateNever]
        public Flight Flight { get; set; } = null!;
    }
}
