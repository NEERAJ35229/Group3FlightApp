using Group3Flight.Models.DomainModels;
using Group3Flight.Models.ViewModels;
using Group3Flight.Models.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;
using Group3Flight.Models.DataLayer.Repositories;
using Group3Flight.Models;

namespace Group3Flight.Controllers
{
    public class HomeController : Controller
    {
        //private FlightDatabaseContext _ctx;
        private IReservationRepository reservationRepo;
        private IFlightRepository flightRepo;
        //public HomeController(FlightDatabaseContext ctx)
        //{
        //    _ctx = ctx;
        //}
        public HomeController(IFlightRepository fRepo, IReservationRepository rRepo)
        {
            flightRepo = fRepo;
            reservationRepo = rRepo;
        }
        public ViewResult Index(FlightDetailsViewModel model)
        {
            var session = new FlightSessions(HttpContext.Session);

            session.SetActiveFrom(model.ActiveFromKey);
            session.SetActiveTo(model.ActiveToKey);
            session.SetActiveDepartureDate(model.ActiveDepartureDate);
            session.SetActiveCabinType(model.ActiveCabinType);

            var cookies = new FlightCookies(Request.Cookies, Response.Cookies);
            var ids = cookies.GetReservationIds();
            var reservations = session.GetReservations();

            if (reservations == null || !reservations.Any())
            {
                if (ids.Length > 0)
                {
                    var flights = flightRepo.List(new QueryOptions<Flight>
                    {
                        Includes = "Airline"
                    }).Where(f => ids.Contains(f.FlightId.ToString())).ToList();

                    reservations = flights.Select(f => new Reservation
                    {
                        ReservationId = f.FlightId,
                        FlightId = f.FlightId,
                        Flight = f
                    }).ToList();

                    session.SetReservations(reservations);
                }
            }

            var options = new QueryOptions<Flight>
            {
                Includes = "Airline",
                OrderBy = f => f.FlightCode
            };

            var flightsQuery = flightRepo.List(options).AsQueryable();

            if (!string.IsNullOrEmpty(model.ActiveFromKey) && model.ActiveFromKey.ToLower() != "all")
            {
                flightsQuery = flightsQuery.Where(f => f.From == model.ActiveFromKey);
            }

            if (!string.IsNullOrEmpty(model.ActiveToKey) && model.ActiveToKey.ToLower() != "all")
            {
                flightsQuery = flightsQuery.Where(f => f.To == model.ActiveToKey);
            }

            if (!string.IsNullOrEmpty(model.ActiveDepartureDate) && model.ActiveDepartureDate.ToLower() != "all")
            {
                DateTime selectedDate = DateTime.Parse(model.ActiveDepartureDate);
                flightsQuery = flightsQuery.Where(f => f.Date.Date == selectedDate.Date);
            }

            if (!string.IsNullOrEmpty(model.ActiveCabinType) && model.ActiveCabinType.ToLower() != "all")
            {
                flightsQuery = flightsQuery.Where(f => f.CabinType == model.ActiveCabinType);
            }

            model.CabinTypes = new List<string>
            {
                "Basic Economy",
                "Economy",
                "Economy Plus",
                "Business"
            };

            model.FromCities = flightRepo.List(new QueryOptions<Flight>())
                .Select(f => f.From).Distinct().ToList();

            model.ToCities = flightRepo.List(new QueryOptions<Flight>())
                .Select(f => f.To).Distinct().ToList();

            model.Flight = flightsQuery.ToList();

            return View(model);
        }
        [HttpPost]
        public IActionResult ReserveAll()
        {
            var session = new FlightSessions(HttpContext.Session);
            var reservations = session.GetReservations();

            if (reservations == null || !reservations.Any())
            {
                TempData["Error"] = "No flights selected to reserve.";
                return RedirectToAction("Reservations");
            }

            foreach (var item in reservations)
            {
                if (!reservationRepo.IsFlightReserved(item.FlightId))
                {
                    var reservation = new Reservation
                    {
                        FlightId = item.FlightId
                    };

                    reservationRepo.Insert(reservation);
                }
            }

            reservationRepo.Save();

            TempData["Message"] = "Flights successfully reserved!";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddReservation(int id)
        {
            var session = new FlightSessions(HttpContext.Session);
            var cookies = new FlightCookies(Request.Cookies, Response.Cookies);

            var flight = flightRepo.GetWithAirline(id);
            if (flight == null)
                return NotFound();

            var myReservations = session.GetReservations();

            myReservations.Add(new Reservation
            {
                ReservationId = id,
                FlightId = id,
                Flight = flight
            });

            session.SetReservations(myReservations);
            cookies.SetReservationIds(myReservations);

            TempData["Message"] = "Flight added to selection!";

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

            var reservations = session.GetReservations();

            if (reservations == null || !reservations.Any())
            {
                var ids = cookies.GetReservationIds();

                if (ids.Length > 0)
                {
                    var flights = flightRepo.List(new QueryOptions<Flight>
                    {
                        Includes = "Airline"
                    }).Where(f => ids.Contains(f.FlightId.ToString())).ToList();

                    reservations = flights.Select(f => new Reservation
                    {
                        ReservationId = f.FlightId,
                        FlightId = f.FlightId,
                        Flight = f
                    }).ToList();

                    session.SetReservations(reservations);
                }
            }

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
            session.RemoveMyReservations();

            var cookies = new FlightCookies(Request.Cookies, Response.Cookies);
            cookies.RemoveReservationIds();

            TempData["Message"] = "All selections cleared!";
            return RedirectToAction("Reservations");
        }



        [HttpPost]
        public IActionResult CancelReservation(int id)
        {
            var session = new FlightSessions(HttpContext.Session);

            var myReservations = session.GetReservations();
            var reservation = myReservations.FirstOrDefault(r => r.ReservationId == id);

            if (reservation != null)
            {
                myReservations.Remove(reservation);
                session.SetReservations(myReservations);
            }

            var cookies = new FlightCookies(Request.Cookies, Response.Cookies);
            cookies.SetReservationIds(myReservations);

            TempData["Message"] = "Selection removed!";
            return RedirectToAction("Reservations");
        }


        public IActionResult Details(int id)
        {
            var flight = flightRepo.GetWithAirline(id);
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
