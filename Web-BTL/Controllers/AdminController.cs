using MediaToolkit;
using MediaToolkit.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_BTL.Models.Medias;
using Web_BTL.Models.User.Customer;
using Web_BTL.Repository;
using static System.Net.Mime.MediaTypeNames;

namespace Web_BTL.Controllers
{
    public class AdminController : Controller
    {
        private readonly DataContext _datacontext;
        private readonly IWebHostEnvironment _environment;
        public AdminController(DataContext datacontext, IWebHostEnvironment environment = null)
        {
            _datacontext = datacontext;
            _environment = environment;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("LogIn Session") == null)
                return RedirectToAction(nameof(SignIn), "Account");
            var medias = _datacontext.Medias.ToList();
            return View(medias);
        }
        [HttpGet]
        public IActionResult AddMedia()
        {

            ViewBag.AllPackage = Enum.GetValues(typeof(ServicePackage)).Cast<ServicePackage>().ToList();
            //ViewBag.AllPackage = Enum.GetValues(typeof(ServicePackage)).Cast<ServicePackage>().ToList();
            var genres = _datacontext.Genres.ToList();
            ViewBag.AllGenres = new List<SelectListItem>();
            ViewBag.AllGenres.AddRange(genres.Select(g => new SelectListItem
            {
                Text = g.Type,
                Value = g.GenreId.ToString()
            }));
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddMedia(MediaModel media, IFormFile Image, IFormFile Video, List<int> SelectedGenreId)
        {
            if (ModelState.IsValid)
            {
                if (Image != null && Image.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_environment.WebRootPath, "images/medias");
                    string fileExtension = Path.GetExtension(Image.FileName);
                    string name = media.MediaName;
                    string fileName = $"{name}{fileExtension}";
                    string newFilePath = Path.Combine(uploadsFolder, fileName);
                    try
                    {
                        using (var fileStream = new FileStream(newFilePath, FileMode.Create))
                        {
                            await Image.CopyToAsync(fileStream);
                        }
                        media.MediaImagePath = fileName;
                    }
                    catch { }

                }
                if (Video != null && Video.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_environment.WebRootPath, "videos");
                    string fileExtension = Path.GetExtension(Video.FileName);
                    string name = media.MediaName + media.MediaQuality;
                    string fileName = $"{name}{fileExtension}";
                    string newFilePath = Path.Combine(uploadsFolder, fileName);
                    try
                    {
                        using (var fileStream = new FileStream(newFilePath, FileMode.Create))
                        {
                            await Video.CopyToAsync(fileStream);
                        }
                        media.MediaUrl = fileName;
                        //var templeFile = Path.GetTempFileName();
                        //var inputFile = new MediaFile { Filename = templeFile };
                        //using (var engine = new Engine())
                        //{
                        //    engine.GetMetadata(inputFile);
                        //}
                        //var durationInSeconds = inputFile.Metadata.Duration.TotalSeconds;
                        //var durationInMinutes = durationInSeconds / 60;
                        //System.IO.File.Delete(templeFile);
                        //media.MediaDuration = TimeSpan.FromMinutes(durationInMinutes);
                    }
                    catch(Exception ex) {
                        Console.WriteLine($"Khong up duoc video: {ex.Message}");
                    }
                }
                if (SelectedGenreId.Count == 0)
                {
                    Console.WriteLine("Lỗi ở SelectedGenreId.Count == 0");
                    ViewBag.AllPackage = Enum.GetValues(typeof(ServicePackage)).Cast<ServicePackage>().ToList();
                    media.Genres = new List<GenreModel>();
                    media.Genres = await _datacontext.Genres.ToListAsync();
                    return View(media);
                }
                else
                {
                    //string[] cat = media.SelectedGenreId.Split(',');
                    foreach (var item in SelectedGenreId)
                    {
                        var g = await _datacontext.Genres.FirstOrDefaultAsync(g => g.GenreId == item);
                        if (g != null) media.Genres.Add(g);
                    }
                }
                await _datacontext.Medias.AddAsync(media);
                await _datacontext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                // Bạn có thể log hoặc kiểm tra lỗi ở đây
                foreach (var error in errors)
                {
                    Console.WriteLine(error); // hoặc ghi log
                }
            }
            ViewBag.AllPackage = Enum.GetValues(typeof(ServicePackage)).Cast<ServicePackage>().ToList();
            ViewBag.AllGenres = new List<SelectListItem>();
            var genres = _datacontext.Genres.ToList();
            ViewBag.AllGenres.AddRange(genres.Select(g => new SelectListItem // tạo list item để lựa chon genre cho media
            {
                Text = g.Type,
                Value = g.GenreId.ToString()
            }));
            media.Genres = await _datacontext.Genres.ToListAsync();
            return View(media);
        }
    }
}
