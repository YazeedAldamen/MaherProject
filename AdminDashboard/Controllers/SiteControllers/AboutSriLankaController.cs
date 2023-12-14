using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services;
using ServiceLayer;
using AdminDashboard.Models;
using ServiceLayer.DTO;
using System.Text.Json;
using AdminDashboard.Models.SiteModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdminDashboard.Controllers.SiteControllers
{
    public class AboutSriLankaController : Controller
    {
        private readonly BlogServices _blogServices;

        public AboutSriLankaController(UnitOfWorkServices unitOfWorkServices)
        {
            _blogServices = unitOfWorkServices.BlogServices;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> GetData(int page = 1)
        {
            int pageSize = 10;
            var data = await _blogServices.GetPublishedBlogs(page, pageSize);
            var model = new BlogsListModel()
            {
                Blogs = data.data.Select(x => new BlogModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    CardImageUrl = x.CardImageUrl,
                    ShortDescription = x.ShortDescription,
                    LastUpdate = x.LastUpdated,
                }).ToList(),
                TotalRecords = data.Count
            };
            return PartialView("_BlogsPartial", model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var data=await _blogServices.GetById(id);

            BlogModel model = new BlogModel
            {
                BlogMainText = data.BlogMainText,
                Title = data.Title,
                IsPublished = data.IsPublished,
                BlogMainImage = data.BlogMainImage,
                SecondaryDescription = data.SecondaryDescription,
                CardImageUrl = data.CardImageUrl,
                VideoUrl = data.Video,
                ShortDescription = data.ShortDescription
            };
            if (!string.IsNullOrEmpty(data.BlogMainImage))
            {
                List<ImageInfo> deserializedCollection = JsonSerializer.Deserialize<List<ImageInfo>>(data.BlogMainImage);
                model.ImageInfo = deserializedCollection;
            }
            return View(model);
        }
    }
}
