using AdminDashboard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using ServiceLayer.DTO;
using ServiceLayer.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdminDashboard.Controllers.AdminControllers
{
    [Authorize(Roles = "Admin")]
    public class PackagesController : BaseController
    {
        private readonly PackageServices _packageServices;
        public PackagesController(UnitOfWorkServices unitOfWorkServices)
        {
            _packageServices = unitOfWorkServices.PackageServices;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetDataTable(DataTableModel dataTable)
        {
            var data = await _packageServices.ListWithPaging(
            orderBy: dataTable.OrderBy,
            pageSize: dataTable.Length,
            page: dataTable.Start,
            isDescending: dataTable.isDescending);

            return Json(new { data = data.EntityData, recordsTotal = data.Count, recordsFiltered = data.Count, lastquestion = dataTable.Start.ToString() });
        }

        public async Task<IActionResult> Edit(int Id)
        {
            var data = await _packageServices.GetById(Id);
            PackageModel model = new PackageModel
            {
                Id = Id,
                Description = data.Description,
                PackageMainImage = data.PackageMainImage,
                PackageImage1 = data.PackageImage1,
                PackageImage2 = data.PackageImage2,
                PackageImage3 = data.PackageImage3,
                PackageTypeId = data.PackageTypeId,

                Price = data.Price,
                Discount = data.Discount,

                IsPublished = data.IsPublished,
                IsVip = data.IsVip,
                IsAC = data.IsAC,
                IsTV = data.IsTV,
                IsWifi = data.IsWifi,
                IsRoomHeater = data.IsRoomHeater,
                IsDeleted = data.IsDeleted,

                HotelName = data.HotelName,
                HotelDescription = data.HotelDescription,
                HotelMainImage = data.HotelMainImage,
                HotelImage1 = data.HotelImage1,
                HotelImage2 = data.HotelImage2,
                HotelImage3 = data.HotelImage3,

                NumberOfAdults = data.NumberOfAdults,
                NumberOfChildren = data.NumberOfChildren,
                NumberOfBeds = data.NumberOfBeds,
                NumberOfBathrooms = data.NumberOfBathrooms,
                NumberOfSofas = data.NumberOfSofas,

                NumberOfDays = data.NumberOfDays,
                NumberOfNights = data.NumberOfNights,

                UserId = data.UserId
            };
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(PackageModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var data = new PackageDTO
            {
                Id = model.Id,
                Description = model.Description,
                PackageMainImageFile = model.PackageMainImageFile,
                Name = model.Name,

                PackageImages = model.PackageImages,
                PackageTypeId = model.PackageTypeId,

                Price = model.Price,
                Discount = model.Discount,

                IsPublished = model.IsPublished,
                IsVip = model.IsVip,
                IsAC = model.IsAC,
                IsTV = model.IsTV,
                IsWifi = model.IsWifi,
                IsRoomHeater = model.IsRoomHeater,
                IsDeleted = model.IsDeleted,

                HotelName = model.HotelName,
                HotelDescription = model.HotelDescription,
                HotelMainImageFile = model.HotelMainImageFile,
                HotelImages = model.HotelImages,

                NumberOfAdults = model.NumberOfAdults,
                NumberOfChildren = model.NumberOfChildren,
                NumberOfBeds = model.NumberOfBeds,
                NumberOfBathrooms = model.NumberOfBathrooms,
                NumberOfSofas = model.NumberOfSofas,

                NumberOfDays = model.NumberOfDays,
                NumberOfNights = model.NumberOfNights,

                UserId = model.UserId
            };
            try
            {
                await _packageServices.Edit(data);
                ShowSuccessMessage("Package Updated Successfully");

            }
            catch (Exception ex)
            {
                ShowErrorMessage("Something went wrong" + ex.Message);
            }
            return RedirectToAction(nameof(Index), new { model.Id });
        }
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                await _packageServices.Delete(Id);
                ShowSuccessMessage("Package Deleted Successfully");
            }
            catch (Exception ex)
            {
                ShowErrorMessage("This package has orders");
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(PackageModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var data = new PackageDTO
            {
                Name = model.Name,
                Description = model.Description,
                PackageMainImageFile = model.PackageMainImageFile,
                PackageImages = model.PackageImages,
                PackageTypeId = model.PackageTypeId,

                Price = model.Price,
                Discount = model.Discount,

                IsPublished = model.IsPublished,
                IsVip = model.IsVip,
                IsAC = model.IsAC,
                IsTV = model.IsTV,
                IsWifi = model.IsWifi,
                IsRoomHeater = model.IsRoomHeater,
                IsDeleted = model.IsDeleted,

                HotelName = model.HotelName,
                HotelDescription = model.HotelDescription,
                HotelMainImageFile = model.HotelMainImageFile,
                HotelImages = model.HotelImages,

                NumberOfAdults = model.NumberOfAdults,
                NumberOfChildren = model.NumberOfChildren,
                NumberOfBeds = model.NumberOfBeds,
                NumberOfBathrooms = model.NumberOfBathrooms,
                NumberOfSofas = model.NumberOfSofas,

                NumberOfDays = model.NumberOfDays,
                NumberOfNights = model.NumberOfNights,

                UserId = model.UserId
            };
            try
            {
                await _packageServices.Create(data);
                ShowSuccessMessage("Package Updated Successfully");

            }
            catch (Exception ex)
            {
                ShowErrorMessage("Something went wrong" + ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
