using Microsoft.AspNetCore.Mvc;
using ORM;
using System.Collections.Generic;

namespace Monito.Controllers
{
    public partial class DashboardController : Controller
    {
        private int _total = 1000;
        private readonly string _format = "M-d H:m:s";
        /// <summary>
        /// color
        /// </summary>
        private readonly List<string> _color = new List<string>
                                      {

                                          "#ff9800",
                                          "#e91e63",
                                          "#673ab7",
                                          "#00bcd4",
                                          "#9c27b0",
                                          "#3f51b5",
                                          "#2196f3",
                                          "#8bc34a",
                                          "#009688",
                                          "#cddc39",
                                          "#f44336"
                                      };

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