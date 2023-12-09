using DataLayer.Entities;
using DataLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using ServiceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ServiceLayer.Services
{
    public class PackageServices
    {
        private readonly IGenericRepository<Package> _packageRepository;
        private readonly ImageService _imageService;
        public PackageServices(IUnitOfWorkRepositories unitofworkRepository, ImageService imageService)
        {
            _packageRepository = unitofworkRepository.PackageRepository;
            _imageService = imageService;
        }

        public async Task<(IEnumerable<PackageDTO> EntityData, int Count)> ListWithPaging(
string orderBy, int? page, int? pageSize, bool isDescending)
        {
            Func<IQueryable<Package>, IOrderedQueryable<Package>> orderByExpression;

            switch (orderBy)
            {
                case "Id":
                    orderByExpression = query => isDescending ? query.OrderByDescending(entity => entity.Id) : query.OrderBy(entity => entity.Id);
                    break;
                case "Name":
                    orderByExpression = query => isDescending ? query.OrderByDescending(entity => entity.Name) : query.OrderBy(entity => entity.Name);
                    break;
                case "BlogMainText":
                    orderByExpression = query => isDescending ? query.OrderByDescending(entity => entity.Description) : query.OrderBy(entity => entity.Description);
                    break;
                case "Price":
                    orderByExpression = query => isDescending ? query.OrderByDescending(entity => entity.Price) : query.OrderBy(entity => entity.Price);
                    break;
                case "IsPublished":
                    orderByExpression = query => isDescending ? query.OrderByDescending(entity => entity.IsPublished) : query.OrderBy(entity => entity.IsPublished);
                    break;
                default:
                    orderByExpression = query => query.OrderBy(entity => entity.Id);
                    break;
            }

            (IList<Package> EntityData, int Count) = await _packageRepository.ListWithPaging(
                orderBy: orderByExpression,
                page: page,
                pageSize: pageSize,
                filter: null
            );

            IEnumerable<PackageDTO> results = EntityData.Select(x => new PackageDTO
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                IsPublished = x.IsPublished,
            }).ToList();

            return (results, Count);
        }

        public async Task<PackageDTO> GetById(int Id)
        {
            var entity = await _packageRepository.GetById(Id);
            List<ImageInfo> images = new List<ImageInfo>();
            List<AboutPackage> aboutPackages = new List<AboutPackage>();
            if (!string.IsNullOrEmpty(entity.PackageImage1))
            {
                images = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ImageInfo>>(entity.PackageImage1);
            }
            if(!string.IsNullOrEmpty(entity.AboutPackage))
            {
                aboutPackages = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AboutPackage>>(entity.AboutPackage);
            }
            var data = new PackageDTO
            {
                Description = entity.Description,
                PackageMainImage = entity.PackageMainImage,
                ImageInfo = images,
                PackageTypeId = entity.PackageTypeId,
                Name = entity.Name,
                Price = entity.Price,
                Discount = entity.Discount,
                IsPublished = entity.IsPublished,
                IsDeleted = entity.IsDeleted,
                NumberOfDays = entity.NumberOfDays,
                NumberOfNights = entity.NumberOfNights,
                UserId = entity.UserId,
                PackageDays = aboutPackages,
                RoomClassId = entity.RoomClassId,
            };
            return data;
        }


        public async Task Edit(PackageDTO data)
        {
            try
            {
                var entity = await _packageRepository.GetById(data.Id);
                entity.Name = data.Name;
                entity.Description = data.Description;
                entity.PackageTypeId = data.PackageTypeId;
                entity.RoomClassId = data.RoomClassId;
                entity.Price = data.Price;
                entity.Discount = data.Discount;

                entity.IsPublished = data.IsPublished;

                entity.IsDeleted = data.IsDeleted;
                if(data.AboutPackage != "[]")
                {
                    entity.AboutPackage = data.AboutPackage;
                }


                entity.NumberOfDays = data.NumberOfDays;
                entity.NumberOfNights = data.NumberOfNights;
                entity.UserId = data.UserId;
                //entity = await HandlePackageMultipleImages(entity, data);
                //entity = await HandleHotelMultipleImages(entity, data);

                entity.PackageMainImage = await _imageService.HandleImage(entity.PackageMainImage, data.PackageMainImageFile);
                entity.PackageImage1 = data.PackageImages != null ? await _imageService.HandleMultipleImages(data.PackageImages, true, entity.PackageImage1) : entity.PackageImage1;


                await _packageRepository.Edit(entity);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task Delete(int Id)
        {
            var entity = await _packageRepository.GetById(Id);
            if (entity != null)
            {
                List<string> images = new List<string>();
                images.Add(entity.PackageMainImage);
                images.Add(entity.PackageImage1);
                images.ForEach(async image =>
                {
                    if (!string.IsNullOrEmpty(image))
                    {
                        FileManager.DeleteFile(image);
                    }
                });
                await _packageRepository.Delete(entity);
            }
        }

        public async Task Create(PackageDTO data)
        {
            try
            {
                var entity = new Package()
                {
                    Description = data.Description,
                    Name = data.Name,
                    Price = data.Price,
                    Discount = data.Discount,
                    IsPublished = data.IsPublished,
                    IsDeleted = data.IsDeleted,
                    NumberOfDays = data.NumberOfDays,
                    NumberOfNights = data.NumberOfNights,
                    UserId = data.UserId,
                    AboutPackage = data.AboutPackage,
                    RoomClassId = data.RoomClassId,
                    PackageTypeId = data.PackageTypeId,

                };

                entity.PackageMainImage = await ImageService.UploadFile(data.PackageMainImageFile);
                entity.PackageImage1 = await _imageService.HandleMultipleImages(data.PackageImages, false);

                await _packageRepository.Add(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


    }
}
