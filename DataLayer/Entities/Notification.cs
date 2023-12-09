using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Notification
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Title { get; set; }
        
        public string? Description { get; set; }

        public string? Path { get; set; }

        public DateTime CreateDate { get; set; }

        [ForeignKey(nameof(User))]
        public Guid? HotelId { get; set; }

        public virtual AspNetUser? User { get; set; }

    }
}
