using Group3Flight.Models;
using Microsoft.AspNetCore.Mvc;

namespace Group3Flight.Controllers
{
    public class ValidationController : Controller
    {
        private FlightDatabaseContext context;
        public ValidationController(FlightDatabaseContext ctx) => context = ctx;

        public JsonResult CheckFlight(string FlightCode, DateTime Date)
        {
            string msg = Check.FlightCodeDateExists(context, FlightCode, Date);
            if (string.IsNullOrEmpty(msg))
            {
                TempData["okFlight"] = true;
                return Json(true);
            }
            else return Json(msg);
        }
    }
}
