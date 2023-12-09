using DataLayer.Entities;
using DataLayer.Interfaces;
using DataLayer.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class UnitOfWorkRepositories: IDisposable, IUnitOfWorkRepositories
    {
        private readonly IEfContextFactory _efContextFactory;
        public IGenericRepository<Blog> BlogRepository { get; set; }
        public IGenericRepository<Contact> ContactRepository { get; set; }
        public IGenericRepository<Package> PackageRepository { get; set; }
        public IGenericRepository<PackageOrder> PackageOrderRepository { get; set; }
        public IGenericRepository<HotelRoom> HotelRoomRepository { get; set; }
        public IGenericRepository<ProviderService> ProviderServiceRepository { get; set; }
        public IGenericRepository<Review> ReviewRepository { get; set; }
        public IGenericRepository<RoomsOrder> RoomOrderRepository { get; set; }
        public IGenericRepository<AspNetUser> UsersRepository { get; set; }
        public IGenericRepository<AspNetUserRole> UserRoleRepository { get; set; }
        public IGenericRepository<AspNetRole> RoleRepository { get; set; }
        public IGenericRepository<Notification> NotificationRepository { get; set; }

        public NotificationRepository NotificationRepositoryScroll { get; set; }

        public UnitOfWorkRepositories(IEfContextFactory efContextFactory)
        {
            _efContextFactory = efContextFactory;
            BlogRepository = new GenericRepository<Blog>(efContextFactory);
            ContactRepository = new GenericRepository<Contact>(efContextFactory);
            PackageRepository = new GenericRepository<Package>(efContextFactory);
            PackageOrderRepository = new GenericRepository<PackageOrder>(efContextFactory);
            HotelRoomRepository = new GenericRepository<HotelRoom>(efContextFactory);
            ProviderServiceRepository = new GenericRepository<ProviderService>(efContextFactory);
            ReviewRepository = new GenericRepository<Review>(efContextFactory);
            RoomOrderRepository = new GenericRepository<RoomsOrder>(efContextFactory);
            UsersRepository = new GenericRepository<AspNetUser>(efContextFactory);
            UserRoleRepository = new GenericRepository<AspNetUserRole>(efContextFactory);
            RoleRepository = new GenericRepository<AspNetRole>(efContextFactory);
            NotificationRepository= new GenericRepository<Notification>(efContextFactory);
            NotificationRepositoryScroll = new NotificationRepository(efContextFactory);
        }


        public void Dispose()
        {
        }
    }
}
