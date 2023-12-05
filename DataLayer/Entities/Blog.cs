using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Blog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? BlogMainImage { get; set; }
        public string? BlogMainText { get; set; }
        public string? SecondaryDescription { get; set; }
        public string? Video { get; set; }
        public bool IsPublished { get; set; }
        public bool IsDeleted { get; set; }
    }
}
