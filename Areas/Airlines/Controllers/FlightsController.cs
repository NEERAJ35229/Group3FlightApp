using System.Security.Policy;
using Group3Flight.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Group3Flight.Areas.Airlines.Controllers
{
    [Area("Airlines")]
    public class FlightsController : Controller
    {
        private FlightDatabaseContext _ctx { get; set; }

        public FlightsController(FlightDatabaseContext ctx) => _ctx = ctx;

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            var airlines = _ctx.Airline
                .OrderBy(m => m.AirlineId).ToList();
            ViewBag.Airlines = new SelectList(airlines, "AirlineId", "Name");
            return View("Edit", new Flight());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Disable = "";
            var airlines = _ctx.Airline
                .OrderBy(m => m.AirlineId).ToList();
            ViewBag.Airlines = new SelectList(airlines, "AirlineId", "Name");
            var flight = _ctx.Flight.Find(id);
            return View(flight);
        }

        [HttpPost]
        public IActionResult Edit(Flight flight)
        {

            if (TempData["okFlight"] == null)
            {
                string msg = Check.FlightCodeDateExists(_ctx, flight.FlightCode.ToString(), flight.Date);
                if (!string.IsNullOrEmpty(msg))
                {
                    ModelState.AddModelError(nameof(flight.FlightCode), msg);
                    TempData["Message"] = "Please fix the error";
                }
            }
            if (ModelState.IsValid)
            {
                if (flight.FlightId == 0)
                {
                    _ctx.Flight.Add(flight);
                    TempData["Message"] = $"{flight.FlightCode} Added Successfully";
                }
                else
                {
                    _ctx.Flight.Update(flight);
                    TempData["Message"] = $"{flight.FlightCode} updated successfully.";
                }
                _ctx.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var airlines = _ctx.Airline
                .OrderBy(m => m.AirlineId).ToList();
                ViewBag.Airlines = new SelectList(airlines, "AirlineId", "Name");
                ViewBag.Action = (flight.FlightId == 0) ? "Add" : "Edit";
                return View(flight);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var flight = _ctx.Flight.Find(id);
            return View(flight);
        }

        [HttpPost]
        public IActionResult Delete(Flight flight)
        {
            _ctx.Flight.Remove(flight);
            _ctx.SaveChanges();
            TempData["Message"] = $"{flight.FlightCode} Deleted Successfully";
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Manage()
        {
            return View();
        }
    }
}
