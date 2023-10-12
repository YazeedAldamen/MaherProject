using DataLayer.Entities;
using DataLayer.Interfaces;
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

	}
}
