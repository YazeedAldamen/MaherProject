using DataLayer.Entities;
using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IUnitOfWorkRepositories
    {
        IGenericRepository<Blog> BlogRepository { get; }
        IGenericRepository<Contact> ContactRepository { get; }
        IGenericRepository<Package> PackageRepository { get; }
        IGenericRepository<PackageOrder> PackageOrderRepository { get; }
        IGenericRepository<HotelRoom> HotelRoomRepository { get; }
        IGenericRepository<ProviderService> ProviderServiceRepository { get; }
        IGenericRepository<RoomsOrder> RoomOrderRepository { get; }
        IGenericRepository<AspNetUser> UsersRepository { get; }
        IGenericRepository<AspNetUserRole> UserRoleRepository { get; }
        IGenericRepository<AspNetRole> RoleRepository { get; }
        NotificationRepository NotificationRepository { get; }
        SitePackagesRepository SitePackagesRepository { get; }
        SiteBlogsRepository SiteBlogsRepository { get; }
        ReviewsRepository ReviewsRepository { get; }
    }
}
