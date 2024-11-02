﻿using Microsoft.AspNetCore.Mvc;
using Web_BTL.Repository;
using Web_BTL.Models;
using System.Linq;
using System.Threading.Tasks;

namespace WebBTL.ViewComponents
{
    public class GenreViewComponent : ViewComponent
    {
        private readonly DataContext _context;

        public GenreViewComponent(DataContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool isAdmin = false)
        {
            var genres = _context.Genres.ToList();
            if (isAdmin) return View("AdminGenre", genres);
            return View("Default", genres);
        }
    }
}
