using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_BTL.Models.User;
using Web_BTL.Models.User.Customer;
using Web_BTL.Repository;
using static System.Net.Mime.MediaTypeNames;

namespace Web_BTL.Controllers
{
    public class UserController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _environment;
        public UserController(DataContext dataContext, IWebHostEnvironment environment)
        {
            _dataContext = dataContext;
            _environment = environment;
        }
        [HttpGet]
        public async Task<IActionResult> UserInformation()
        {
            // lấy thông tin người dùng từ s
            string email = HttpContext.Session.GetString("LogIn Session");
            if (email != null)
            {
                var admin = await _dataContext.Admins.FirstOrDefaultAsync(c => c.UserEmail == email);
                if (admin != null) return View(admin);
                var customer = await _dataContext.Customers.FirstOrDefaultAsync(c => c.UserEmail == email);
                if (customer != null) return View(customer);
                else
                {
                    Console.WriteLine("Khong co du lieu nguoi dung");
                    return RedirectToAction(nameof(SignIn), "Account");
                }
            }
            Console.WriteLine("Khong co email");
            return RedirectToAction(nameof(SignIn), "Account");
        }
        public async Task<IActionResult> Image()
        {
            string email = HttpContext.Session.GetString("LogIn Session");
            var admin = await _dataContext.Admins.FirstOrDefaultAsync(c => c.UserEmail == email);
            if (admin != null) return PartialView("SaveImage", admin);
            var customer = await _dataContext.
                Customers.
                FirstOrDefaultAsync(c => c.UserEmail == email);
            return PartialView("SaveImage", customer);
        }
        [HttpPost]
        public async Task<IActionResult> SaveImage(IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                Console.WriteLine("Da lay duoc anh vao post SaveImage");
                string email = HttpContext.Session.GetString("LogIn Session");
                var admin = await _dataContext.Admins.FirstOrDefaultAsync(a => a.UserEmail == email);
                if (admin != null) admin.UserImagePath = await SaveImageAsync(admin, image);
                else
                {
                    var customer = await _dataContext.Customers.FirstOrDefaultAsync(c => c.UserEmail == email);
                    if (customer != null) customer.UserImagePath = await SaveImageAsync(customer, image);
                }
                await _dataContext.SaveChangesAsync();
            }
            else
                Console.WriteLine("Khong lay duoc anh ve");
            return RedirectToAction(nameof(UserInformation));
        }
        private async Task<string> SaveImageAsync(UserModel model, IFormFile image)
        {
            // Đường dẫn tới thư mục chứa ảnh trong wwwroot
            string uploadsFolder = Path.Combine(_environment.WebRootPath, "images/users");
            // Lấy ảnh cũ từ cơ sở dữ liệu
            string oldImagePath = Path.Combine(uploadsFolder, model.UserImagePath);
            // Kiểm tra nếu file ảnh cũ tồn tại thì xóa nó đi
            if (System.IO.File.Exists(oldImagePath) && model.UserImagePath != "default.jpg")
                System.IO.File.Delete(oldImagePath);
            // Lấy phần mở rộng của file (ví dụ .jpg, .png)
            string fileExtension = Path.GetExtension(image.FileName);
            // Tạo tên file tùy chỉnh, ở đây đang lấy theo tên đăng nhập
            string name = model.UserLogin;
            string fileName = $"{name}{fileExtension}";
            // Đường dẫn file mới
            string newFilePath = Path.Combine(uploadsFolder, fileName);
            // Lưu ảnh mới vào thư mục với tên mới
            using (var fileStream = new FileStream(newFilePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            // Cập nhật đường dẫn ảnh mới vào cơ sở dữ liệu
            return fileName;
        }
        public async Task<IActionResult> Name()
        {
            string email = HttpContext.Session.GetString("LogIn Session");
            var admin = await _dataContext.Admins.FirstOrDefaultAsync(c => c.UserEmail == email);
            if (admin != null) return PartialView("EditName", admin);
            var customer = await _dataContext.Customers.FirstOrDefaultAsync(c => c.UserEmail == email);
            return PartialView("EditName", customer);
        }
        [HttpPost]
        public async Task<IActionResult> EditName(string name)
        {
            string email = HttpContext.Session.GetString("LogIn Session");
            var admin = await _dataContext.Admins.FirstOrDefaultAsync(c => c.UserEmail == email);
            if (admin != null) admin.UserName = name;
            else
            {
                var customer = await _dataContext.Customers.FirstOrDefaultAsync(c => c.UserEmail == email);
                customer.UserName = name;
            }
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(UserInformation));
        }
        public async Task<IActionResult> rPassword()
        {
            string email = HttpContext.Session.GetString("LogIn Session");
            var admin = await _dataContext.Admins.FirstOrDefaultAsync(c => c.UserEmail == email);
            if (admin != null) return PartialView("EditPassword", admin);
            var customer = await _dataContext.Customers.FirstOrDefaultAsync(c => c.UserEmail == email);
            return PartialView("EditPassword", customer);
        }
        [HttpPost]
        public async Task<IActionResult> EditPassword(string oPassword, string nPassword, string cPassword)
        {
            string email = HttpContext.Session.GetString("LogIn Session");
            var admin = await _dataContext.Admins.FirstOrDefaultAsync(c => c.UserEmail == email);
            var customer = await _dataContext.Customers.FirstOrDefaultAsync(c => c.UserEmail == HttpContext.Session.GetString("LogIn Session"));
            if (admin != null) CheckPassword(admin, oPassword, nPassword, cPassword);
            else CheckPassword(customer, oPassword, nPassword, cPassword);
            if (!ModelState.IsValid)
                return View("UserInformation", customer);
            else
            {
                if (admin != null) admin.LoginPassword = nPassword;
                else customer.LoginPassword = nPassword;
                await _dataContext.SaveChangesAsync();
            }
            return RedirectToAction(nameof(UserInformation));
        }
        private void CheckPassword(UserModel model, string oPassword, string nPassword, string cPassword)
        {
            if (oPassword != model.LoginPassword)
                ModelState.AddModelError("ErrorPassword", "Mật khẩu cũ không đúng");
            else if (oPassword == nPassword)
                ModelState.AddModelError("ErrorPassword", "Không được dùng lại mật khẩu cũ");
            else if (nPassword != cPassword)
                ModelState.AddModelError("ErrorPassword", "Mật khẩu điền lại không đúng");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
