using DataLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<PackageOrder> PackageOrders { get; set; }
        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<HotelRoom> HotelRooms { get; set; }
        public virtual DbSet<ProviderService> ProviderServices { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<RoomsOrder> RoomsOrders { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<PackageType> PackageTypes { get; set; }
    }
}
