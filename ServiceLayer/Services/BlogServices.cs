using DataLayer.Entities;
using DataLayer.Interfaces;
using ServiceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class BlogServices
    {
        private readonly IGenericRepository<Blog> _blogRepository;

        public BlogServices(IUnitOfWorkRepositories genericRepository)
        {
            _blogRepository=genericRepository.BlogRepository;
        }

        public async Task<IEnumerable<BlogModel>> GetAllBlogs()
        {
            var allBlogs = await _blogRepository.GetAllAsync();

            IEnumerable<BlogModel> data = allBlogs.Select(x => new BlogModel
            {
                Id=x.Id,
                Title=x.Title,
                BlogMainImage=x.BlogMainImage,
                BlogMainText=x.BlogMainText,
                IsDeleted=x.IsDeleted,
                IsPublished=x.IsPublished,
            }).ToList();
            return data;
        }
    }
}
