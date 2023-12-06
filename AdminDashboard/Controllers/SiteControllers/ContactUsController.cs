using AdminDashboard.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using ServiceLayer.DTO;
using ServiceLayer.Services;

namespace AdminDashboard.Controllers.SiteControllers
{
    public class ContactUsController : BaseController
    {
        private readonly ContactService _contactServices;

        public ContactUsController(UnitOfWorkServices unitOfWorkServices)
        {
            _contactServices = unitOfWorkServices.ContactService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(ContactModel model) 
        {
            try 
            { 
                if (!ModelState.IsValid) 
                {
                    return View(model);
                }
                ContactDTO data = new ContactDTO();
                data.Name = model.Name;
                data.Email = model.Email;
                data.Message = model.Message;
                await _contactServices.CreateContact(data);
                ShowSuccessMessage("Message Sent Successfully");
            }
            catch (Exception ex) 
            {
                ShowErrorMessage("Something Went Wrong , Please try again later");
            }
            return View();
        }
    }
}
