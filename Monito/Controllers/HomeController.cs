using Microsoft.AspNetCore.Mvc;

namespace Monito.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
