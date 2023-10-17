using AdminDashboard.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using ServiceLayer.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using X.PagedList;
using ServiceLayer.DTO;
using DataLayer.Entities;

namespace AdminDashboard.Controllers.AdminControllers
{
    public class HotelRoomsController : BaseController
    {
        private readonly HotelRoomService _roomService;

        public HotelRoomsController(UnitOfWorkServices unitOfWorkServices)
        {
            _roomService = unitOfWorkServices.HotelRoomService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 4;
            ViewBag.Page=page;
            var model = await _roomService.ListWithPaging(page, pageSize);

            var viewModel = new RoomListViewModel
            {
                Rooms = model.EntityData.Select(x => new HotelRoomsDTO
                {
                    Id = x.Id,
                    HotelName = x.HotelName,
                    NumberOfBathrooms = x.NumberOfBathrooms,
                    NumberOfBeds = x.NumberOfBeds,
                    HotelMainImage = x.HotelMainImage,
                    NumberOfSofas = x.NumberOfSofas,
                    IsAC = x.IsAC,
                    IsRoomHeater = x.IsRoomHeater,
                    IsTV = x.IsTV,
                    IsWifi = x.IsWifi,
                    NumberOfAdults = x.NumberOfAdults,
                    NumberOfChildren = x.NumberOfChildren,
                    Price = x.Price,
                    IsPublished = x.IsPublished,
                }).ToList(),
                TotalCount = model.Count
            };
            return View(viewModel);
        }

		public IActionResult Requests()
		{
			return View();
		}

		public async Task<JsonResult> GetDataTable(DataTableModel dataTable)
		{
			var data = await _roomService.ListWithPaging(
			orderBy: dataTable.OrderBy,
			pageSize: dataTable.Length,
			page: dataTable.Start,
			isDescending: dataTable.isDescending);

			return Json(new { data = data.EntityData, recordsTotal = data.Count, recordsFiltered = data.Count, lastquestion = dataTable.Start.ToString() });
		}

		public async Task<IActionResult> Details(int id)
		{
			var model = await _roomService.GetById(id);
			ViewBag.id = id;
			return Json(model);
		}

		public async Task<IActionResult> Accept(int id)
		{
			try
			{
				await _roomService.Accept(id);
				ShowSuccessMessage("Room Accepted Successfully");
			}
			catch (Exception ex)
			{
				ShowErrorMessage("Something Went Wrong" + ex.Message);

			}
			return RedirectToAction("Requests");
		}

		public async Task<IActionResult> Reject(int id)
		{
			try
			{
				await _roomService.Reject(id);
                ShowSuccessMessage("Room Rejected Successfully");
            }
            catch (Exception ex)
			{
				ShowErrorMessage("Something Went Wrong" + ex.Message);

			}
			return RedirectToAction("Requests");
		}

		public async Task<IActionResult> Delete(int Id)
        {
            try 
            { 
                await _roomService.Delete(Id);
                ShowSuccessMessage("Room Deleted Successfully");

            }
            catch (Exception ex)
            {
                ShowErrorMessage("Something went Wrong," + ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Hide(int Id)
        {
            try 
            { 
                await _roomService.Hide(Id);
                ShowSuccessMessage("Room Hidden Successfully");
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Something went Wrong," + ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Show(int Id)
        {
            try 
            { 
                await _roomService.Show(Id);
                ShowSuccessMessage("Room Shown successfully");
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Something went Wrong," + ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
