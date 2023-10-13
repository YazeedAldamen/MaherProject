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
    }
}
