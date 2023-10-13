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

        public async Task<(IEnumerable<ProviderServiceDTO> EntityData, int Count)> ListWithPaging(
        string orderBy, int? page, int? pageSize, bool isDescending)
        {
            (IList<ProviderService> EntityData, int Count) = await _providerServiceRepository.ListWithPaging(
                page: page,
                pageSize: pageSize,
                filter:x=>x.IsPublished==false,
                includeProperties:c=>c.Include(c=>c.User)
                );

            IEnumerable<ProviderServiceDTO> results = EntityData.Select(x => new ProviderServiceDTO
            {
                Id = x.Id,
                HotelName = x.HotelName,
                HotelDescription = x.HotelDescription,
                IsPublished = x.IsPublished,
                IsAC=x.IsAC,
                IsWifi=x.IsWifi,
                IsRoomHeater=x.IsRoomHeater,
                IsTV=x.IsTV,
                CityId=x.CityId,
                NumberOfBathrooms=x.NumberOfBathrooms,
                NumberOfBeds=x.NumberOfBeds,
                NumberOfRooms=x.NumberOfRooms,
                NumberOfSofas=x.NumberOfSofas,
                HotelImage1 = x.HotelImage1,
                HotelImage2 = x.HotelImage2,
                HotelImage3 = x.HotelImage3,
                HotelMainImage = x.HotelMainImage,
                Price = x.Price,
                Discount = x.Discount,
                UserEmail=x.User.Email,
                UserName=x.User.FirstName,
                UserPhone=x.User.PhoneNumber,
            }).ToList();

            return (results, Count);
        }

        public async Task<ProviderServiceDTO> GetById(int Id) 
        {
            var entity=await _providerServiceRepository.GetBy(x=>x.Id==Id,includeProperties:c=>c.Include(c=>c.User));
            var data = new ProviderServiceDTO 
            {
                Id = entity.Id,
                HotelName = entity.HotelName,
                HotelDescription = entity.HotelDescription,
                IsPublished = entity.IsPublished,
                IsAC = entity.IsAC,
                IsWifi = entity.IsWifi,
                IsRoomHeater = entity.IsRoomHeater,
                IsTV = entity.IsTV,
                CityId = entity.CityId,
                NumberOfBathrooms = entity.NumberOfBathrooms,
                NumberOfBeds = entity.NumberOfBeds,
                NumberOfRooms = entity.NumberOfRooms,
                NumberOfSofas = entity.NumberOfSofas,
                HotelImage1 = entity.HotelImage1,
                HotelImage2 = entity.HotelImage2,
                HotelImage3 = entity.HotelImage3,
                HotelMainImage = entity.HotelMainImage,
                Price = entity.Price,
                Discount = entity.Discount,
                UserEmail = entity.User.Email,
                UserName = entity.User.FirstName,
                UserPhone = entity.User.PhoneNumber,

            };

            return data;
        }

        public async Task Accept(int Id) 
        {
            var entity=await _providerServiceRepository.GetById(Id);
            entity.IsPublished = true;
            await _providerServiceRepository.Edit(entity);
        }

        public async Task Reject(int Id)
        {
            var entity = await _providerServiceRepository.GetById(Id);
            await _providerServiceRepository.Delete(entity);
        }
    }
}
