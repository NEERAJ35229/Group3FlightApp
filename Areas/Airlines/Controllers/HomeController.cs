using Microsoft.AspNetCore.Mvc;

namespace Group3Flight.Areas.Airlines.Controllers
{
    [Area("Airlines")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Regulations()
        {
            return Content("Area -> Airlines, Controller -> Flights, Action: Regulations");
        }
    }
}
