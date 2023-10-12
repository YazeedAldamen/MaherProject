using DataLayer.Entities;
using DataLayer.Interfaces;
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
    }
}
