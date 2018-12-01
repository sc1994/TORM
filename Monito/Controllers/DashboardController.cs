using Microsoft.AspNetCore.Mvc;

namespace Monito.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View("Error");
        }
    }
}