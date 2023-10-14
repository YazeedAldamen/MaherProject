using DataLayer.Entities;
using Microsoft.AspNetCore.Identity;

namespace AdminDashboard.Models
{
    public class AdminUsersModel : IdentityUser<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserType { get; set; }
    }
}
