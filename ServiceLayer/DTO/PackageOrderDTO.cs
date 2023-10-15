using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTO
{
    public class PackageOrderDTO
    {
        public int Id { get; set; }
        public Guid? UserId { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public int? NumberOfAdults { get; set; }
        public int? NumberOfChildren { get; set; }
        public string? PaymentMethod { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPhone { get; set;}
        public string? City { get; set; }
        public string? PackageName { get; set; }
    }
}
