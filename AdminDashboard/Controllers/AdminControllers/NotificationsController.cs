using AdminDashboard.Models;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using ServiceLayer.DTO;
using ServiceLayer.Services;
using System.Security.Claims;

namespace AdminDashboard.Controllers.AdminControllers
{
    [Authorize(Roles = "Admin,Service Provider")]
    public class NotificationsController : Controller
    {
        private readonly NotificationServices _notificationServices;

        public NotificationsController(UnitOfWorkServices unitOfWorkServices)
        {
            _notificationServices = unitOfWorkServices.NotificationServices;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            return View();
        }

        public async Task<IActionResult> GetData(int page = 1)
        {
            var viewModel = new NotificationListModel();
            int pageSize = 10;
            Guid userId = Guid.Empty;
            bool isServiceProvider = User.IsInRole("Service Provider");
            if (isServiceProvider) 
            {
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;

                var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

                userId = Guid.Parse(claim.Value);
            }
            var model = await _notificationServices.GetNotificationList(userId,page, pageSize);
            viewModel = new NotificationListModel
            {
                NotificationsDTO = model.data.Select(x => new NotificationDTO
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    CreateDate = x.CreateDate,
                    Path = x.Path
                }).ToList(),
                TotalCount = model.Count
            };
   
            return PartialView("_NotificationPartial", viewModel);
        }

        public async Task<IActionResult> ChangeStatus(int notificationId)
        {
            await _notificationServices.ChangeStatus(notificationId);
            return Json(new { success = true }); // You can include additional data if needed

        }
    }
}
