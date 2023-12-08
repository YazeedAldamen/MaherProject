using DataLayer.Entities;
using ServiceLayer.DTO;
using System.ComponentModel.DataAnnotations;

namespace AdminDashboard.Models
{
    public class PackageModel
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

        public Guid? UserId { get; set; }
        public IFormFile? PackageMainImageFile { get; set; }
        public List<IFormFile>? PackageImages { get; set; }

        public string? AboutPackage { get; set; }
        public int? RoomClassId { get; set; }

        public List<ImageInfo>? ImageInfo { get; set; }
    }
}
