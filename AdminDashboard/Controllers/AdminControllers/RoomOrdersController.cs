using AdminDashboard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using ServiceLayer.Services;
using System.Security.Claims;

namespace AdminDashboard.Controllers.AdminControllers
{
    [Authorize(Roles = "Admin,Service Provider")]
    public class RoomOrdersController : Controller
    {
        private readonly RoomsOrderService _roomOrderService;

        public RoomOrdersController(UnitOfWorkServices unitOfWorkServices)
        {
            _roomOrderService = unitOfWorkServices.RoomsOrderService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetDataTable(DataTableModel dataTable)
        {
            Guid userId = Guid.Empty;
            bool isServiceProvider = User.IsInRole("Service Provider");
            if (isServiceProvider)
            {
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;

                var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

                userId = Guid.Parse(claim.Value);
            }
            var data = await _roomOrderService.ListWithPaging(
            orderBy: dataTable.OrderBy,
            pageSize: dataTable.Length,
            page: dataTable.Start,
            isDescending: dataTable.isDescending,
            UserId:userId);

            return Json(new { data = data.EntityData, recordsTotal = data.Count, recordsFiltered = data.Count, lastquestion = dataTable.Start.ToString() });
        }
    }
}
