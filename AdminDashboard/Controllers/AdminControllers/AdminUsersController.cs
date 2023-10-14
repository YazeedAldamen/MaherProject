using AdminDashboard.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using ServiceLayer.Services;

namespace AdminDashboard.Controllers.AdminControllers
{
    public class AdminUsersController : Controller
    {
        private readonly AdminUsersService _adminUsersService;
        public AdminUsersController(UnitOfWorkServices unitOfWorkServices)
        {
            _adminUsersService = unitOfWorkServices.AdminUsersService;
        }
        public IActionResult AdminUsers()
        {
            return View();
        }

        public async Task<JsonResult> GetDataTable(DataTableModel dataTable)
        {
            var data = await _adminUsersService.ListWithPaging(
                           orderBy: dataTable.OrderBy,
                           pageSize: dataTable.Length,
                           page: dataTable.Start,
                           isDescending: dataTable.isDescending);

            return Json(new { data = data.EntityData, recordsTotal = data.Count, recordsFiltered = data.Count, lastquestion = dataTable.Start.ToString(), roles = data.roles });
        }

        // write me a post method to update the user role after the dropdown is changed
        [HttpPost]
        public async Task<IActionResult> UpdateRole(string id, string role)
        {
            try
            {
                await _adminUsersService.UpdateUserRole(id, role);
                //ShowSuccessMessage("Role Updated Successfully");
            }
            catch (Exception ex)
            {
                //ShowErrorMessage("Something Went Wrong" + ex.Message);

            }
            return RedirectToAction("AdminUsers");
        }
    }
}
