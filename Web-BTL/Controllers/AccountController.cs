using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_BTL.Models;
using Web_BTL.Models.User.Customer;
using Web_BTL.Repository;
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
        public IActionResult SendOTP()
        {
            string ToEmail = EmailAddress.email;
            //string OTP = SendEmail.GenerateOTP();
            return View();
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            Console.WriteLine("Day la get SignIn");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(LogIn model)
        {
            Console.WriteLine("Day la post SignIn");
            var admin = await _dataContext.Admins.FirstOrDefaultAsync(a => (a.UserEmail == model.LogInName || a.UserLogin == model.LogInName) && a.LoginPassword == model.Password);
            
            if(admin == null)
            {
                var customer = await _dataContext.Customers.FirstOrDefaultAsync(c => (c.UserEmail == model.LogInName || c.UserLogin == model.LogInName) && c.LoginPassword == model.Password);
                if (customer != null)
                {
                    EmailAddress.email = customer.UserEmail;
                    return RedirectToAction();
                }
                else
                    return View();
            }
            EmailAddress.email = admin.UserEmail;
            return RedirectToAction();
        }

        

        public IActionResult Index()
        {
            return View();
        }
    }
}
