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
        public UnitOfWorkServices(IUnitOfWorkRepositories unitOfWorkRepository) {
            _unitOfWorkRepository = unitOfWorkRepository;
        }

        public BlogServices BlogServices =>_blogServices??=new BlogServices(_unitOfWorkRepository);
        public HotelRoomService HotelRoomService=>_hotelRoomService??new HotelRoomService(_unitOfWorkRepository);

    }
}
