using DataLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DataLayer.DbContext
{
    public class MainDbContext:IdentityDbContext<AspNetUser, AspNetRole, Guid, AspNetUserClaim, AspNetUserRole, AspNetUserLogin, AspNetRoleClaim, AspNetUserToken>
    {
        public MainDbContext()
    {
    }

    public MainDbContext(IMySqlDbContextOptionsFactory dbContextOptionsFactory)
        : base(dbContextOptionsFactory.OptionsBuilder.Options)
    {
    }
    }
}
