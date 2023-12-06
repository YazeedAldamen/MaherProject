using AdminDashboard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using ServiceLayer.Services;

namespace AdminDashboard.Controllers.AdminControllers
{
    [Authorize(Roles = "Admin")]
    public class ContactController : Controller
    {
        private readonly ContactService _contactServices;

        public ContactController(UnitOfWorkServices unitOfWorkServices)
        {
            _contactServices = unitOfWorkServices.ContactService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetDataTable(DataTableModel dataTable)
        {
            var data = await _contactServices.ListWithPaging(
            orderBy: dataTable.OrderBy,
            pageSize: dataTable.Length,
            page: dataTable.Start,
            isDescending: dataTable.isDescending);

            return Json(new { data = data.EntityData, recordsTotal = data.Count, recordsFiltered = data.Count, lastquestion = dataTable.Start.ToString() });
        }
    }
}
