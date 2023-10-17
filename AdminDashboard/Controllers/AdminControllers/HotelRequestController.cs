using AdminDashboard.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using ServiceLayer.DTO;
using ServiceLayer.Services;

namespace AdminDashboard.Controllers.AdminControllers
{
    public class HotelRequestController : BaseController
    {
        private readonly ProviderServiceService _providerService;

        public HotelRequestController(UnitOfWorkServices unitOfWorkServices)
        {
            _providerService = unitOfWorkServices.ProviderService;
        }

        
    }
}
