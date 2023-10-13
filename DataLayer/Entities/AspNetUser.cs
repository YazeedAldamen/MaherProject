using Microsoft.AspNetCore.Identity;

namespace DataLayer.Entities
{
    public class AspNetUser:IdentityUser<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<PackageOrder> packageOrders { get; set; } = new List<PackageOrder>();
        public ICollection<Package> Packages { get; set; } = new List<Package>();
        public ICollection<HotelRoom> HotelRooms { get; set; } = new List<HotelRoom>();
        public ICollection<RoomsOrder> RoomsOrders { get; set; } = new List<RoomsOrder>();
        public ICollection<ProviderService> providerServices { get; set; } = new List<ProviderService>();
        public ICollection<AspNetUserRole> AspNetUserRole { get; set;} = new List<AspNetUserRole>();
        public ICollection<AspNetRole> AspNetRole { get; set; } = new List<AspNetRole>();
    }
}
