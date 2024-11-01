using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_BTL.Models;
using Web_BTL.Models.ListMedia.Watch;
using Web_BTL.Models.User.Customer;
using Web_BTL.Repository;
using Web_BTL.Services.Cookie;
using Web_BTL.Services.EmailServices;
using Microsoft.Extensions.Caching.Memory;

namespace Web_BTL.Controllers
{
    public class AccountController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly SendEmail _sendEmail;
        private readonly CookieService _cookieService;
        private readonly string OTP = "OTP";
        private readonly IMemoryCache _cache;
        public AccountController(DataContext dataContext, SendEmail sendEmail, CookieService cookieService, IMemoryCache cache)
        {
            _dataContext = dataContext;
            _sendEmail = sendEmail;
            _cookieService = cookieService;
            _cache = cache;
        }
        
        [HttpGet]
        public IActionResult SignIn() // Get Sign In khi người dùng trỏ đến linh Sign In
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(LogInModel model)
        {
            if (ModelState.IsValid)
            {
                var admin = await _dataContext.Admins.
                    FirstOrDefaultAsync(a => ((a.UserEmail == model.LogInName || a.UserLogin == model.LogInName) && a.LoginPassword == model.Password) && a.UserState == true);
                if (admin == null)
                {
                    var customer = await _dataContext.Customers.
                        FirstOrDefaultAsync(c => ((c.UserEmail == model.LogInName || c.UserLogin == model.LogInName) && c.LoginPassword == model.Password) && c.UserState == true);
                    if (customer != null)
                    {
                        HttpContext.Session.SetString("LogIn Session", customer.UserEmail);
                        return RedirectToAction(nameof(Index), "Home");
                    }
                    else return View(model);
                }
                HttpContext.Session.SetString("LogIn Session", admin.UserEmail);
                HttpContext.Session.SetString("Admin", admin.Role.ToString());
                return RedirectToAction(nameof(Index), "Home");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(CustomerModel model)
        {
            if (ModelState.IsValid)
            {
                var admin = await _dataContext.Admins.FirstOrDefaultAsync(a => a.UserEmail == model.UserEmail || a.UserLogin == model.UserLogin);
                var customer = await _dataContext.Customers.FirstOrDefaultAsync(c => c.UserEmail == model.UserEmail || c.UserLogin == model.UserLogin);
                if (customer == null || admin == null)
                {
                    model.UserImagePath = "default.jpg";
                    model.UserState = true;
                    model._ServicePackage = ServicePackage.Basic;
                    model.UserCreateDate = DateTime.Now;
                    return SendOtp(model, 1);
                }
                ModelState.AddModelError(string.Empty, "Email hoặc tên đăng nhập đã tồn tại");
                return View(model);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult RecoverPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RecoverPassword(LogInModel model)
        {
            if (ModelState.IsValid)
            {
                var cus = await _dataContext.Customers.FirstOrDefaultAsync(c => c.UserLogin == model.LogInName || c.UserEmail == model.LogInName);
                if (cus != null)
                {
                    cus.LoginPassword = model.Password;
                    Console.WriteLine("ban dang di dung huong ben trong post Recover Password");
                    return SendOtp(cus, 2);
                }
                else
                {
                    Console.WriteLine("Ban dang khong tim thay customer");
                    return View(model);
                }
            }
            Console.WriteLine("Ban da nhap thieu thong tin gi do");
            return View(model);
        }
        [HttpGet]
        public IActionResult SendOtp(CustomerModel cus, int CheckMethod)
        {
            string otpCode = _sendEmail.GenerateOTP();
            string _to = cus.UserEmail;
            _sendEmail.SendOTPEmail(_to, otpCode);
            _cookieService.SetCookie(OTP, otpCode, 1, Response);
            HttpContext.Session.SetString(OTP, otpCode);
            HttpContext.Session.SetString("email", cus.UserEmail);
            HttpContext.Session.SetString("method", CheckMethod.ToString());
            _cache.Set("customer", cus, TimeSpan.FromMinutes(5));
            Console.WriteLine($"Day la ma Otp: {otpCode}");
            return View("SendOtp");
        }
        [HttpPost]
        public async Task<IActionResult> SendOtp(string Otp)
        {
            if (_cache.TryGetValue("customer", out CustomerModel customer))
            {
                if (Otp == HttpContext.Session.GetString(OTP))
                {
                    string method = HttpContext.Session.GetString("method");
                    if (method != null)
                    {
                        if (method == "1")
                        {
                            
                            _dataContext.Customers.Add(customer); // thêm customer mới vào database
                            await _dataContext.SaveChangesAsync();
                            var watchList = new WatchListModel
                            {
                                CustomerId = customer.CustomerId
                            };
                            _dataContext.WatchLists.Add(watchList); // thêm watchList mới vào database
                            await _dataContext.SaveChangesAsync();
                            customer.WatchListId = watchList.WatchListId;
                            await _dataContext.SaveChangesAsync();

                        }
                        else if (method == "2")
                        {
                            var cus = await _dataContext.Customers.FindAsync(customer.CustomerId);
                            cus.LoginPassword = customer.LoginPassword;
                            await _dataContext.SaveChangesAsync();
                        }
                    }
                    HttpContext.Session.Clear();
                    _cache.Remove("customer");
                    return RedirectToAction(nameof(SignIn));
                }
                else ModelState.AddModelError(string.Empty, "OTP không khớp. Vui lòng kiểm tra lại.");
            }
            return View();
        }
        [HttpPost]
        public IActionResult ResendOtp()
        {
            Console.WriteLine("Day la post ResenOtp");
            if (_cache.TryGetValue("customer", out CustomerModel customer))
            {
                string otp = _sendEmail.GenerateOTP();
                string _to = customer.UserEmail;
                _sendEmail.SendOTPEmail(_to, otp);
                HttpContext.Session.SetString(OTP, otp);
                Console.WriteLine($"Day la ma otp: {otp}");
                TempData.Keep("customer");
                return Json(new {success = true, message = "Đã gửi otp thành công"});
            }
            return Json(new { success = false, message = "Không thê gửi mã otp, vui lòng thử lại" });
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
