using Microsoft.AspNetCore.Http;
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

       

       

        public bool IsPublished { get; set; }

        public bool IsDeleted { get; set; }

       public int? RoomClassId { get; set; }

        public Guid? UserId { get; set; }
        public IFormFile? PackageMainImageFile { get; set; }
        public List<IFormFile>? PackageImages { get; set; }
        //public IFormFile? PackageImage1File { get; set; }
        //public IFormFile? PackageImage2File { get; set; }

        //public IFormFile? PackageImage3File { get; set; }
        public string? AboutPackage { get; set; }
        public List<ImageInfo>? ImageInfo { get; set; }

    }
}
