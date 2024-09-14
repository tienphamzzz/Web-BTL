using Microsoft.AspNetCore.Mvc;

namespace Web_BTL.Controllers
{
    public class MoviesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
