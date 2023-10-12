using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTO
{
    public class PackageDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int? PackageTypeId { get; set; }

        public float? Price { get; set; }

        public float? Discount { get; set; }

        public string? Description { get; set; }

        public int? NumberOfDays { get; set; }

        public int? NumberOfNights { get; set; }

        public string? PackageMainImage { get; set; }

        public string? PackageImage1 { get; set; }

        public string? PackageImage2 { get; set; }

        public string? PackageImage3 { get; set; }

        public string? HotelName { get; set; }

        public int? NumberOfAdults { get; set; }

        public int? NumberOfChildren { get; set; }

        public string? HotelDescription { get; set; }

        public string? HotelMainImage { get; set; }

        public string? HotelImage1 { get; set; }

        public string? HotelImage2 { get; set; }

        public string? HotelImage3 { get; set; }

        public int? NumberOfBeds { get; set; }

        public int? NumberOfSofas { get; set; }

        public int? NumberOfBathrooms { get; set; }

        public bool IsPublished { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsVip { get; set; }

        public bool IsWifi { get; set; }

        public bool IsTV { get; set; }

        public bool IsAC { get; set; }
        public bool IsRoomHeater { get; set; }

        public Guid? UserId { get; set; }
    }
}
