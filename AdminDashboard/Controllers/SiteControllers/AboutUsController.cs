using Microsoft.AspNetCore.Mvc;

namespace AdminDashboard.Controllers.SiteControllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
