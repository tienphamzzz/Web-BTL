using Microsoft.AspNetCore.Mvc;

namespace Web_BTL.Controllers
{
    public class SeriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
