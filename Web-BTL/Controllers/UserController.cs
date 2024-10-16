using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult UserInformation()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UserInformation([Bind("UserName, LoginPassword")] CustomerModel customer)
        {
            string email = HttpContext.Session.GetString("LogIn Session");
            if(email != null)
            {
                var cus = await _dataContext.Customers.FirstOrDefaultAsync(c => c.UserEmail == email);
                if (cus == null) return RedirectToAction(nameof(SignIn), "Account");
                if (customer != null)
                {
                    cus.UserName = customer.UserName;
                    cus.LoginPassword = customer.LoginPassword;
                }
                return View("UserInformation", cus);
            }
            return RedirectToAction(nameof(SignIn), "Account");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
