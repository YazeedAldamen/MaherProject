using DataLayer.Entities;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
	public class PackageOrderService
	{
		private readonly IGenericRepository<PackageOrder> _packageOrderRepository;

		public PackageOrderService(IUnitOfWorkRepositories unitofworkRepository)
		{
			_packageOrderRepository = unitofworkRepository.PackageOrderRepository;
		}

        public async Task<(IEnumerable<PackageOrderDTO> EntityData, int Count)> ListWithPaging(string orderBy, int? page, int? pageSize, bool isDescending)
        {
            (IList<PackageOrder> EntityData, int Count) = await _packageOrderRepository.ListWithPaging(
                page: page,
                pageSize: pageSize,
                filter: null,
                includeProperties: x => x.Include(x => x.User).Include(x=>x.Package)
                );

            IEnumerable<PackageOrderDTO> results = EntityData.Select(x => new PackageOrderDTO
            {
                Id = x.Id,
                UserName=x.User.FirstName,
                UserEmail=x.User.Email,
                UserPhone=x.User.PhoneNumber,
                CheckIn = x.CheckIn,
                CheckOut = x.CheckOut,
                NumberOfAdults=x.NumberOfAdults,
                NumberOfChildren=x.NumberOfChildren,    
                PaymentMethod=x.PaymentMethod,
                PackageName=x.Package.Name
            }).ToList();

            return (results, Count);
        }

    }
}
