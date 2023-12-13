using AdminDashboard.Models;
using AdminDashboard.Models.SiteModels;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using ServiceLayer.DTO;
using ServiceLayer.Services;

namespace AdminDashboard.Controllers.SiteControllers
{
    public class SitePackagesController : BaseController
    {
        private readonly PackageServices _packageServices;
        public SitePackagesController(UnitOfWorkServices unitOfWorkServices)
        {
            _packageServices = unitOfWorkServices.PackageServices;
        }

        [HttpGet]
        [Route("/packages")]
        public async Task<IActionResult> SitePackages(PackageModel model, int skip = 1)
        {
            int take = 10;
            var dataModel = await _packageServices.GetPackageAsync(skip, take);
            var viewModel = new PackageListModel
            {
                Packages = dataModel.data.Select(x => new PackageDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    PackageMainImage = x.PackageMainImage,
                    NumberOfDays = x.NumberOfDays,
                    NumberOfNights = x.NumberOfNights,
                    Price = x.Price,
                }).ToList(),
                TotalRecords = dataModel.totalRecords
            };

            return View(viewModel);
        }

        public async Task<IActionResult> GetData(int page = 1)
        {
            int pageSize = 10;
            var model = await _packageServices.GetPackagesList(page, pageSize);
            var viewModel = new PackageListModel
            {
                Packages = model.data.Select(x => new PackageDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    PackageMainImage = x.PackageMainImage,
                    NumberOfDays = x.NumberOfDays,
                    NumberOfNights = x.NumberOfNights,
                    Price = x.Price,
                }).ToList(),
                TotalRecords = model.Count
            };
            return PartialView("_PackagesPartial", viewModel);
        }

        [HttpGet]
        [Route("/packages/details/{Id}")]
        public async Task<IActionResult> PackageDetails(int Id)
        {
            var data = await _packageServices.GetById(Id);
            var model = new PackageModel
            {
                Description = data.Description,
                PackageMainImage = data.PackageMainImage,
                ImageInfo = data.ImageInfo,
                PackageTypeId = data.PackageTypeId,
                Name = data.Name,
                Price = data.Price,
                Discount = data.Discount,
                IsPublished = data.IsPublished,
                IsDeleted = data.IsDeleted,
                NumberOfDays = data.NumberOfDays,
                NumberOfNights = data.NumberOfNights,
                UserId = data.UserId,
                PackageDays = data.PackageDays,
                RoomClassId = data.RoomClassId
            };
            ViewBag.Cities = Cities;
            return View(model);
        }

    }
}
