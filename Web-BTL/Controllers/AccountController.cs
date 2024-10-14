using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_BTL.Models;
using Web_BTL.Models.ListMedia.Watch;
using Web_BTL.Models.User.Customer;
using Web_BTL.Repository;
using Web_BTL.Services.CheckAction;
using Web_BTL.Services.EmailServices;

namespace Web_BTL.Controllers
{
    public class AccountController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly SendEmail _sendEmail;
        private readonly string emailSignIn = "SignIn", emailSignUp = "SignUp", emailRecoverPassword = "RPass", OTP = "OTP";
        public AccountController(DataContext dataContext, SendEmail sendEmail)
        {
            _dataContext = dataContext;
            _sendEmail = sendEmail;
        }
        private void Cookie(string nameCookie, string valueCookie, int timeLimit = 360)
        {
            CookieOptions options = new CookieOptions // khởi tạo cookie
            {
                Expires = DateTime.Now.AddMinutes(timeLimit), // đặt time limit
                Secure = true, // chỉ truyền qua https
                HttpOnly = true, // chỉ có thể lấy dữ liệu bên server
            };
            Response.Cookies.Append(nameCookie, valueCookie, options); // tạo 1 cookie với name = nameCookie và value = valueCookie
        }
        private void deleteCookie(string nameCookie) { Response.Cookies.Delete(nameCookie); } // xoá cookie với name = nameCookie
        private string valueCookie(string nameCookie) { return Request.Cookies[nameCookie]; } // lấy giá trị cookie có name = nameCookie
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
                        EmailAddress.email = customer.UserEmail;
                        
                        Cookie(emailSignIn, customer.UserEmail, 60);
                        return RedirectToAction(nameof(SendOtp));
                    }
                    else
                    {
                        Console.WriteLine("Dang nhap sai mat khau hoac tai khoan");
                        return View(model);
                    }
                }
                Console.WriteLine("Da dang nhap bang tai khoan Admin");
                EmailAddress.email = admin.UserEmail;
                Cookie(emailSignIn, admin.UserEmail, 60); // gán giá trị cho cookie có tên là Email
                return RedirectToAction(nameof(SendOtp));
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
                Cookie(emailSignUp, model.UserEmail, 60);
                Cookie("Password", model.LoginPassword, 60);
                Cookie("LogInName", model.UserLogin, 60);
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
                    Cookie(emailRecoverPassword, model.LogInName, 60);
                    Cookie("RecoverPassword", model.Password, 60);
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
            if (valueCookie(emailSignIn) != null)
                _to = valueCookie(emailSignIn);
            else if (valueCookie(emailSignUp) != null)
                _to = valueCookie(emailSignUp);
            else if(valueCookie(emailRecoverPassword) != null)
                _to = valueCookie(emailRecoverPassword);
            if (_to != "")
            {
                _sendEmail.SendOTPEmail(_to, otpCode);
                //HttpContext.Session.SetString("otpCode", otpCode); // sử dụng session toàn cục
                Cookie(OTP, otpCode, 1); 
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
                if (Request.Cookies[emailSignIn] != null)
                {
                    HttpContext.Session.SetString("LogIn Session", valueCookie(emailSignIn));
                    deleteCookie(emailSignIn);
                    return RedirectToAction(nameof(UserInformation));
                }
                if(Request.Cookies[emailSignUp] != null)
                {
                    CustomerModel customer = new CustomerModel
                    {
                        UserName = valueCookie("LogInName"),
                        UserLogin = valueCookie("LogInName"),
                        UserEmail = valueCookie(emailSignUp),
                        LoginPassword = valueCookie("Password"),
                        UserState = true,
                        _ServicePackage = ServicePackage.Bacis,
                        UserCreateDate = DateTime.Now
                    };
                    _dataContext.Customers.Add(customer); // thêm customer mới vào database
                    await _dataContext.SaveChangesAsync();
                    WatchListModel watchList = new WatchListModel
                    {
                        CustomerId = customer.CustomerId
                    };
                    _dataContext.WatchLists.Add(watchList); // thêm watchList mới vào database
                    await _dataContext.SaveChangesAsync();
                    customer.WatchListId = watchList.WatchListId;
                    await _dataContext.SaveChangesAsync();
                    deleteCookie(emailSignUp);
                    return RedirectToAction(nameof(SignIn));
                }
                if (Request.Cookies[emailRecoverPassword] != null)
                {
                    string userName = valueCookie(emailRecoverPassword);
                    var customer = await _dataContext.Customers.FirstOrDefaultAsync(c => c.UserName == userName || c.UserEmail == userName);
                    if(customer != null)
                    {
                        customer.LoginPassword = valueCookie("RecoverPassword");
                    }
                    await _dataContext.SaveChangesAsync();
                    deleteCookie("RecoverPassword");
                    deleteCookie(emailRecoverPassword);
                    return RedirectToAction(nameof(SignIn));
                }
            }
            Console.WriteLine("Ban da nhap sai otp");
            return View();
        }
        [HttpPost]        
        public async Task<IActionResult> UserInformation([Bind("UserName, LoginPassword")]CustomerModel customer)
        {
            string email = HttpContext.Session.GetString("LogIn Session");
            var cus = await _dataContext.Customers.FirstOrDefaultAsync(c => c.UserEmail == email);
            return View("UserInformation", cus);
        }
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
