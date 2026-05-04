using Group3Flight.Models.DomainModels;

namespace Group3Flight.Models.DataLayer.Repositories
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        bool IsFlightReserved(int flightId);

        Reservation? GetByFlightId(int flightId);
    }
}