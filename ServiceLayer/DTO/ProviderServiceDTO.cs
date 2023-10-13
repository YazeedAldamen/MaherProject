using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTO
{
    public class ProviderServiceDTO
    {
        public int Id { get; set; }

        public string? HotelName { get; set; }

        public string? HotelDescription { get; set; }

        public float? Price { get; set; }

        public float? Discount { get; set; }

        public string? HotelMainImage { get; set; }

        public string? HotelImage1 { get; set; }

        public string? HotelImage2 { get; set; }

        public string? HotelImage3 { get; set; }

        public int? CityId { get; set; }

        public int? NumberOfBeds { get; set; }

        public int? NumberOfSofas { get; set; }

        public int? NumberOfBathrooms { get; set; }

        public int? NumberOfRooms { get; set; }

        public bool IsPublished { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsWifi { get; set; }

        public bool IsTV { get; set; }

        public bool IsAC { get; set; }

        public bool IsRoomHeater { get; set; }

        public bool IsShowNotification { get; set; }

        public string? UserName { get; set; }

        public string? UserEmail { get; set; }

        public string? UserPhone { get; set; }
    }
}
