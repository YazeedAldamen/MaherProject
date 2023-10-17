using DataLayer.Entities;
using DataLayer.Interfaces;
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
	public class ProviderServiceService
	{
		private readonly IGenericRepository<ProviderService> _providerServiceRepository;
		public ProviderServiceService(IUnitOfWorkRepositories unitofworkRepository)
		{
			_providerServiceRepository = unitofworkRepository.ProviderServiceRepository;
		}

        
    }
}
