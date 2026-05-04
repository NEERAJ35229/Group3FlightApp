using Group3Flight.Models.DomainModels;

namespace Group3Flight.Models.DataLayer.Repositories
{
    public interface IFlightRepository : IRepository<Flight>
    {
        Flight? GetWithAirline(int id);

        IEnumerable<Airline> GetAllAirlines();

        bool FlightCodeDateExists(string flightCode, DateTime date);
    }
}