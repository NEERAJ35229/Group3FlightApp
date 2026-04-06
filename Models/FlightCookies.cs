namespace Group3Flight.Models
{
    public class FlightCookies
    {
        private const string ReservationKey = "myReservations";
        private const string Delimiter = "-";

        private IRequestCookieCollection requestCookies { get; set; } = null!;
        private IResponseCookies responseCookies { get; set; } = null!;

        public FlightCookies(IRequestCookieCollection request, IResponseCookies response)
        {
            requestCookies = request;
            responseCookies = response;
        }
        public void RemoveReservationId(int id)
        {
            string[] ids = GetReservationIds();
            var updatedIds = ids.Where(rid => rid != id.ToString()).ToArray();
            SetReservationIds(updatedIds);
        }
        public void SetReservationIds(List<Reservation> myReservations)
        {
            var ids = myReservations.Select(r => r.ReservationId.ToString()).ToList();
            SetReservationIds(ids);
        }
        public void SetReservationIds(IEnumerable<string> ids)
        {
            if (responseCookies == null)
                throw new InvalidOperationException("Response cookies are not initialized.");

            string idsString = string.Join(Delimiter, ids);
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(14),
                IsEssential = true
            };

            RemoveReservationIds();
            responseCookies.Append(ReservationKey, idsString, options);
        }
        public string[] GetReservationIds()
        {
            string cookie = requestCookies[ReservationKey] ?? String.Empty;
            if (string.IsNullOrEmpty(cookie))
                return Array.Empty<string>();
            else
                return cookie.Split(Delimiter);
        }

        public void RemoveReservationIds()
        {
            if (responseCookies == null)
                throw new InvalidOperationException("Response cookies are not initialized.");

            responseCookies.Delete(ReservationKey);
        }
    }
}
