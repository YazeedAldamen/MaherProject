using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTO
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public Guid? UserId { get; set; }
        public int? HotelId { get; set; }
        public int? PackageId { get; set; }
        public string? ReviewText { get; set; }
    }
}
