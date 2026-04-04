using Group3Flight.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Group3Flight.Controllers
{
    public class HomeController : Controller
    {
        private FlightDatabaseContext _ctx;
        public HomeController(FlightDatabaseContext ctx)
        {
            _ctx = ctx;
        }
        public ViewResult Index(FlightDetailsViewModel model)
        {
            var session = new FlightSessions(HttpContext.Session);

            session.SetActiveFrom(model.ActiveFromKey);
            session.SetActiveTo(model.ActiveToKey);
            session.SetActiveDepartureDate(model.ActiveDepartureDate);
            session.SetActiveCabinType(model.ActiveCabinType);

            int? count = session.GetReservationsCount();

            if (!count.HasValue)
            {
                var cookies = new FlightCookies(Request.Cookies, Response.Cookies);
                string[] ids = cookies.GetReservationIds();

                if (ids.Length > 0)
                {
                    var myReservations = _ctx.Reservation
                        .Include(r => r.Flight)
                        .Where(r => ids.Contains(r.ReservationId.ToString()))
                        .ToList();

                    session.SetReservations(myReservations);
                }
            }

            IQueryable<Flight> query = _ctx.Flight
                .Include(r => r.Airline)
                .OrderBy(r => r.FlightCode);

            // From filter
            if (!string.IsNullOrEmpty(model.ActiveFromKey) &&
                model.ActiveFromKey.ToLower() != "all")
            {
                query = query.Where(r => r.From == model.ActiveFromKey);
            }

            // To filter
            if (!string.IsNullOrEmpty(model.ActiveToKey) &&
                model.ActiveToKey.ToLower() != "all")
            {
                query = query.Where(r => r.To == model.ActiveToKey);
            }

            if (!string.IsNullOrEmpty(model.ActiveDepartureDate) && model.ActiveDepartureDate.ToLower() != "all")
            {
                DateTime selectedDate = DateTime.Parse(model.ActiveDepartureDate);

                query = query.Where(r => r.Date.Date == selectedDate.Date);
            }

            // Cabin filter
            if (!string.IsNullOrEmpty(model.ActiveCabinType) &&
                model.ActiveCabinType.ToLower() != "all")
            {
                query = query.Where(r => r.CabinType == model.ActiveCabinType);
            }

            model.CabinTypes = new List<string>
            {
                "Basic Economy",
                "Economy",
                "Economy Plus",
                "Business"
            };

            model.Flight = query.ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult AddReservation(int id)
        {
            var session = new FlightSessions(HttpContext.Session);
            var cookies = new FlightCookies(Request.Cookies, Response.Cookies);

            var reservation = new Reservation
            {
                FlightId = id,
            };

            _ctx.Reservation.Add(reservation);
            _ctx.SaveChanges();

            var myReservations = session.GetReservations();
            myReservations.Add(reservation);
            session.SetReservations(myReservations);
            cookies.SetReservationIds(myReservations);

            TempData["Message"] = "Your ticket has been confirmed!.";

            return RedirectToAction("Index", new
            {
                ActiveFromKey = session.GetActiveFrom(),
                ActiveToKey = session.GetActiveTo(),
                ActiveDepartureDate = session.GetActiveDepartureDate(),
                ActiveCabinType = session.GetActiveCabinType()
            });
        }

        public IActionResult Reservations()
        {
            var session = new FlightSessions(HttpContext.Session);
            var cookies = new FlightCookies(Request.Cookies, Response.Cookies);

            var reservationIds = cookies.GetReservationIds();

            var reservations = _ctx.Reservation
                .Include(r => r.Flight)
                    .ThenInclude(f => f.Airline)
                .Where(r => reservationIds.Contains(r.ReservationId.ToString()))
                .ToList();

            var model = new FlightDetailsViewModel
            {
                Reservation = reservations,
                ActiveFromKey = session.GetActiveFrom(),
                ActiveToKey = session.GetActiveTo(),
                ActiveDepartureDate = session.GetActiveDepartureDate(),
                ActiveCabinType = session.GetActiveCabinType()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteAllReservations()
        {
            var session = new FlightSessions(HttpContext.Session);
            var myReservations = session.GetReservations();

            if (myReservations != null && myReservations.Any())
            {
                var ids = myReservations.Select(r => r.ReservationId).ToList();

                var reservations = _ctx.Reservation
                    .Where(r => ids.Contains(r.ReservationId))
                    .ToList();

                _ctx.Reservation.RemoveRange(reservations);
                _ctx.SaveChanges();

                session.RemoveMyReservations();

                var cookies = new FlightCookies(Request.Cookies, Response.Cookies);
                cookies.RemoveReservationIds();
            }

            TempData["Message"] = "All Reservations cancelled successfully!";
            return RedirectToAction("Reservations");
        }


        [HttpPost]
        public IActionResult CancelReservation(int id)
        {
            var session = new FlightSessions(HttpContext.Session);
            var reservation = _ctx.Reservation.Find(id);
            if (reservation != null)
            {
                _ctx.Reservation.Remove(reservation);
                _ctx.SaveChanges();
            }

            var myReservations = session.GetReservations();
            var reservation1 = myReservations.FirstOrDefault(r => r.ReservationId == id);
            if (reservation1 != null)
            {
                myReservations.Remove(reservation1);
                session.SetReservations(myReservations);
            }

            var cookies = new FlightCookies(Request.Cookies, Response.Cookies);
            cookies.RemoveReservationId(id);

            TempData["Message"] = "Ticket cancelled successfully!";
            return RedirectToAction("Reservations");
        }


        public IActionResult Details(int id)
        {
            var flight = _ctx.Flight
                .Include(r => r.Airline)
                .FirstOrDefault(r => r.FlightId == id);
            if (flight == null)
                return NotFound();

            var session = new FlightSessions(HttpContext.Session);

            var viewModel = new FlightDetailsViewModel
            {
                Flights = flight,
                ActiveFromKey = session.GetActiveFrom(),
                ActiveToKey = session.GetActiveTo(),
                ActiveDepartureDate = session.GetActiveDepartureDate(),
                ActiveCabinType = session.GetActiveCabinType()
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
