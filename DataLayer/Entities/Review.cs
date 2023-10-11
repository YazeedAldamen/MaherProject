using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Review
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? UserId { get; set; }
        public virtual AspNetUser? User { get; set; }
        public int? HotelId { get; set; }
        public virtual HotelRoom? Hotel { get; set; }
        public int? PackageId { get; set; }
        public virtual Package? Package { get; set; }
        public string? ReviewText { get; set; }
    }
}
