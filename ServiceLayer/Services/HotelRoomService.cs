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
	public class HotelRoomService
	{
		private readonly IGenericRepository<HotelRoom> _hotelRoomRepository;

		public HotelRoomService(IUnitOfWorkRepositories unitofworkRepository)
		{
			_hotelRoomRepository = unitofworkRepository.HotelRoomRepository;
		}

        public async Task<(IEnumerable<HotelRoomsDTO> EntityData, int Count)> ListWithPaging(int? page, int? pageSize)
        {
            page = (page - 1) * pageSize;
            (IList<HotelRoom> EntityData, int Count) = await _hotelRoomRepository.ListWithPaging(
                page: page,
                pageSize: pageSize,
                filter: null
                );

            IEnumerable<HotelRoomsDTO> results = EntityData.Select(x => new HotelRoomsDTO
            {
                Id = x.Id,
                HotelName=x.HotelName,
                NumberOfBathrooms = x.NumberOfBathrooms,
                NumberOfBeds = x.NumberOfBeds,
                HotelMainImage = x.HotelMainImage,
                NumberOfSofas = x.NumberOfSofas,
                IsAC=x.IsAC,
                IsRoomHeater=x.IsRoomHeater,
                IsTV=x.IsTV,
                IsWifi=x.IsWifi,
                NumberOfAdults=x.NumberOfAdults,
                NumberOfChildren=x.NumberOfChildren,
                Price=x.Price,
                IsPublished=x.IsPublished,
            }).ToList();

            return (results, Count);
        }

        public async Task Delete(int Id)
        {
            var entity = await _hotelRoomRepository.GetById(Id);
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(entity.HotelMainImage))
                {
                    FileManager.DeleteFile(entity.HotelMainImage);
                }
                if (!string.IsNullOrEmpty(entity.HotelImage1))
                {
                    FileManager.DeleteFile(entity.HotelImage1);
                }
                if (!string.IsNullOrEmpty(entity.HotelImage2))
                {
                    FileManager.DeleteFile(entity.HotelImage2);
                }
                if (!string.IsNullOrEmpty(entity.HotelImage3))
                {
                    FileManager.DeleteFile(entity.HotelImage3);
                }
                await _hotelRoomRepository.Delete(entity);
            }
        }

        public async Task Hide(int Id)
        {
            var entity = await _hotelRoomRepository.GetById(Id);
            entity.IsPublished = false;
            await _hotelRoomRepository.Edit(entity);
        }
        public async Task Show(int Id)
        {
            var entity = await _hotelRoomRepository.GetById(Id);
            entity.IsPublished = true;
            await _hotelRoomRepository.Edit(entity);
        }

		public async Task<(IEnumerable<HotelRoomsDTO> EntityData, int Count)> ListWithPaging(
		string orderBy, int? page, int? pageSize, bool isDescending)
		{
			(IList<HotelRoom> EntityData, int Count) = await _hotelRoomRepository.ListWithPaging(
				page: page,
				pageSize: pageSize,
				filter: x => x.IsPublished == false,
				includeProperties: c => c.Include(x => x.User)
				);

			IEnumerable<HotelRoomsDTO> results = EntityData.Select(x => new HotelRoomsDTO
			{
				Id = x.Id,
				HotelName = x.HotelName,
				HotelDescription = x.HotelDescription,
				IsPublished = x.IsPublished,
				IsAC = x.IsAC,
				IsWifi = x.IsWifi,
				IsRoomHeater = x.IsRoomHeater,
				IsTV = x.IsTV,
				CityId = x.CityId,
				NumberOfBathrooms = x.NumberOfBathrooms,
				NumberOfBeds = x.NumberOfBeds,
				NumberOfSofas = x.NumberOfSofas,
				HotelImage1 = x.HotelImage1,
				HotelImage2 = x.HotelImage2,
				HotelImage3 = x.HotelImage3,
				HotelMainImage = x.HotelMainImage,
				Price = x.Price,
				Discount = x.Discount,
				UserEmail = x.User?.Email,
				UserName = x.User?.FirstName,
				UserPhone = x.User?.PhoneNumber,
			}).ToList();

			return (results, Count);
		}

		public async Task<HotelRoomsDTO> GetById(int Id)
		{
			var entity = await _hotelRoomRepository.GetBy(x => x.Id == Id, includeProperties: c => c.Include(c => c.User));
			var data = new HotelRoomsDTO
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
			var entity = await _hotelRoomRepository.GetById(Id);
			entity.IsPublished = true;
			await _hotelRoomRepository.Edit(entity);
		}

		public async Task Reject(int Id)
		{
			var entity = await _hotelRoomRepository.GetById(Id);
			await _hotelRoomRepository.Delete(entity);
		}

		public async Task UpdatePrice(int id, float price, float discountPrice)
		{
			var entity=await _hotelRoomRepository.GetById(id);
			entity.Price = price;
			entity.Discount = discountPrice;
			await _hotelRoomRepository.Edit(entity);
		}
	}
}
