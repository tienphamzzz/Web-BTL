﻿using Microsoft.AspNetCore.Mvc;

namespace Web_BTL.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
