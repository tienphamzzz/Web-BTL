using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Web_BTL.Models.Medias;
using Web_BTL.Repository;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace Web_BTL.Controllers
{
    public class GenreController : Controller
    {
        private DataContext db;
        private int PageSize = 16;
        public GenreController(DataContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            var media = db.Medias.Include(m => m.Genres).ToList();
            return View(media);
        }

        public IActionResult AllMedias(int? pageindex)
        {
            // Khởi tạo IQueryable chứa danh sách MediaModel từ cơ sở dữ liệu
            var medias = db.Medias.AsQueryable();

            // Xác định số trang hiện tại
            var page = pageindex ?? 1;

            // Định nghĩa số lượng items trên mỗi trang
            int pageSize = 8;  // Bạn có thể thay đổi giá trị PageSize tùy theo yêu cầu

            // Tính tổng số trang dựa trên số lượng items và pageSize
            int totalItems = medias.Count();
            int pageNumbers = (int)Math.Ceiling(totalItems / (double)pageSize);

            // Gán tổng số trang vào ViewBag để hiển thị trong View
            ViewBag.PageNumbers = pageNumbers;
            ViewBag.CurrentPage = page;

            // Lấy các item cho trang hiện tại, sử dụng Skip và Take để phân trang
            var result = medias
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Trả về View cùng với danh sách các MediaModel của trang hiện tại
            return View(result);
        }

    }
}
