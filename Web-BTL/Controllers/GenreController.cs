using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_BTL.Repository;

namespace Web_BTL.Controllers
{
    public class GenreController : Controller
    {
        private DataContext db;
        public GenreController(DataContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            var media = db.Medias.Include(m => m.Genres).ToList();
            return View(media);
        }
    }
}
