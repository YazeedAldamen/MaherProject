using ServiceLayer.DTO;

namespace AdminDashboard.Models
{
    public class NotificationListModel
    {
        public List<NotificationDTO> NotificationsDTO { get; set; }
        public int TotalCount { get; set; }
    }
}
