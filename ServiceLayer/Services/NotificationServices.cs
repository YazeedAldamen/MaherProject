using DataLayer.Entities;
using DataLayer.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
                case "contact":
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
                notification.Seen = false;
                await _notificationRepository.Add(notification);
            }

        }

        public async Task<(List<NotificationDTO> data,int Count)> GetNotificationList(int page=0, int pageSize=10)
        {
            page = (page - 1) * pageSize;

            Func<IQueryable<Notification>, IOrderedQueryable<Notification>> orderByExpression;
            orderByExpression = q => q.OrderBy(x => x.Seen).ThenByDescending(x => x.CreateDate);

            var notifications = await _notificationRepository.ListWithPaging(page: page, pageSize: pageSize, orderBy:orderByExpression);

            var notificationDTO = notifications.EntityData.Select(x => new NotificationDTO 
            {   Id=x.Id,
                Title=x.Title,
                Description=x.Description,
                CreateDate=x.CreateDate,
                Path=x.Path
            }).ToList();
            return (notificationDTO, notifications.Count);
        }

        public async Task ChangeStatus(int id)
        { 
            var entity= await _notificationRepository.GetById(id);
            if (entity == null || entity.Seen) 
            {
                return;
            }
            entity.Seen = true;
            await _notificationRepository.Edit(entity);
        }
    }
}
