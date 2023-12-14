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
        public string Name { get; set; }
        public string? ReviewText { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UserImage { get; set; }
    }
}
