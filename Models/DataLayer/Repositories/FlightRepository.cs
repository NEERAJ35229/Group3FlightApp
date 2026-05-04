using Group3Flight.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace Group3Flight.Models.DataLayer.Repositories
{
    public class FlightRepository : Repository<Flight>, IFlightRepository
    {
        public FlightRepository(FlightDatabaseContext ctx) : base(ctx) { }

        public Flight? GetWithAirline(int id)
        {
            return dbset
                .Include(f => f.Airline)
                .FirstOrDefault(f => f.FlightId == id);
        }

        public IEnumerable<Airline> GetAllAirlines()
        {
            return context.Set<Airline>()
                .OrderBy(a => a.AirlineId)
                .ToList();
        }

        public bool FlightCodeDateExists(string flightCode, DateTime date)
        {
            return dbset.Any(f =>
                f.FlightCode == flightCode &&
                f.Date.Date == date.Date);
        }
    }
}