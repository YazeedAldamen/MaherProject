using DataLayer.Entities;
using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
	public class ContactService
	{
		private readonly IGenericRepository<Contact> _contactRepository;

		public ContactService(IUnitOfWorkRepositories unitofworkRepository)
		{
			_contactRepository = unitofworkRepository.ContactRepository;
		}
	}
}
