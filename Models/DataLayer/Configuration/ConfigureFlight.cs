using Group3Flight.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group3Flight.Models
{
    internal class ConfigureFlight : IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> entity)
        {
            // seed initial data
            entity.HasData(
                new Flight
                {
                    FlightId = 1,
                    FlightCode = "EK601",
                    From = "Dubai",
                    To = "New York",
                    Date = new DateTime(2026, 5, 5),
                    DepartureTime = new TimeSpan(2, 0, 0),
                    ArrivalTime = new TimeSpan(8, 30, 0),
                    CabinType = "Business",
                    Emission = 650.5,
                    AircraftType = "Boeing 777",
                    Price = 1500,
                    AirlineId = 2,
                },
                new Flight
                {
                    FlightId = 2,
                    FlightCode = "QR702",
                    From = "Doha",
                    To = "London",
                    Date = new DateTime(2026, 5, 7),
                    DepartureTime = new TimeSpan(9, 15, 0),
                    ArrivalTime = new TimeSpan(14, 45, 0),
                    CabinType = "Economy",
                    Emission = 420.3,
                    AircraftType = "Boeing 787 Dreamliner",
                    Price = 800,
                    AirlineId = 1,
                },
                new Flight
                {
                    FlightId = 3,
                    FlightCode = "LH803",
                    From = "Frankfurt",
                    To = "Chicago",
                    Date = new DateTime(2026, 5, 10),
                    DepartureTime = new TimeSpan(13, 0, 0),
                    ArrivalTime = new TimeSpan(16, 30, 0),
                    CabinType = "Economy Plus",
                    Emission = 700.0,
                    AircraftType = "Boeing 747",
                    Price = 1800,
                    AirlineId = 4,
                },
                new Flight
                {
                    FlightId = 4,
                    FlightCode = "AF904",
                    From = "Paris",
                    To = "Dubai",
                    Date = new DateTime(2026, 5, 12),
                    DepartureTime = new TimeSpan(7, 30, 0),
                    ArrivalTime = new TimeSpan(15, 0, 0),
                    CabinType = "Economy",
                    Emission = 500.0,
                    AircraftType = "Airbus A380",
                    Price = 950,
                    AirlineId = 3,
                },
                new Flight
                {
                    FlightId = 5,
                    FlightCode = "SQ1005",
                    From = "Singapore",
                    To = "Dubai",
                    Date = new DateTime(2026, 5, 15),
                    DepartureTime = new TimeSpan(20, 0, 0),
                    ArrivalTime = new TimeSpan(6, 30, 0), // next day
                    CabinType = "Business",
                    Emission = 900.0,
                    AircraftType = "Airbus A350",
                    Price = 2000,
                    AirlineId = 5,
                },
                new Flight
                {
                    FlightId = 6,
                    FlightCode = "EY1106",
                    From = "Abu Dhabi",
                    To = "Sydney",
                    Date = new DateTime(2026, 5, 18),
                    DepartureTime = new TimeSpan(22, 15, 0),
                    ArrivalTime = new TimeSpan(10, 45, 0), // next day
                    CabinType = "Business",
                    Emission = 950.0,
                    AircraftType = "Boeing 787",
                    Price = 2200,
                    AirlineId = 6,
                }
            );
        }
    }

}
