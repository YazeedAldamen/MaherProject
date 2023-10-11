using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using ServiceLayer.Services;

namespace AdminDashboard.Controllers
{
    public class BlogsController : Controller
    {
        private readonly BlogServices _blogServices;

        public BlogsController(UnitOfWorkServices unitOfWorkServices)
        {
            _blogServices=unitOfWorkServices.BlogServices;
        }
        public async Task<IActionResult> Index()
        {
            var blogs=await _blogServices.GetAllBlogs();
            return View(blogs);
        }
    }
}
