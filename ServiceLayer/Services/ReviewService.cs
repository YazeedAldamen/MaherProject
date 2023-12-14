using DataLayer.Entities;
using DataLayer.Interfaces;
using DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class ReviewService
    {
        private readonly ReviewsRepository _reviewsRepository;

        public ReviewService(IUnitOfWorkRepositories unitOfWorkRepositories)
        {
            _reviewsRepository = unitOfWorkRepositories.ReviewsRepository;
        }

        public async Task AddPackageReview(int Id, string ReviewText,Guid UserId) 
        {
            Review review = new Review()
            {
                PackageId = Id,
                ReviewText = ReviewText,
                UserId = UserId
            };
            await _reviewsRepository.Add(review);
        }

        public async Task<(List<ReviewDTO> data, int Count)> GetReviews(int id,int page = 0, int pageSize = 10)
        {
            Func<IQueryable<Review>, IOrderedQueryable<Review>> orderByExpression;
            orderByExpression = q => q.OrderByDescending(x => x.CreateDate);

            Expression<Func<Review, bool>> filterExpression = x =>
            (id == 0 || x.PackageId.Equals(id));

            var reviews = await _reviewsRepository.ListWithPaging(
                page: page,
                pageSize: pageSize,
                orderBy: orderByExpression,
                filter:filterExpression,
                includeProperties:x=>x.Include(y=>y.User));

            var reviewDTO = reviews.EntityData.Select(x => new ReviewDTO
            {
                Id = x.Id,
                Name=x.User?.FirstName+" "+x.User?.LastName,
                CreatedDate=x.CreateDate,
                ReviewText=x.ReviewText,
                UserImage=x.User?.ProfileImage
            }).ToList();
            return (reviewDTO, reviews.Count);
        }
    }
}
