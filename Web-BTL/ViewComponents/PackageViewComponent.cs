using Microsoft.AspNetCore.Mvc;
using Web_BTL.Repository;

namespace Web_BTL.ViewComponents
{
    public class PackageViewComponent : ViewComponent
    {
        private readonly DataContext _dataContext;
        public PackageViewComponent(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<string> package = new List<string> { "Basic", "Premium", "Vip" };
            return View("Default", package);
        }
    }
}
