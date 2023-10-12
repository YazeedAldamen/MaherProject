using DataLayer.Entities;
using DataLayer.Interfaces;
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
	}
}
