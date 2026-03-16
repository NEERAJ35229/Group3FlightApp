using Microsoft.AspNetCore.Mvc;

namespace Group3Flight.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Manage()
        {
            return Content("Area -> Admin, Controller -> Users, Action -> Manage");
        }
        public IActionResult RightsAndObligations()
        {
            return Content("Area -> Admin, Controller -> Users, Action -> RightsAndObligations");
        }
    }
}
