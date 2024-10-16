﻿using Microsoft.AspNetCore.Mvc;
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
        public GenreController(DataContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            var media = db.Medias.Include(m => m.Genres).ToList();
            // Lấy danh sách thể loại từ cơ sở dữ liệu
            var genres = db.Genres.ToList();

            // Gán danh sách thể loại vào ViewBag
            ViewBag.Genre = genres;
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

        public IActionResult MoviesFilter(int? mid)
        {
            int PageSize = 8;
            var medias = (IQueryable<MediaModel>)db.Medias;

            if (mid != null)
            {
                medias = (IQueryable<MediaModel>)db.Medias
                    .Where(m => m.Genres.Any(g => g.GenreId == mid));

                ViewBag.mid = mid;
            }

            var result = medias.Take(PageSize).ToList();

            return PartialView("_MediaPartial", result);
        }

    }
}
