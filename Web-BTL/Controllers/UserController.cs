using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_BTL.Models.User;
using Web_BTL.Models.User.Customer;
using Web_BTL.Repository;

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
            if(email != null)
            {
                var _userModel = await _dataContext.Customers.FirstOrDefaultAsync(c => c.UserEmail == email);
                if (_userModel != null) return View(_userModel);
                else
                {
                    Console.WriteLine("Khong co du lieu nguoi dung");
                    return RedirectToAction(nameof(SignIn), "Account");
                }
            }
            Console.WriteLine("Khong co email");
            return RedirectToAction(nameof(SignIn), "Account");
        }
        public async Task<IActionResult> Image(int uid)
        {
            var customer = await _dataContext.
                Customers.
                FirstOrDefaultAsync(c => c.UserEmail == HttpContext.Session.GetString("LogIn Session"));
            return PartialView("SaveImage", customer);
        }
        [HttpPost]
        public async Task<IActionResult> SaveImage(IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                Console.WriteLine("Da lay duoc anh vao post SaveImage");
                var customer = await _dataContext.
                    Customers.
                    FirstOrDefaultAsync(c => c.UserEmail == HttpContext.Session.GetString("LogIn Session"));
                // Đường dẫn tới thư mục chứa ảnh trong wwwroot
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "images/users");

                // Lấy ảnh cũ từ cơ sở dữ liệu
                string oldImagePath = Path.Combine(uploadsFolder, customer.UserImagePath);

                // Kiểm tra nếu file ảnh cũ tồn tại thì xóa nó đi
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }

                // Lấy phần mở rộng của file (ví dụ .jpg, .png)
                string fileExtension = Path.GetExtension(image.FileName);

                // Tạo tên file tùy chỉnh, ví dụ: UserName_yyyyMMdd_HHmmss.ext
                string userName = customer.UserLogin; // Hoặc lấy từ CustomerId hoặc UserLogin
                
                string customFileName = $"{userName}{fileExtension}";

                // Đường dẫn file mới
                string newFilePath = Path.Combine(uploadsFolder, customFileName);

                // Lưu ảnh mới vào thư mục với tên mới
                using (var fileStream = new FileStream(newFilePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                // Cập nhật đường dẫn ảnh mới vào cơ sở dữ liệu
                customer.UserImagePath = customFileName;
                await _dataContext.SaveChangesAsync();
            }
            else
                Console.WriteLine("Khong lay duoc anh ve");
            return RedirectToAction(nameof(UserInformation));
        }
        public async Task<IActionResult> Name()
        {
            var customer = await _dataContext.
                Customers.
                FirstOrDefaultAsync(c => c.UserEmail == HttpContext.Session.GetString("LogIn Session"));
            return PartialView("EditName", customer);
        }
        public async Task<IActionResult> EditName(string name)
        {
            var customer = await _dataContext.
                Customers.
                FirstOrDefaultAsync(c => c.UserEmail == HttpContext.Session.GetString("LogIn Session"));
            customer.UserName = name;
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(UserInformation));
        }
        public async Task<IActionResult> rPassword()
        {
            var customer = await _dataContext.
                Customers.
                FirstOrDefaultAsync(c => c.UserEmail == HttpContext.Session.GetString("LogIn Session"));
            return PartialView("EditPassword", customer);
        }
        [HttpPost]
        public async Task<IActionResult> EditPassword(string oPassword, string nPassword, string cPassword)
        {
            var customer = await _dataContext.
                Customers.
                FirstOrDefaultAsync(c => c.UserEmail == HttpContext.Session.GetString("LogIn Session"));
            if (oPassword != customer.LoginPassword)
                ModelState.AddModelError("", "Mật khẩu cũ không đúng");
            else if (oPassword == nPassword)
                ModelState.AddModelError("", "Không được dùng lại mật khẩu cũ");
            else if (nPassword != cPassword)
                ModelState.AddModelError("", "Mật khẩu điền lại không đúng");
            if (!ModelState.IsValid)
                return View("UserInformation", customer);
            else
            {
                customer.LoginPassword = nPassword;
                await _dataContext.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(UserInformation));
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
