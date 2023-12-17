using AdminDashboard.Models;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ServiceLayer;
using ServiceLayer.DTO;
using ServiceLayer.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdminDashboard.Controllers.AdminControllers
{
    //[Authorize(Roles = "Admin")]
    public class PackagesController : BaseController
    {
        private readonly PackageServices _packageServices;
        public PackagesController(UnitOfWorkServices unitOfWorkServices)
        {
            _packageServices = unitOfWorkServices.PackageServices;
        }
        [Route("/Admin/Packages")]
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
            PackageModel model = new PackageModel();
            try
            {
                var data = await _packageServices.GetById(Id);
                model = new PackageModel
                {
                    Id = Id,
                    Description = data.Description,
                    PackageMainImage = data.PackageMainImage,
                    ImageInfo = data.ImageInfo,
                    PackageTypeId = data.PackageTypeId,
                    RoomClassId = data.RoomClassId,
                    Price = data.Price,
                    Discount = data.Discount,
                    Name = data.Name,
                    IsPublished = data.IsPublished,

                    IsDeleted = data.IsDeleted,

                    NumberOfDays = data.NumberOfDays,
                    NumberOfNights = data.NumberOfNights,

                    UserId = data.UserId,
                    PackageDays = data.PackageDays,

                };

                ViewBag.PackageTypes = PackageTypes;
                ViewBag.RoomClass = RoomClass;
                ViewBag.Cities = Cities;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Something went wrong" + ex.Message);
            }
            return View(model);

        }

        [HttpPut]
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
                AboutPackage = model.AboutPackage,
                PackageImages = model.PackageImages,
                PackageTypeId = model.PackageTypeId,
                RoomClassId = model.RoomClassId,
                Price = model.Price,
                Discount = model.Discount,

                IsPublished = model.IsPublished,

                IsDeleted = model.IsDeleted,
                AboutPackageImages = model.AboutPackageImages,


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
            ViewBag.PackageTypes = PackageTypes;
            ViewBag.RoomClass = RoomClass;
            ViewBag.Cities = Cities;
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
                RoomClassId = model.RoomClassId,
                Price = model.Price,
                Discount = model.Discount,
                AboutPackage = model.AboutPackage,
                IsPublished = model.IsPublished,
                AboutPackageImages = model.AboutPackageImages,
                IsDeleted = model.IsDeleted,
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
        public async Task<IActionResult> TopTenPackages()
        {
            var data = await _packageServices.GetTopTen();
            return View(data);
        }

        public async Task<IActionResult> EditTopTenPackages()
        {
            var data = await _packageServices.GetAllPackagesAsync();
            var packages = data.Select(x => new PackageModel
            {
                Id = x.Id,
                Name = x.Name,
                TopTen = x.TopTen
            }).ToList();
            var result =  new TopTenModel
            {
                 Packages = packages,
                 oldTopTen = data.Select(x=>x.Id).ToList(),
                 newTopTen= new List<int>()
            };
            return View(result);
        }

        [HttpPut]
        public async Task<IActionResult> EditTopTen(List<int> newTopTen, List<int> oldTopTen)
        {
            try
            {
                await _packageServices.EditTopTen(newTopTen, oldTopTen);
                ShowSuccessMessage("Package Updated Successfully");
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Something went wrong" + ex.Message);
            }
            return RedirectToAction(nameof(TopTenPackages));
        }
    }
}
