namespace Group3Flight.Models
{
    public static class Check
    {
        public static string FlightCodeDateExists(FlightDatabaseContext ctx, string flightCode, DateTime date)
        {
            string msg = string.Empty;

            if (!string.IsNullOrWhiteSpace(flightCode))
            {
                var flight = ctx.Flight
                    .FirstOrDefault(f => f.FlightCode == flightCode && f.Date == date);

                if (flight != null)
                {
                    msg = $"Flight {flightCode} already exists for this date.";
                }
            }
            else
            {
                msg = "Invalid FlightCode.";
            }

            return msg;
        }
    }
}
