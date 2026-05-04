using Group3Flight.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace Group3Flight.Models.DataLayer.Repositories
{
    public class ReservationRepository : Repository<Reservation>, IReservationRepository
    {
        public ReservationRepository(FlightDatabaseContext ctx) : base(ctx) { }

        public bool IsFlightReserved(int flightId)
        {
            return dbset.Any(r => r.FlightId == flightId);
        }

        public Reservation? GetByFlightId(int flightId)
        {
            return dbset
                .Include(r => r.Flight)
                .FirstOrDefault(r => r.FlightId == flightId);
        }
    }
}