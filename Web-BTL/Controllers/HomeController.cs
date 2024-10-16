using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web_BTL.Models;
using Web_BTL.Repository;

namespace Web_BTL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext db;
        public HomeController(DataContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View(db.Medias.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
