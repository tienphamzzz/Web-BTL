using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_BTL.Models.Medias;
using Web_BTL.Repository;

namespace Web_BTL.Controllers
{
    public class MovieController : Controller
    {
        private readonly DataContext db;
        public MovieController(DataContext context)
        {
            db = context;
        }
        public IActionResult Index(int movieId)
        {
            var movie = db.Medias.FirstOrDefault(m => m.MediaId == movieId);

            if (movie == null)
            {
                return NotFound(); // If the movie does not exist, return a 404 error
            }

            // Pass the movie object to the view
            return View(movie);
        }
        public IActionResult watching(int movieId)
            {
                var movie = db.Medias.FirstOrDefault(m => m.MediaId == movieId);

                if (movie == null)
                {
                    return NotFound(); // If the movie does not exist, return a 404 error
                }

                // Pass the movie object to the view
                return View(movie);
            }
        }
    
}
