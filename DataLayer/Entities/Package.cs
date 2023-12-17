using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Package : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Name { get; set; }
        [ForeignKey(nameof(PackageType))]

        public int? PackageTypeId { get; set; }

        public float? Price { get; set; }
        public string? AboutPackage { get; set; }

        public float? Discount { get; set; }

        public string? Description { get; set; }

        public int? NumberOfDays { get; set; }

        public int? NumberOfNights { get; set; }

        public string? PackageMainImage { get; set; }

        public string? PackageImage1 { get; set; }
        public bool IsPublished { get; set; }

        public bool IsDeleted { get; set; }
        [ForeignKey(nameof(User))]

        public Guid? UserId { get; set; }
        public virtual AspNetUser? User { get; set; }
        public virtual PackageType? PackageType { get; set; }
        public string? PackageDetails { get; set; }
        [ForeignKey(nameof(RoomClass))]
        public int? RoomClassId { get; set; }

        public virtual RoomClass? RoomClass { get; set; }

        public int? TopTen { get; set; }

    }
}
