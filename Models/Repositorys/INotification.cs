using Microsoft.EntityFrameworkCore;

namespace Mvc_Project.Models.Repositorys
{
    public interface INotification
    {
        public void AddNotification(int userId, string message);
       

        public List<Notification> GetUserNotifications(int userId);



        public void MarkAsRead(int notificationId);
        public IEnumerable<Notification> GetReadNotifications(int userId);


    }
}
