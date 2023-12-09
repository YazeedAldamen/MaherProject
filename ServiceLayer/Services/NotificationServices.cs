using DataLayer.Entities;
using DataLayer.Interfaces;
using ServiceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class NotificationServices
    {
        private readonly IGenericRepository<Notification> _notificationRepository;

        public NotificationServices (IUnitOfWorkRepositories unitOfWorkRepositories)
        {
            _notificationRepository = unitOfWorkRepositories.NotificationRepository;
        }

        public async Task CreateNotification(string ControllerName,string Description,Guid? HotelId=null)
        {
            ControllerName = ControllerName.ToLower().Replace("controller", "");
            Notification notification=new Notification();
            switch (ControllerName)
            {
                case "contactus":
                    notification.Title = "A New Contact Message Has Been Sent";
                    notification.Description= Description;
                    notification.HotelId = HotelId;
                    notification.Path = ControllerName+"/Index";
                break;

                default:
                break;
            }
            if (notification != null)
            {
                notification.CreateDate= DateTime.Now;
                await _notificationRepository.Add(notification);
            }

        }
    }
}
