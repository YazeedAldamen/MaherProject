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
	public class ContactService
	{
		private readonly IGenericRepository<Contact> _contactRepository;

		public ContactService(IUnitOfWorkRepositories unitofworkRepository)
		{
			_contactRepository = unitofworkRepository.ContactRepository;
		}

        public async Task<(IEnumerable<ContactDTO> EntityData, int Count)> ListWithPaging(
        string orderBy, int? page, int? pageSize, bool isDescending)
        {
            Func<IQueryable<Contact>, IOrderedQueryable<Contact>> orderByExpression;

            switch (orderBy)
            {
                case "Id":
                    orderByExpression = query => isDescending ? query.OrderByDescending(entity => entity.Id) : query.OrderBy(entity => entity.Id);
                    break;
                case "Name":
                    orderByExpression = query => isDescending ? query.OrderByDescending(entity => entity.Name) : query.OrderBy(entity => entity.Name);
                    break;
                case "Email":
                    orderByExpression = query => isDescending ? query.OrderByDescending(entity => entity.Email) : query.OrderBy(entity => entity.Email);
                    break;
                case "Message":
                    orderByExpression = query => isDescending ? query.OrderByDescending(entity => entity.Message) : query.OrderBy(entity => entity.Message);
                    break;
                default:
                    orderByExpression = query => query.OrderBy(entity => entity.Id);
                    break;
            }

            (IList<Contact> EntityData, int Count) = await _contactRepository.ListWithPaging(
                orderBy: orderByExpression,
                page: page,
                pageSize: pageSize,
                filter: null
            );

            IEnumerable<ContactDTO> results = EntityData.Select(x => new ContactDTO
            {
                Id = x.Id,
                Name= x.Name,
                Email= x.Email,
                Message= x.Message,
            }).ToList();

            return (results, Count);
        }

        public async Task CreateContact(ContactDTO data)
        {
            Contact contact = new Contact();
            contact.Name = data.Name;
            contact.Email = data.Email;
            contact.Message = data.Message;
            await _contactRepository.Add(contact);
        }
    }
}
