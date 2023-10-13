using DataLayer.Entities;
using ServiceLayer.DTO;

namespace AdminDashboard.Models
{
    public class RoomListViewModel
    {
        public List<HotelRoomsDTO> Rooms { get; set; }
        public int TotalCount { get; set; }
    }
}
