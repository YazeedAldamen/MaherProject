using DataLayer.Entities;
using DataLayer.Interfaces;
using Microsoft.AspNetCore.Identity;
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
        private AdminUsersService _adminUsersService;
        private UserManager<AspNetUser> _userManager;
        private RoleManager<AspNetRole> _roleManager;


        public UnitOfWorkServices(IUnitOfWorkRepositories unitOfWorkRepository, UserManager<AspNetUser> userManager, RoleManager<AspNetRole> roleManager, ImageService imageServices = null)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
            _imageServices = imageServices;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public BlogServices BlogServices =>_blogServices??=new BlogServices(_unitOfWorkRepository);
        public HotelRoomService HotelRoomService=>_hotelRoomService??new HotelRoomService(_unitOfWorkRepository);
		public PackageServices PackageServices => _packageServices ??= new PackageServices(_unitOfWorkRepository,_imageServices);
        public ProviderServiceService ProviderService=>_providerServiceService??new ProviderServiceService(_unitOfWorkRepository);
        public AdminUsersService AdminUsersService=>_adminUsersService??new AdminUsersService(_unitOfWorkRepository,_userManager,_roleManager);
	}
}
