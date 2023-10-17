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
	public class RoomsOrderService
	{
		private readonly IGenericRepository<RoomsOrder> _roomsOrderRepository;

        public RoomsOrderService(IUnitOfWorkRepositories unitofworkRepository)
        {
            _roomsOrderRepository = unitofworkRepository.RoomOrderRepository;
        }

        public async Task<(IEnumerable<RoomsOrderDTO> EntityData, int Count)> ListWithPaging(string orderBy, int? page, int? pageSize, bool isDescending)
        {
            (IList<RoomsOrder> EntityData, int Count) = await _roomsOrderRepository.ListWithPaging(
                page: page,
                pageSize: pageSize,
                filter: null,
                includeProperties: x => x.Include(x => x.User).Include(x => x.Room)
                );

            IEnumerable<RoomsOrderDTO> results = EntityData.Select(x => new RoomsOrderDTO
            {
                Id = x.Id,
                UserName = x.User.FirstName,
                UserEmail = x.User.Email,
                UserPhone = x.User.PhoneNumber,
                CheckIn = x.CheckIn,
                CheckOut = x.CheckOut,
                NumberOfAdults = x.NumberOfAdults,
                NumberOfChildren = x.NumberOfChildren,
                PaymentMethod = x.PaymentMethod,
                HotelName = x.Room.HotelName
            }).ToList();

            return (results, Count);
        }
    }
}
