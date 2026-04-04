namespace Group3Flight.Models
{
    public class FlightSessions
    {
        private const string CountKey = "ReservationsCount";
        private const string ReservationsKey = "Reservations";
        private const string FromKey = "From";
        private const string ToKey = "To";
        private const string CabinTypeKey = "CabinType";
        private const string DepartureDateKey = "DepartureDate";

        private ISession session { get; set; }
        public FlightSessions(ISession session) => this.session = session;

        public void SetReservations(List<Reservation> reservations)
        {
            session.SetObject(ReservationsKey, reservations);
            session.SetInt32(CountKey, reservations.Count);
        }
        public List<Reservation> GetReservations() =>
            session.GetObject<List<Reservation>>(ReservationsKey) ?? new List<Reservation>();
        public int? GetReservationsCount() => session.GetInt32(CountKey);

        public void SetActiveFrom(string activeFrom) =>
            session.SetString(FromKey, activeFrom);
        public string GetActiveFrom() =>
            session.GetString(FromKey) ?? string.Empty;

        public void SetActiveTo(string activeTo) =>
            session.SetString(ToKey, activeTo);
        public string GetActiveTo() =>
            session.GetString(ToKey) ?? string.Empty;

        public void SetActiveDepartureDate(string activeDeptDate) =>
            session.SetString(DepartureDateKey, activeDeptDate);
        public string GetActiveDepartureDate() =>
            session.GetString(DepartureDateKey) ?? string.Empty;

        public void SetActiveCabinType(string activeCabinType) =>
            session.SetString(CabinTypeKey, activeCabinType);
        public string GetActiveCabinType() =>
            session.GetString(CabinTypeKey) ?? string.Empty;
        public void RemoveMyReservations()
        {
            session.Remove(ReservationsKey);
            session.Remove(CountKey);
        }
    }
}
