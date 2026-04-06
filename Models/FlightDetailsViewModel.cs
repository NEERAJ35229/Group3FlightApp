namespace Group3Flight.Models
{
    public class FlightDetailsViewModel
    {
        public string ActiveFromKey { get; set; } = "all";
        public string ActiveToKey { get; set; } = "all";
        public string ActiveDepartureDate { get; set; } = "all";
        public string ActiveCabinType { get; set; } = "all";
        public List<string> CabinTypes { get; set; } = new List<string>();
        public List<string> FromCities { get; set; } = new();
        public List<string> ToCities { get; set; } = new();
        public List<Flight> Flight { get; set; } = new List<Flight>();
        public List<Reservation> Reservation { get; set; } = new List<Reservation>();
        public Flight Flights { get; set; } = new Flight();
    }
}
