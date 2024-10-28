using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_BTL.Models.Medias;
using Web_BTL.Models.User.Customer;
using Web_BTL.Repository;
using Web_BTL.Services.UploadFile;
using static System.Net.Mime.MediaTypeNames;

namespace Web_BTL.Controllers
{
    public class AdminController : Controller
    {
        private readonly DataContext _datacontext; // luồng đọc dữ liệu từ database
        private readonly IWebHostEnvironment _environment; // môi trường web
        private readonly SaveImageVideo _save; // service dùng để lưu ảnh và video
        public AdminController(DataContext datacontext, IWebHostEnvironment environment, SaveImageVideo save)
        {
            _datacontext = datacontext;
            _environment = environment;
            _save = save;
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
            string role = HttpContext.Session.GetString("Admin");
            if (role == null || role != Models.User.Admin.Role.SuperAdmin.ToString() && role != Models.User.Admin.Role.Movie_Management.ToString()) 
                return RedirectToAction("UserInformation", "User");
            CreateViewBag();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddMedia(MediaModel media, IFormFile image, IFormFile video, List<int> SelectedGenreId)
        {
            var email = HttpContext.Session.GetString("LogIn Session");
            string role = HttpContext.Session.GetString("Admin");
            if (role != Models.User.Admin.Role.SuperAdmin.ToString() 
                && role != Models.User.Admin.Role.Movie_Management.ToString())
                return RedirectToAction("UserInformation", "User");
            if (ModelState.IsValid)
            {
                if (email == null) return RedirectToAction("SignIn", "Account");
                var admin = await _datacontext.Admins.FirstOrDefaultAsync(a => a.UserEmail == email);
                if (image != null && image.Length > 0)
                    media.MediaImagePath = await _save.SaveImageAsync(_environment, "images/medias", "", media.MediaName, image);
                if (video != null && video.Length > 0)
                {
                    var resule = await _save.SaveVideoAsync(_environment, "videos", "", media.MediaName + media.MediaQuality, video, true);
                    media.MediaUrl = resule.videoName;
                    media.MediaDuration = resule.duration;
                    //var tempPath = Path.Combine(Path.GetTempPath(), Video.FileName);
                }
                if (SelectedGenreId.Count == 0)
                {
                    Console.WriteLine("Lỗi ở SelectedGenreId.Count == 0");
                    CreateViewBag();
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
                // Lấy lỗi
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage).ToList();
                foreach (var error in errors)
                    Console.WriteLine(error);// Kiểm tra lỗi trên console
            }
            CreateViewBag();
            return View(media);
        }
        private void CreateViewBag()
        {
            ViewBag.AllPackage = Enum.GetValues(typeof(ServicePackage)).Cast<ServicePackage>().ToList();
            ViewBag.AllGenres = new List<SelectListItem>();
            var genres = _datacontext.Genres.ToList();
            ViewBag.AllGenres.AddRange(genres.Select(g => new SelectListItem // tạo list item để lựa chon genre cho media
            {
                Text = g.Type,
                Value = g.GenreId.ToString()
            }));
        }
        // gọi đến sửa media
        [HttpGet]
        public IActionResult EditMedia(int? mid)
        {
            if (mid == null)
                return RedirectToAction(nameof(Index));
            var media = _datacontext.Medias.FirstOrDefault(m => m.MediaId == mid);
            CreateViewBag();
            return View(media);
        }
        // lấy dữ liệu media đã sửa về
        [HttpPost]
        public async Task<IActionResult> EditMedia(int mid, [Bind("MediaName,MediaDescription,MediaQuality,ReleaseDate,MediaAgeRating,package")] MediaModel? model, IFormFile? image, IFormFile? video)
        {
            var media = await _datacontext.Medias.FirstOrDefaultAsync(m => m.MediaId == mid);
            if (media == null || model == null) return RedirectToAction(nameof(Index));
            media.MediaName = model.MediaName;
            media.MediaDescription = model.MediaDescription;
            media.MediaQuality = model.MediaQuality;
            media.ReleaseDate = model.ReleaseDate;
            media.MediaAgeRating = model.MediaAgeRating;
            media.package = model.package;
            if (image != null && image.Length > 0)
                media.MediaImagePath = await _save.SaveImageAsync(_environment, "images/medias", "", media.MediaName, image);
            if (video != null && video.Length > 0)
            {
                var resule = await _save.SaveVideoAsync(_environment, "videos", "", media.MediaName + media.MediaQuality, video, true);
                media.MediaImagePath = resule.videoName;
                media.MediaDuration = resule.duration;
            }
            await _datacontext.SaveChangesAsync();
            return View(model);
        }
        // xoá media
        [HttpPost]
        public async Task<IActionResult> DeleteMedia(int? mid, bool YesNo)
        {
            Console.WriteLine("Day la delete media");
            Console.WriteLine("mid = " + mid);
            if (mid != null && YesNo)
            {
                var media = await _datacontext.Medias.FirstAsync(m => m.MediaId == mid);
                _save.DeleteFile(_environment, "images/medias", media.MediaImagePath);
                _save.DeleteFile(_environment, "videos", media.MediaUrl);
                _datacontext.Medias.Remove(media);
                await _datacontext.SaveChangesAsync();
                Console.WriteLine("da xoa thanh cong");
            }
            else Console.WriteLine("Không xoá thành công");
            return RedirectToAction(nameof(Index));
        }
    }
}
