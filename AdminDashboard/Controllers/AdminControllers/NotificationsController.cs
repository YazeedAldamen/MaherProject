using AdminDashboard.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using ServiceLayer.DTO;
using ServiceLayer.Services;

namespace AdminDashboard.Controllers.AdminControllers
{
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
            int pageSize = 10;
            var model = await _notificationServices.GetNotificationList(page, pageSize);
            var viewModel = new NotificationListModel
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
