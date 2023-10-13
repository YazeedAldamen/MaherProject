using AdminDashboard.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using ServiceLayer.Services;

namespace AdminDashboard.Controllers
{
    public class UsersController : Controller
    {
        private readonly UsersService _usersServices;
        public UsersController(UnitOfWorkServices unitOfWorkServices)
        {
            _usersServices = unitOfWorkServices.UsersServices;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetDataTable(DataTableModel dataTable)
        {
            var data = await _usersServices.ListWithPaging(
            orderBy: dataTable.OrderBy,
            pageSize: dataTable.Length,
            page: dataTable.Start,
            isDescending: dataTable.isDescending);

            return Json(new { data = data.EntityData, recordsTotal = data.Count, recordsFiltered = data.Count, lastquestion = dataTable.Start.ToString() });
        }
    }
}
