using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services;
using ServiceLayer;
using AdminDashboard.Models.SiteModels;
using AdminDashboard.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace AdminDashboard.Controllers.SiteControllers
{
    public class ReviewsController : BaseController
    {
        private readonly ReviewService _reviewsServices;

        public ReviewsController(UnitOfWorkServices unitOfWorkServices)
        {
            _reviewsServices = unitOfWorkServices.ReviewService;
        }

        [Authorize(Roles ="User")]
        public async Task<IActionResult> AddPackageReview(int PackageId, string ReviewText) 
        {
            if (User.Identity.IsAuthenticated) 
            { 
                Guid userId = Guid.Empty;
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
                userId = Guid.Parse(claim.Value);
                await _reviewsServices.AddPackageReview(PackageId, ReviewText,userId);
                ShowSuccessMessage("Review Added Successfully");
            }
            return RedirectToAction("GetData", "Reviews");

        }

        public async Task<IActionResult> GetData(int Id,int page = 1)
        {
            int pageSize = 10;
            var data = await _reviewsServices.GetReviews(Id,page, pageSize);
            var model = new ReviewsListModel()
            {
                Reviews = data.data,
                TotalRecords = data.Count
            };
            return PartialView("_ReviewsPartial", model);
        }

    }
}
