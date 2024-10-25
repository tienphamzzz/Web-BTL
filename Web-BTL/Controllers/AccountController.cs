using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_BTL.Models;
using Web_BTL.Models.ListMedia.Watch;
using Web_BTL.Models.User.Customer;
using Web_BTL.Repository;
using Web_BTL.Services.Cookie;
using Web_BTL.Services.EmailServices;

namespace Web_BTL.Controllers
{
    public class AccountController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly SendEmail _sendEmail;
        private readonly CookieService _cookieService;
        private readonly string emailSignIn = "SignIn", emailSignUp = "SignUp", emailRecoverPassword = "RPass", OTP = "OTP";
        public AccountController(DataContext dataContext, SendEmail sendEmail, CookieService cookieService)
        {
            _dataContext = dataContext;
            _sendEmail = sendEmail;
            _cookieService = cookieService;
        }
        [HttpGet]
        public IActionResult SignIn() // Get Sign In khi người dùng trỏ đến linh Sign In
        {
            Console.WriteLine("Day la get SignIn");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(LogInModel model)
        {
            Console.WriteLine("Day la post SignIn");
            Console.WriteLine("Ten tai khoan: " + model.LogInName + " - Mat khau: " + model.Password);
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
                        Console.WriteLine("Da dang nhap bang tai khoan Customer");
                        HttpContext.Session.SetString("LogIn Session", customer.UserEmail);
                        return RedirectToAction(nameof(Index), "Home");
                    }
                    else
                    {
                        Console.WriteLine("Dang nhap sai mat khau hoac tai khoan");
                        return View(model);
                    }
                }
                Console.WriteLine("Da dang nhap bang tai khoan Admin");
                HttpContext.Session.SetString("LogIn Session", admin.UserEmail);
                HttpContext.Session.SetString("User", "Admin");
                return RedirectToAction(nameof(Index), "Home");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            Console.WriteLine("Day la get SignUp");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(CustomerModel model)
        {
            Console.WriteLine("Day la post SignUp");
            if (ModelState.IsValid)
            {
                if(await _dataContext.Admins.FirstOrDefaultAsync(a => a.UserEmail == model.UserEmail || a.UserLogin == model.UserLogin) != null || 
                   await _dataContext.Customers.FirstOrDefaultAsync(c => c.UserEmail == model.UserEmail || c.UserLogin == model.UserLogin) != null)
                {
                    ModelState.AddModelError(string.Empty, "Email hoặc tên đăng nhập đã tồn tại");
                    return View(model);
                }

                _cookieService.SetCookie(emailSignUp, model.UserEmail, 60, Response);
                _cookieService.SetCookie("Password", model.LoginPassword, 60, Response);
                _cookieService.SetCookie("LogInName", model.UserLogin, 60, Response);
                return RedirectToAction(nameof(SendOtp));
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult RecoverPassword()
        {
            Console.WriteLine("Day la get RecoverPassword");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RecoverPassword(LogInModel model)
        {
            Console.WriteLine("Day la post RecoverPassword");
            
            if (ModelState.IsValid)
            {
                var customer = await _dataContext.Customers.FirstOrDefaultAsync(c => c.UserName == model.LogInName || c.UserEmail == model.LogInName);
                if (customer != null)
                {
                    _cookieService.SetCookie(emailRecoverPassword, model.LogInName, 60, Response);
                    _cookieService.SetCookie("RecoverPassword", model.Password, 60, Response);
                    return RedirectToAction(nameof(SendOtp));
                }
                else return View(model);
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult SendOtp()
        {
            Console.WriteLine("Day la get SendOtp");
            string otpCode = _sendEmail.GenerateOTP();
            string _to = "";
            //if (_cookieService.GetCookie(emailSignIn, Request) != null)
            //    _to = _cookieService.GetCookie(emailSignIn, Request);
            if (_cookieService.GetCookie(emailSignUp, Request) != null)
                _to = _cookieService.GetCookie(emailSignUp, Request);
            else if(_cookieService.GetCookie(emailRecoverPassword, Request) != null)
                _to = _cookieService.GetCookie(emailRecoverPassword, Request);
            if (_to != "")
            {
                _sendEmail.SendOTPEmail(_to, otpCode);
                //HttpContext.Session.SetString("otpCode", otpCode); // sử dụng session toàn cục
                _cookieService.SetCookie(OTP, otpCode, 1, Response); 
                return View();
            }
            return RedirectToAction(nameof(SignIn));
        }
        [HttpPost]
        public async Task<IActionResult> SendOtp(string Otp)
        {
            Console.WriteLine("Day la post SendOtp");
            //if(Otp == HttpContext.Session.GetString("otpCode")) // lấy giá trị của session toàn cục
            if(Otp == Request.Cookies["OTP"])
            {
                Console.WriteLine("Ban da nhap dung ma OTP");
                //if (Request.Cookies[emailSignIn] != null)
                //{
                //    HttpContext.Session.SetString("LogIn Session", _cookieService.GetCookie(emailSignIn, Request));
                //    //deleteCookie(emailSignIn);
                //    _cookieService.DeleteCookie(emailSignIn, Response);
                //    return RedirectToAction(nameof(Index), "Home");
                //}
                if(Request.Cookies[emailSignUp] != null)
                {
                    var customer = new CustomerModel
                    {
                        UserName = _cookieService.GetCookie("LogInName", Request),
                        UserLogin = _cookieService.GetCookie("LogInName", Request),
                        UserEmail = _cookieService.GetCookie(emailSignUp, Request),
                        LoginPassword = _cookieService.GetCookie("Password", Request),
                        UserImagePath = "default.png",
                        UserState = true,
                        _ServicePackage = ServicePackage.Basic,
                        UserCreateDate = DateTime.Now
                    };
                    _dataContext.Customers.Add(customer); // thêm customer mới vào database
                    var watchList = new WatchListModel
                    {
                        CustomerId = customer.CustomerId
                    };
                    _dataContext.WatchLists.Add(watchList); // thêm watchList mới vào database
                    await _dataContext.SaveChangesAsync();
                    customer.WatchListId = watchList.WatchListId;
                    await _dataContext.SaveChangesAsync();
                    _cookieService.DeleteCookie(emailSignUp, Response);
                    return RedirectToAction(nameof(SignIn));
                }
                if (Request.Cookies[emailRecoverPassword] != null)
                {
                    string userName = _cookieService.GetCookie(emailRecoverPassword, Request);
                    var customer = await _dataContext.Customers.FirstOrDefaultAsync(c => c.UserName == userName || c.UserEmail == userName);
                    if(customer != null)
                        customer.LoginPassword = _cookieService.GetCookie("RecoverPassword", Request);
                    await _dataContext.SaveChangesAsync();
                    _cookieService.DeleteCookie("RecoverPassword", Response);
                    _cookieService.DeleteCookie(emailRecoverPassword, Response);
                    return RedirectToAction(nameof(SignIn));
                }
            }
            Console.WriteLine("Ban da nhap sai otp");
            return View();
        }
        //[HttpPost]        
        //public async Task<IActionResult> UserInformation([Bind("UserName, LoginPassword")]CustomerModel customer)
        //{
        //    string email = HttpContext.Session.GetString("LogIn Session");
        //    var cus = await _dataContext.Customers.FirstOrDefaultAsync(c => c.UserEmail == email);
        //    return View("UserInformation", cus);
        //}
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
