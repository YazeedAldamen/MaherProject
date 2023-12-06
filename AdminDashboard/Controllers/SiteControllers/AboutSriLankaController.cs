using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services;
using ServiceLayer;
using AdminDashboard.Models;
using ServiceLayer.DTO;
using System.Text.Json;

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
            var data= await _blogServices.GetPublishedBlogs();
            var model = data.Select(x=> new BlogModel
            {
                Id = x.Id,
                Title = x.Title,
                CardImageUrl = x.CardImageUrl,
                ShortDescription = x.ShortDescription,
                LastUpdate = x.LastUpdated,
            });
            return View(model);
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
