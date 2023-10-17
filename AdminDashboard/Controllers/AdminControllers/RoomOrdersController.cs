using AdminDashboard.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using ServiceLayer.Services;

namespace AdminDashboard.Controllers.AdminControllers
{
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
            var data = await _roomOrderService.ListWithPaging(
            orderBy: dataTable.OrderBy,
            pageSize: dataTable.Length,
            page: dataTable.Start,
            isDescending: dataTable.isDescending);

            return Json(new { data = data.EntityData, recordsTotal = data.Count, recordsFiltered = data.Count, lastquestion = dataTable.Start.ToString() });
        }
    }
}
