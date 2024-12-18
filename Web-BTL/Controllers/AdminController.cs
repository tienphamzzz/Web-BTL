using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_BTL.Models.Actors;
using Web_BTL.Models.Medias;
using Web_BTL.Models.User.Customer;
using Web_BTL.Repository;
using Web_BTL.Services.UploadFile;

namespace Web_BTL.Controllers
{
    public class AdminController : Controller
    {
        private readonly DataContext _datacontext; // luồng đọc dữ liệu từ database
        private readonly IWebHostEnvironment _environment; // môi trường web
        private readonly SaveImageVideo _save; // service dùng để lưu ảnh và video
        public AdminController(DataContext datacontext, IWebHostEnvironment environment, SaveImageVideo save) // gán giá trị khởi tạo
        {
            _datacontext = datacontext;
            _environment = environment;
            _save = save;
        }
        public IActionResult Index()
        {
            var medias = _datacontext.Medias.ToList();
            return View(medias);
        }
        [HttpGet]
        public IActionResult AddMedia()
        {
            if (!checkRole(true, true)) return NotFound("Bạn không đủ quyền hạn");
            CreateViewBag();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddMedia(MediaModel media, IFormFile image, IFormFile banner, IFormFile video, List<int> SelectedGenreId)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.Length > 0)
                    media.MediaImagePath = await _save.SaveImageAsync(_environment, "images/medias", "", media.MediaName, image);
                if (banner != null && banner.Length > 0)
                    media.MediaBannerPath = await _save.SaveImageAsync(_environment, "images/banners", "", media.MediaName + "banner", banner);
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
        
        // gọi đến sửa media
        [HttpGet]
        public IActionResult EditMedia(int? mid)
        {
            if (!checkRole(true, true)) return NotFound("Bạn không đủ quyền hạn");
            if (mid == null)
                return RedirectToAction(nameof(Index));
            var media = _datacontext.Medias.FirstOrDefault(m => m.MediaId == mid);
            CreateViewBag();
            return View(media);
        }
        // lấy dữ liệu media đã sửa về
        [HttpPost]
        public async Task<IActionResult> EditMedia(int mid, MediaModel model, 
            IFormFile? image, IFormFile? banner, IFormFile? video, List<int> SelectedGenreId, List<int> SelectedActorId, List<int> SelectedActorMain)
        {
            if (mid != model.MediaId) return NotFound();
            var media = await _datacontext.Medias.FirstOrDefaultAsync(m => m.MediaId == mid);
            if (media == null || model == null) return RedirectToAction(nameof(Index));
            if (ModelState.IsValid)
            {
                media.MediaName = model.MediaName;
                media.MediaDescription = model.MediaDescription;
                media.MediaQuality = model.MediaQuality;
                media.ReleaseDate = model.ReleaseDate;
                media.MediaAgeRating = model.MediaAgeRating;
                media.package = model.package;
                await _datacontext.SaveChangesAsync();
                if (image != null && image.Length > 0)
                    media.MediaImagePath = await _save.SaveImageAsync(_environment, "images/medias", "", media.MediaName, image);
                if (banner != null && banner.Length > 0)
                    media.MediaBannerPath = await _save.SaveImageAsync(_environment, "images/banners", "", media.MediaName + "banner", banner);
                if (video != null && video.Length > 0)
                {
                    var resule = await _save.SaveVideoAsync(_environment, "videos", "", media.MediaName + media.MediaQuality, video, true);
                    media.MediaUrl = resule.videoName;
                    media.MediaDuration = resule.duration;
                }
                if (SelectedActorId.Count > 0)
                {
                    var ra = await _datacontext.Actor_Medias.Where(a => a.MediaId == mid).ToListAsync();
                    _datacontext.Actor_Medias.RemoveRange(ra);
                    foreach (var id in SelectedActorId)
                    {
                        var a = await _datacontext.Actor_Medias.FirstOrDefaultAsync(am => am.MediaId == model.MediaId && am.ActorId == id);
                        if (a == null)
                        {
                            await _datacontext.Actor_Medias.AddAsync(new Actor_MediaModel
                            {
                                MediaId = mid,
                                Media = media,
                                ActorId = id,
                                Actor = await _datacontext.Actors.FindAsync(id)
                            });
                        }
                    }
                    await _datacontext.SaveChangesAsync();
                    foreach (var main in SelectedActorMain)
                    {
                        var a = await _datacontext.Actor_Medias.FirstOrDefaultAsync(am => am.MediaId == model.MediaId && am.ActorId == main);
                        a.IsMain = true;
                    }
                    await _datacontext.SaveChangesAsync();
                }
                if (SelectedGenreId.Count > 0)
                {
                    await _datacontext.Database.ExecuteSqlRawAsync($"DELETE FROM Media_Genre WHERE MediaId = {media.MediaId}");
                    await _datacontext.SaveChangesAsync();
                    foreach (var item in SelectedGenreId)
                    {
                        var g = await _datacontext.Genres.FirstOrDefaultAsync(g => g.GenreId == item);
                        if (g != null) media.Genres.Add(g);
                    }
                }
                await _datacontext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            CreateViewBag();
            return View(model);
        }
        private bool mediaExists(int id)
        {
            return (_datacontext.Medias?.Any(e => e.MediaId == id)).GetValueOrDefault();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteMedia(int? mid, bool YesNo = false)
        {
            if (!checkRole(true, true)) return NotFound("Bạn không đủ quyền hạn");
            if (mid != null && YesNo)
            {
                var media = await _datacontext.Medias.FirstAsync(m => m.MediaId == mid);
                _save.DeleteFile(_environment, "images/medias", media.MediaImagePath); // xoá ảnh của media trong wwwroot
                _save.DeleteFile(_environment, "videos", media.MediaUrl); // xoá media trong wwwroot
                _datacontext.Medias.Remove(media);
                await _datacontext.SaveChangesAsync();
            }
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
            if (!checkRole(true, true)) return NotFound("Bạn không đủ quyền hạn");
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
            if (!checkRole(true, true)) return Json(new { success = false });
            var model = _datacontext.Genres.FirstOrDefault(g => g.GenreId == genre.GenreId);
            if (model != null)
            {
                model.Type = genre.Type;
                _datacontext.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false});
        }
        [HttpPost]
        public async Task<IActionResult> DeleteGenre(int? gid, bool YesNo = false)
        {
            if (!checkRole(true, true)) return NotFound("Bạn không đủ quyền hạn");
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
            if (!checkRole(true, true)) return NotFound("Bạn không đủ quyền hạn");
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
            if (!checkRole(true, true)) return Json(new { success = false });
            var model = _datacontext.Actors.Find(actor.ActorID);
            if (model != null)
            {
                model.ActorName = actor.ActorName;
                _datacontext.SaveChanges();
                return Json(new {success = true});
            }
            return Json(new {success = false});
        }
        [HttpPost]
        public IActionResult EditActorDate([FromBody] ActorModel actor)
        {
            if (!checkRole(true, true)) return Json(new { success = false });
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
            if (!checkRole(true, true)) return NotFound("Bạn không đủ quyền hạn");
            if (aid != null && YesNo)
            {
                var actor = await _datacontext.Actors.FirstOrDefaultAsync(a => a.ActorID == aid);
                _datacontext.Actors.Remove(actor);
                await _datacontext.SaveChangesAsync();
            }
            return RedirectToAction(nameof(ListActor));
        }
        public IActionResult ListCustomer()
        {
            var customers = _datacontext.Customers.ToList();
            return View(customers);
        }
        [HttpPost]
        public IActionResult ToggleUserState(int customerId)
        {
            // kiểm tra đây có phải tài khoản admin không và có đủ quyền chỉnh sửa không
            if (!checkRole(true, false, false, true)) Json(new { success = false });
            var customer = _datacontext.Customers.Find(customerId);
            Console.WriteLine("day la ToggleUserState");
            if (customer != null)
            {
                customer.UserState = !customer.UserState;
                _datacontext.SaveChanges();
                return Json(new { success = true, newState = customer.UserState });
            }
            return Json(new { success = false });
        }
        [HttpGet]
        public IActionResult LoadMediaList(string type, string id)
        {
            if (type == "package")
            {
                var e = ServicePackage.Basic;
                if (id == "Premium") e = ServicePackage.Premium;
                else if (id == "Vip") e = ServicePackage.Vip;
                var mediaList = _datacontext.Medias.Where(m => m.package == e).ToList();
                return PartialView("MediaTable", mediaList);
            }
            if (type == "genre")
            {
                int gid = int.Parse(id);
                var mediaList = _datacontext.Medias.Where(m => m.Genres.Any(g => g.GenreId == gid)).ToList();
                return PartialView("MediaTable", mediaList);
            }
            if (type == "all")
            {
                var mediaList = _datacontext.Medias.ToList();
                return PartialView("MediaTable", mediaList);
            }
            return NotFound();
        }
        [HttpGet]
        public IActionResult LoadCustomerList(string id)
        {

            if (id == null) return NotFound();
            if (id == "all")
            {
                var cus = _datacontext.Customers.ToList();
                return PartialView("CustomerTable", cus);
            }
            var e = ServicePackage.Basic;
            if (id == "Premium") e = ServicePackage.Premium;
            else if (id == "Vip") e = ServicePackage.Vip;
            var customer = _datacontext.Customers.Where(c => c._ServicePackage == e).ToList();
            return PartialView("CustomerTable", customer);
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
            var actors = _datacontext.Actors.ToList();
            ViewBag.AllActors = new List<SelectListItem>();
            ViewBag.AllActors.AddRange(actors.Select(a => new SelectListItem
            {
                Text = a.ActorName,
                Value = a.ActorID.ToString()
            }));
        }
        private bool checkRole(bool SuperAdmin = false, bool Movie_Management = false, bool CommentAndReview_Management = false, bool ServicePackage_Management = false)
        {
            string email = HttpContext.Session.GetString("LogIn Session");
            if (email == null) return false;
            string role = HttpContext.Session.GetString("Admin");
            if (SuperAdmin && role == Models.User.Admin.Role.SuperAdmin.ToString()) return true;
            if (Movie_Management && role == Models.User.Admin.Role.Movie_Management.ToString()) return true;
            if (CommentAndReview_Management && role == Models.User.Admin.Role.CommentAndReview_Management.ToString()) return true;
            if (ServicePackage_Management && role == Models.User.Admin.Role.ServicePackage_Management.ToString()) return true;
            return false;
        }
        
    }
}
