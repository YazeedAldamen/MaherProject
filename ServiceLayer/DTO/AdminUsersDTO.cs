using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTO
{
    public class AdminUsersDTO : IdentityUser<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserType { get; set; }
        
    }
}
