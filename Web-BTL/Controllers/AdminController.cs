using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_BTL.Models.Actors;
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
        // tạo các ViewBag cho các action sử dụng (hầu như đều dùng) trong controller Admin
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
            Console.WriteLine("day la get edit media");
            var actors = _datacontext.Actors.ToList();
            ViewBag.AllActors = new List<SelectListItem>();
            ViewBag.AllActors.AddRange(actors.Select(a => new SelectListItem
            {
                Text = a.ActorName,
                Value = a.ActorID.ToString()
            }));
            if (mid == null)
                return RedirectToAction(nameof(Index));
            var media = _datacontext.Medias.FirstOrDefault(m => m.MediaId == mid);
            CreateViewBag();
            return View(media);
        }
        // lấy dữ liệu media đã sửa về
        [HttpPost]
        public async Task<IActionResult> EditMedia(int mid, MediaModel model, 
            IFormFile? image, IFormFile? video, List<int> SelectedActorId, List<int> SelectedActorMain)
        {
            Console.WriteLine("Day la post Edit media");
            Console.WriteLine("day la id" + mid);
            Console.WriteLine("day la id cua media" + model.MediaId);
            if (mid != model.MediaId) return NotFound();
            var media = await _datacontext.Medias.FirstOrDefaultAsync(m => m.MediaId == mid);
            if (media == null || model == null) return RedirectToAction(nameof(Index));
            if (ModelState.IsValid)
            {
                try
                {
                    _datacontext.Update(media);
                    await _datacontext.SaveChangesAsync();
                }catch (DbUpdateConcurrencyException)
                {
                    if (!mediaExists(media.MediaId))
                        return NotFound();
                    else throw;
                }
            }
            if (image != null && image.Length > 0)
                media.MediaImagePath = await _save.SaveImageAsync(_environment, "images/medias", "", media.MediaName, image);
            if (video != null && video.Length > 0)
            {
                var resule = await _save.SaveVideoAsync(_environment, "videos", "", media.MediaName + media.MediaQuality, video, true);
                media.MediaImagePath = resule.videoName;
                media.MediaDuration = resule.duration;
            }
            if (SelectedActorId.Count > 0)
            {
                
                foreach (var id in SelectedActorId)
                {
                    await _datacontext.Actor_Medias.AddAsync(new Actor_MediaModel
                    {
                        MediaId = mid,
                        Media = media,
                        ActorId = id,
                        Actor = await _datacontext.Actors.FindAsync(id)
                    });
                }
                await _datacontext.SaveChangesAsync();
                foreach (var main in SelectedActorMain)
                {
                    var a = await _datacontext.Actor_Medias.FindAsync(main);
                    a.IsMain = true;
                }
                await _datacontext.SaveChangesAsync();
            }else Console.WriteLine("Loi o phan add Actor");
            await _datacontext.SaveChangesAsync();
            ViewBag.AllActors = new List<SelectListItem>();
            var actors = _datacontext.Actors.ToList();
            ViewBag.AllActors.AddRange(actors.Select(a => new SelectListItem
            {
                Text = a.ActorName,
                Value = a.ActorID.ToString()
            }));
            return View(model);
        }
        private bool mediaExists(int id)
        {
            return (_datacontext.Medias?.Any(e => e.MediaId == id)).GetValueOrDefault();
        }
        // xoá media
        [HttpPost]
        public async Task<IActionResult> DeleteMedia(int? mid, bool YesNo = false)
        {
            Console.WriteLine("Day la delete media");
            Console.WriteLine("mid = " + mid);
            if (mid != null && YesNo)
            {
                var media = await _datacontext.Medias.FirstAsync(m => m.MediaId == mid);
                _save.DeleteFile(_environment, "images/medias", media.MediaImagePath); // xoá ảnh của media trong wwwroot
                _save.DeleteFile(_environment, "videos", media.MediaUrl); // xoá media trong wwwroot
                _datacontext.Medias.Remove(media);
                await _datacontext.SaveChangesAsync();
                Console.WriteLine("da xoa thanh cong");
            }
            else Console.WriteLine("Không xoá thành công");
            return RedirectToAction(nameof(Index));
        }
        public IActionResult ListGenre()
        {
            var genres = _datacontext.Genres.ToList();
            return View(genres);
        }
        [HttpGet]
        public IActionResult AddGenre()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddGenre(GenreModel genre)
        {
            if (genre == null) return View(genre);
            await _datacontext.Genres.AddAsync(genre);
            await _datacontext.SaveChangesAsync();
            return RedirectToAction(nameof(ListGenre));
        }
        [HttpPost]
        public IActionResult EditGenre([FromBody]GenreModel genre)
        {
            Console.WriteLine($"Day la EditGenre - {genre.GenreId} - {genre.Type}" );
            var model = _datacontext.Genres.FirstOrDefault(g => g.GenreId == genre.GenreId);
            if (model != null)
            {
                model.Type = genre.Type;
                _datacontext.SaveChanges();
                Console.WriteLine("Da cap nhat thanh cong");
                return Json(new { success = true });
            }
            Console.WriteLine("Da cap nhat that bai");
            return Json(new { success = false});
        }
        [HttpPost]
        public async Task<IActionResult> DeleteGenre(int? gid, bool YesNo = false)
        {
            if (gid != null && YesNo)
            {
                var genre = _datacontext.Genres.FirstOrDefault(g => g.GenreId == gid);
                _datacontext.Genres.Remove(genre);
                await _datacontext.SaveChangesAsync();
            }
            return RedirectToAction(nameof(ListGenre));
        }
        public IActionResult ListActor()
        {
            var actors = _datacontext.Actors.ToList();
            return View(actors);
        }
        [HttpGet]
        public IActionResult AddActor()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddActor(ActorModel actor)
        {
            if (actor == null) return View(actor);
            await _datacontext.Actors.AddAsync(actor);
            await _datacontext.SaveChangesAsync();
            return RedirectToAction(nameof(ListActor));
        }
        [HttpPost]
        public IActionResult EditActorName([FromBody]ActorModel actor)
        {
            var model = _datacontext.Actors.Find(actor.ActorID);
            if (model != null)
            {
                model.ActorName = actor.ActorName;
                //model.AcctorDate = actor.AcctorDate;
                _datacontext.SaveChanges();
                return Json(new {success = true});
            }
            return Json(new {success = false});
        }
        [HttpPost]
        public IActionResult EditActorDate([FromBody] ActorModel actor)
        {
            //var model = _datacontext.Actors.Find(actor.ActorID);
            Console.WriteLine($"Day la EditActor - {actor.ActorID}");
            if (actor.ActorID == null) return Json(new { success = false });
            var model = _datacontext.Actors.FirstOrDefault(a => a.ActorID == actor.ActorID);
            if (model != null)
            {
                model.AcctorDate = actor.AcctorDate;
                _datacontext.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteActor(int? aid, bool YesNo = false)
        {
            if (aid != null && YesNo)
            {
                var actor = await _datacontext.Actors.FirstOrDefaultAsync(a => a.ActorID == aid);
                _datacontext.Actors.Remove(actor);
                await _datacontext.SaveChangesAsync();
            }
            return RedirectToAction(nameof(ListActor));
        }
    }
}
