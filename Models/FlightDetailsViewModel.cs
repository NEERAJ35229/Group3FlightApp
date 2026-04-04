namespace Group3Flight.Models
{
    public class FlightDetailsViewModel
    {
        public string ActiveFromKey { get; set; } = "all";
        public string ActiveToKey { get; set; } = "all";
        public string ActiveDepartureDate { get; set; } = "all";
        public string ActiveCabinType { get; set; } = "all";
        public List<string> CabinTypes { get; set; } = new List<string>();
        public List<Flight> Flight { get; set; } = new List<Flight>();
        public List<Reservation> Reservation { get; set; } = new List<Reservation>();
        public Flight Flights { get; set; } = new Flight();
    }
}
