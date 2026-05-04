using Group3Flight.Models;
using Group3Flight.Models.DataLayer.Repositories;
using Group3Flight.Models.DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Group3Flight.Areas.Airlines.Controllers
{
    [Area("Airlines")]
    public class FlightsController : Controller
    {
        private IFlightRepository flightRepo;
        private IReservationRepository reservationRepo;
        private IRepository<Airline> airlineRepo;


        public FlightsController(IFlightRepository fRepo, IRepository<Airline> aRepo, IReservationRepository reservationRepo)
        {
            flightRepo = fRepo;
            airlineRepo = aRepo;
            this.reservationRepo = reservationRepo;
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";

            var airlines = flightRepo.GetAllAirlines();
            ViewBag.Airlines = new SelectList(airlines, "AirlineId", "Name");

            return View("Edit", new Flight());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";

            var airlines = flightRepo.GetAllAirlines();
            ViewBag.Airlines = new SelectList(airlines, "AirlineId", "Name");

            var flight = flightRepo.Get(id);

            return View(flight);
        }

        [HttpPost]
        public IActionResult Edit(Flight flight)
        {
            if (TempData["okFlight"] == null)
            {
                if (flightRepo.FlightCodeDateExists(flight.FlightCode, flight.Date))
                {
                    ModelState.AddModelError(nameof(flight.FlightCode),
                        "Flight already exists for this date.");

                    TempData["Message"] = "Please fix the error";
                }
            }

            if (ModelState.IsValid)
            {
                if (flight.FlightId == 0)
                {
                    flightRepo.Insert(flight);
                    TempData["Message"] = $"{flight.FlightCode} Added Successfully";
                }
                else
                {
                    flightRepo.Update(flight);
                    TempData["Message"] = $"{flight.FlightCode} updated successfully.";
                }

                flightRepo.Save();
                return RedirectToAction("Index", "Home");
            }

            var airlines = flightRepo.GetAllAirlines();
            ViewBag.Airlines = new SelectList(airlines, "AirlineId", "Name");

            ViewBag.Action = (flight.FlightId == 0) ? "Add" : "Edit";

            return View(flight);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var flight = flightRepo.Get(id);
            return View(flight);
        }
        [HttpPost]
        public IActionResult Delete(Flight flight)
        {
            if (reservationRepo.IsFlightReserved(flight.FlightId))
            {
                TempData["Message"] = "Cannot delete reserved flight.";
                return RedirectToAction("Index", "Home");
            }

            flightRepo.Delete(flight);
            flightRepo.Save();

            TempData["Message"] = $"{flight.FlightCode} Deleted Successfully";

            return RedirectToAction("Index", "Home");
        }
        //[HttpPost]
        //public IActionResult Delete(Flight flight)
        //{
        //    flightRepo.Delete(flight);
        //    flightRepo.Save();

        //    TempData["Message"] = $"{flight.FlightCode} Deleted Successfully";

        //    return RedirectToAction("Index", "Home");
        //}
        public IActionResult Manage()
        {
            return View();
        }
    }
}
