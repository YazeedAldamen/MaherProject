using DataLayer.Interfaces;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class UnitOfWorkServices
    {
        private readonly IUnitOfWorkRepositories _unitOfWorkRepository;

        private BlogServices _blogServices;
        private ContactService _contactServices;
        private PackageServices _packageServices;
        private HotelRoomService _hotelRoomService;
        private ImageService _imageServices;
        private ProviderServiceService _providerServiceService;
        private UsersService _usersServices;

        public UnitOfWorkServices(IUnitOfWorkRepositories unitOfWorkRepository, ImageService imageServices = null)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
            _imageServices = imageServices;
        }

        public BlogServices BlogServices =>_blogServices??=new BlogServices(_unitOfWorkRepository);
        public HotelRoomService HotelRoomService=>_hotelRoomService??new HotelRoomService(_unitOfWorkRepository);
		public PackageServices PackageServices => _packageServices ??= new PackageServices(_unitOfWorkRepository,_imageServices);
        public ProviderServiceService ProviderService=>_providerServiceService??new ProviderServiceService(_unitOfWorkRepository);
        public UsersService UsersServices => _usersServices ??= new UsersService(_unitOfWorkRepository);

	}
}
