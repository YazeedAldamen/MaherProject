using Microsoft.AspNetCore.Identity;

namespace DataLayer.Entities
{
    public class AspNetUser:IdentityUser<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
