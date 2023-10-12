using DataLayer.Entities;
using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
	public class PackageServices
	{
		private readonly IGenericRepository<Package> _packageRepository;

		public PackageServices(IUnitOfWorkRepositories unitofworkRepository)
		{
			_packageRepository = unitofworkRepository.PackageRepository;
		}
	}
}
