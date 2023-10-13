using AdminDashboard.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using ServiceLayer.DTO;
using ServiceLayer.Services;

namespace AdminDashboard.Controllers.AdminControllers
{
    public class HotelRequestController : BaseController
    {
        private readonly ProviderServiceService _providerService;

        public HotelRequestController(UnitOfWorkServices unitOfWorkServices)
        {
            _providerService = unitOfWorkServices.ProviderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetDataTable(DataTableModel dataTable)
        {
            var data = await _providerService.ListWithPaging(
            orderBy: dataTable.OrderBy,
            pageSize: dataTable.Length,
            page: dataTable.Start,
            isDescending: dataTable.isDescending);

            return Json(new { data = data.EntityData, recordsTotal = data.Count, recordsFiltered = data.Count, lastquestion = dataTable.Start.ToString() });
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await _providerService.GetById(id);
            ViewBag.id = id;
            return Json(model);
        }

        public async Task<IActionResult>Accept(int id)
        {
            try 
            { 
                await _providerService.Accept(id);
                ShowSuccessMessage("Room Accepted Successfully");
            }
            catch (Exception ex) 
            {
                ShowErrorMessage("Something Went Wrong"+ex.Message);

            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Reject(int id)
        {
            try
            {
                await _providerService.Reject(id);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Something Went Wrong" + ex.Message);

            }
            return RedirectToAction("Index");
        }
    }
}
