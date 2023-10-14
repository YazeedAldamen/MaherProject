using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Shared
{
    public class GuidRoles
    {
        public static Dictionary<string, Guid> Roles = new Dictionary<string, Guid>()
        {
            {"Admin", Guid.Parse("1c2a1e4b-6a4f-4c8a-bf8a-5fbd6b3f58b1")},
            {"User", Guid.Parse("2d3b2e5c-7b5d-4d9c-af9c-6ece7c4f69c2")},
            {"Provider", Guid.Parse("3e4d3f6d-8c6e-4eaf-bd8a-7fde8d5e79a3")}
        };
    }
}
