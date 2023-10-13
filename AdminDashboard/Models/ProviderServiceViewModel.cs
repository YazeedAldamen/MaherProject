using System.ComponentModel.DataAnnotations;

namespace AdminDashboard.Models
{
    public class ProviderServiceViewModel
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Email { get; set; }

        [Display(Name="Phone Number")]
        public string PhoneNumber { get; set; }

    }
}
