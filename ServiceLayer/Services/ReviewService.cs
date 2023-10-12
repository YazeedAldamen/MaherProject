using DataLayer.Entities;
using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
	public class ReviewService
	{
		private readonly IGenericRepository<Review> _reviewRepository;

		public ReviewService(IUnitOfWorkRepositories unitofworkRepository)
		{
			_reviewRepository = unitofworkRepository.ReviewRepository;
		}
	}
}
