using DataLayer.Entities;
using DataLayer.Interfaces;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using ServiceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ServiceLayer.Services
{
    public class PackageServices
    {
        private readonly IGenericRepository<Package> _packageRepository;
        private readonly ImageService _imageService;
        private readonly SitePackagesRepository _sitePackagesRepository;
        public PackageServices(IUnitOfWorkRepositories unitofworkRepository, ImageService imageService)
        {
            _packageRepository = unitofworkRepository.PackageRepository;
            _imageService = imageService;
            _sitePackagesRepository = unitofworkRepository.SitePackagesRepository;
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
            if (!string.IsNullOrEmpty(entity.AboutPackage))
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
                entity.NumberOfDays = data.NumberOfDays;
                entity.NumberOfNights = data.NumberOfNights;
                entity.UserId = data.UserId;
                entity.PackageMainImage = await _imageService.HandleImage(entity.PackageMainImage, data.PackageMainImageFile);
                entity.PackageImage1 = data.PackageImages != null ? await _imageService.HandleMultipleImages(data.PackageImages, true, entity.PackageImage1) : entity.PackageImage1;

                if (data.AboutPackage != "[]")
                {
                    List<AboutPackage> aboutPackages = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AboutPackage>>(entity.AboutPackage);
                    List<ImageInfo> oldImagesList = new List<ImageInfo>();
                    aboutPackages.ForEach(x =>
                    {
                        oldImagesList.Add(new ImageInfo
                        {
                            Name = x.ImageName,
                            ImagePath = x.ImageName
                        });
                    });
                    if (data.AboutPackageImages != null && data.AboutPackageImages.Any(x => x.Length > 0))
                    {
                        string newImages = await _imageService.HandleMultipleImages(data.AboutPackageImages, true, "", oldImagesList);
                        data.AboutPackage = ReplaceImageNameWithImagePath(data.AboutPackage, newImages, oldImagesList);
                    }
                    else
                    {
                        string oldImages = Newtonsoft.Json.JsonConvert.SerializeObject(oldImagesList);
                        data.AboutPackage = ReplaceImageNameWithImagePath(data.AboutPackage, oldImages);
                    }
                }

                entity.AboutPackage = data.AboutPackage;


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
                if (!string.IsNullOrEmpty(entity.PackageImage1))
                {
                    List<ImageInfo> imageInfos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ImageInfo>>(entity.PackageImage1);
                    imageInfos.ForEach(x =>
                    {
                        images.Add(x.ImagePath);
                    });
                }
                // deserialize the about package and get the images
                if (!string.IsNullOrEmpty(entity.AboutPackage))
                {
                    List<AboutPackage> aboutPackages = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AboutPackage>>(entity.AboutPackage);
                    aboutPackages.ForEach(x =>
                    {
                        images.Add(x.ImageName);
                    });
                }
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
                if (data.PackageMainImageFile != null)
                {
                    entity.PackageMainImage = await ImageService.UploadFile(data.PackageMainImageFile);
                }
                if (data.PackageImages.Any())
                {
                    entity.PackageImage1 = await _imageService.HandleMultipleImages(data.PackageImages, false);
                }
                if (data.AboutPackageImages.Any())
                {
                    string newImages = await _imageService.HandleMultipleImages(data.AboutPackageImages, false);
                    entity.AboutPackage = ReplaceImageNameWithImagePath(entity.AboutPackage, newImages);
                }

                await _packageRepository.Add(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public string ReplaceImageNameWithImagePath(string aboutPackage, string jsonImages, List<ImageInfo> oldImagesList = null)
        {
            List<AboutPackage> aboutPackages = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AboutPackage>>(aboutPackage);
            List<ImageInfo> newImagesList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ImageInfo>>(jsonImages);
            // loop through aboutPackages and replace image name with image path respectively on index
            aboutPackages.ForEach(x =>
            {
                if (!string.IsNullOrEmpty(newImagesList[aboutPackages.IndexOf(x)].ImagePath))
                {
                    x.ImageName = newImagesList[aboutPackages.IndexOf(x)].ImagePath;
                }
                else if (oldImagesList != null)
                {
                    x.ImageName = oldImagesList[aboutPackages.IndexOf(x)].ImagePath;
                }
            });
            return Newtonsoft.Json.JsonConvert.SerializeObject(aboutPackages);
        }

        public async Task<(List<PackageDTO> data, int totalRecords)> GetPackageAsync(int skip, int take)
        {
            (IList<Package> EntityData, int Count) packages = await _sitePackagesRepository.ListWithPaging(
                orderBy: null,
                page: skip,
                pageSize: take,
                filter: null
            );
            var packagesDtoList = packages.EntityData.Select(x => new PackageDTO
            {
                Id = x.Id,
                Name = x.Name,
                PackageMainImage = x.PackageMainImage,
                NumberOfDays = x.NumberOfDays,
                NumberOfNights = x.NumberOfNights,
                Price = x.Price,

            }).ToList();
            return (packagesDtoList, packages.Count);
        }


        public async Task<(List<PackageDTO> data, int Count)> GetPackagesList(int page = 0, int pageSize = 10)
        {
            //Func<IQueryable<Package>, IOrderedQueryable<Package>> orderByExpression;
            //orderByExpression = q => q.OrderBy(x => x.Seen).ThenByDescending(x => x.CreateDate);

            var packages = await _sitePackagesRepository.ListWithPaging(page: page, pageSize: pageSize);

            var notificationDTO = packages.EntityData.Select(x => new PackageDTO
            {
                Id = x.Id,
                Name = x.Name,
                PackageMainImage = x.PackageMainImage,
                NumberOfDays = x.NumberOfDays,
                NumberOfNights = x.NumberOfNights,
                Price = x.Price,
            }).ToList();
            return (notificationDTO, packages.Count);
        }

        public async Task<IList<PackageDTO>> GetTopTen()
        {
            Func<IQueryable<Package>, IOrderedQueryable<Package>> orderByExpression;
            orderByExpression = q => q.OrderBy(x => x.TopTen);
            var packages = await _packageRepository.List(x => x.TopTen != null, orderBy : orderByExpression);
            var topTenPackages = packages.Select(x => new PackageDTO
            {
                Id = x.Id,
                Name = x.Name,
                IsPublished = x.IsPublished,
            }).ToList();
            return topTenPackages;
        }

        public async Task<List<PackageDTO>> GetAllPackagesAsync()
        {
            var data = await _packageRepository.GetAllAsync(x=>x.IsPublished == true);
            var result =new List<PackageDTO>();
            result = data.Select(x => new PackageDTO
            {
                Id=x.Id,
                Name = x.Name,
                TopTen = x.TopTen,
            }).ToList();
            return result;
        }

        public async Task EditTopTen(List<int> newTopTen, List<int> oldTopTen)
        {
            List<Package> oldPackages = new List<Package>();
            List<Package> newPackages = new List<Package>();

            foreach (var old in oldTopTen)
            {
                var package = await _packageRepository.GetById(old);
                package.TopTen = null;
                oldPackages.Add(package);
            }
            await _packageRepository.UpdateRange(oldPackages);
            foreach(var newPackage in newTopTen)
            {
                var package = await _packageRepository.GetById(newPackage);
                package.TopTen = newTopTen.IndexOf(newPackage) + 1;
                newPackages.Add(package);
            }
            await _packageRepository.UpdateRange(newPackages);
        }
    }
}
