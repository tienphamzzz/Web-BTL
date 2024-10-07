using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_BTL.Models;
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
        public AccountController(DataContext dataContext, SendEmail sendEmail)
        {
            _dataContext = dataContext;
            _sendEmail = sendEmail;
        }
        [HttpGet]
        public IActionResult SignIn()
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
                CheckAction.checkAccount = 1;
                if (admin == null)
                {
                    var customer = await _dataContext.Customers.
                        FirstOrDefaultAsync(c => ((c.UserEmail == model.LogInName || c.UserLogin == model.LogInName) && c.LoginPassword == model.Password) && c.UserState == true);
                    if (customer != null)
                    {
                        Console.WriteLine("Da dang nhap bang tai khoan Customer");
                        EmailAddress.email = customer.UserEmail;
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
                model.UserState = true;
                model._ServicePackage = ServicePackage.Bacis;
                model.UserCreateDate = DateTime.Now;
                return RedirectToAction(nameof(SendOtp));
            }
            return View(model);
        }

        public IActionResult RecoverPasswork()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SendOtp()
        {
            Console.WriteLine("Day la get SendOtp");
            string otpCode = _sendEmail.GenerateOTP();
            _sendEmail.SendOTPEmail(EmailAddress.email, otpCode);
            HttpContext.Session.SetString("otpCode", otpCode);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendOtp(string Otp)
        {
            Console.WriteLine("Day la post SendOtp");
            if(Otp == HttpContext.Session.GetString("otpCode"))
            {
                Console.WriteLine("Ban da nhap dung ma OTP");
                switch(CheckAction.checkAccount)
                {
                    case 1:
                        return RedirectToAction(nameof(Index), "Home");
                    case 2: break;
                    case 3: break;
                    default: break;
                }
            }
            Console.WriteLine("Ban da nhap sai otp");
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
