using DataLayer.Entities;
using DataLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ServiceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ServiceLayer.Services
{
    public class BlogServices
    {
        private readonly IGenericRepository<Blog> _blogRepository;

        public BlogServices(IUnitOfWorkRepositories unitofworkRepository)
        {
            _blogRepository=unitofworkRepository.BlogRepository;
        }

        #region ListWithPaging
        public async Task<(IEnumerable<BlogDTO> EntityData, int Count)> ListWithPaging(
        string orderBy, int? page, int? pageSize, bool isDescending)
        {
            Func<IQueryable<Blog>, IOrderedQueryable<Blog>> orderByExpression;

            switch (orderBy)
            {
                case "Id":
                    orderByExpression = query => isDescending ? query.OrderByDescending(entity => entity.Id) : query.OrderBy(entity => entity.Id);
                    break;
                case "Title":
                    orderByExpression = query => isDescending ? query.OrderByDescending(entity => entity.Title) : query.OrderBy(entity => entity.Title);
                    break;
                case "BlogMainText":
                    orderByExpression = query => isDescending ? query.OrderByDescending(entity => entity.BlogMainText) : query.OrderBy(entity => entity.BlogMainText);
                    break;
                case "IsPublished":
                    orderByExpression = query => isDescending ? query.OrderByDescending(entity => entity.IsPublished) : query.OrderBy(entity => entity.IsPublished);
                    break;
                default:
                    orderByExpression = query => query.OrderBy(entity => entity.Id);
                    break;
            }

            (IList<Blog> EntityData, int Count) = await _blogRepository.ListWithPaging(
                orderBy: orderByExpression,
                page: page,
                pageSize: pageSize,
                filter: null
            );

            IEnumerable<BlogDTO> results = EntityData.Select(x => new BlogDTO
            {
                Id = x.Id,
                Title = x.Title,
                BlogMainText = x.BlogMainText,
                IsPublished = x.IsPublished,
            }).ToList();

            return (results, Count);
        }
        #endregion

        public async Task<BlogDTO> GetById(int Id)
        {
            var entity=await _blogRepository.GetById(Id);

            var data = new BlogDTO
            {
                Id = entity.Id,
                Title = entity.Title,
                BlogMainImage = entity.BlogMainImage,
                IsPublished = entity.IsPublished,
                BlogMainText = entity.BlogMainText,
                SecondaryDescription = entity.SecondaryDescription,
                Video=entity.Video,
                CardImageUrl=entity.BlogCardImage,
                ShortDescription=entity.ShortDescription,
                LastUpdated=entity.LastUpdated,
            };
            return data;
        }

        public async Task<IEnumerable<BlogDTO>> GetPublishedBlogs()
        {
            var entities = await _blogRepository.List(x => x.IsPublished == true);
            var data = entities.Select(x => new BlogDTO()
            {
                Id = x.Id,
                Title = x.Title,
                CardImageUrl=x.BlogCardImage,
                ShortDescription=x.ShortDescription,
                LastUpdated=x.LastUpdated,
            });
            return data;
        }

        public async Task Edit(BlogDTO data)
        {
            var entity = await _blogRepository.GetById(data.Id);
            entity.Title = data.Title;
            entity.BlogMainText = data.BlogMainText;
            entity.IsPublished = data.IsPublished;
            entity.SecondaryDescription = data.SecondaryDescription;
            entity.ShortDescription = data.ShortDescription;
            entity.LastUpdated= DateTime.Now;
            if (data.VideoFile != null)
            {
                FileManager.DeleteFile(entity.Video);

                entity.Video = await ImageService.UploadFile(data.VideoFile);
            }
            if (data.CardImage != null)
            {
                FileManager.DeleteFile(entity.BlogCardImage);

                entity.BlogCardImage = await ImageService.UploadFile(data.CardImage);
            }

            if (data.Image != null) 
            {
                string images = "";
                int counter = 1;
                if (!string.IsNullOrEmpty(entity.BlogMainImage))
                { 
                    List<ImageInfo> deserializedCollection = JsonSerializer.Deserialize<List<ImageInfo>>(entity.BlogMainImage);
                    foreach (var image in deserializedCollection)
                    {
                       FileManager.DeleteFile(image.ImagePath);
                    }
                }
                List<ImageInfo> imagesList = new List<ImageInfo>();
                foreach (var image in data.Image)
                {
                    ImageInfo imageInfo = new ImageInfo();

                    imageInfo.Name = "image" + counter;
                    imageInfo.ImagePath = await ImageService.UploadFile(image);
                    imagesList.Add(imageInfo);
                    counter++;
                }
                images = JsonSerializer.Serialize(imagesList);
                entity.BlogMainImage = images;
            }
            await _blogRepository.Edit(entity);
        }

        public async Task Delete(int Id) 
        {
            var entity = await _blogRepository.GetById(Id);
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(entity.BlogMainImage))
                {
                    List<ImageInfo> deserializedCollection = JsonSerializer.Deserialize<List<ImageInfo>>(entity.BlogMainImage);
                    foreach (var image in deserializedCollection)
                    {
                        FileManager.DeleteFile(image.ImagePath);
                    }
                }
                await _blogRepository.Delete(entity);
            }
        }

        public async Task Create(BlogDTO data)
        {
            string images = "";
            if (data.Image != null)
            {
                int counter = 1;
              
                List<ImageInfo> imagesList = new List<ImageInfo>();
                foreach (var image in data.Image)
                {
                    ImageInfo imageInfo = new ImageInfo();

                    imageInfo.Name = "image"+counter;
                    imageInfo.ImagePath = await ImageService.UploadFile(image);
                    imagesList.Add(imageInfo);
                    counter++;
                }
                images = JsonSerializer.Serialize(imagesList);
            }

            var entity = new Blog() 
            {
                IsPublished = data.IsPublished,
                BlogMainText = data.BlogMainText,
                Title = data.Title,
                SecondaryDescription = data.SecondaryDescription,
                ShortDescription = data.ShortDescription,
                LastUpdated=DateTime.Now,
            };

            if (data.VideoFile != null)
            {
                entity.Video = await ImageService.UploadFile(data.VideoFile);
            }
            if (data.CardImage != null)
            {
                entity.BlogCardImage = await ImageService.UploadFile(data.CardImage);
            }
            if (!string.IsNullOrEmpty(images))
            {
                entity.BlogMainImage = images;
            }

            await _blogRepository.Add(entity);
        }
    }
}
