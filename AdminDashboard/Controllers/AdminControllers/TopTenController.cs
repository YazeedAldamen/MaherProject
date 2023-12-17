using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services;
using ServiceLayer;

namespace AdminDashboard.Controllers.AdminControllers
{
    public class TopTenController : BaseController
    {
        private readonly PackageServices _packageServices;
        public TopTenController(UnitOfWorkServices unitOfWorkServices)
        {
            _packageServices = unitOfWorkServices.PackageServices;
        }
    }
}
