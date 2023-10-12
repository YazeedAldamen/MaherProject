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
        public int? PackageId { get; set; }
        public virtual Package? Package { get; set; }
        public Guid? UserId { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public int? NumberOfAdults { get; set; }
        public int? NumberOfChildren { get; set; }
        public string? PaymentMethod { get; set; }
    }
}
