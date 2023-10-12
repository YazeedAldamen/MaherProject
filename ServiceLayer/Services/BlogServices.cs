using DataLayer.Entities;
using DataLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ServiceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
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
            };
            return data;
        }

        public async Task Edit(BlogDTO data)
        {
            var entity = await _blogRepository.GetById(data.Id);
            entity.Title = data.Title;
            entity.BlogMainText = data.BlogMainText;
            entity.IsPublished = data.IsPublished;
            if (data.Image != null) 
            {
                if (!string.IsNullOrEmpty(entity.BlogMainImage))
                {
                    FileManager.DeleteFile(entity.BlogMainImage);
                }
                entity.BlogMainImage = await ImageService.UploadFile(data.Image);
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
                    FileManager.DeleteFile(entity.BlogMainImage);
                }
                await _blogRepository.Delete(entity);
            }
        }

        public async Task Create(BlogDTO data)
        {
            var entity = new Blog() 
            {
                IsPublished = data.IsPublished,
                BlogMainText = data.BlogMainText,
                Title = data.Title,            
            };
            if (data.Image != null)
            {
                entity.BlogMainImage = await ImageService.UploadFile(data.Image);
            }

            await _blogRepository.Add(entity);
        }
    }
}
