namespace Mvc_Project.Models.Repositorys
{
    public class NotificationRepository : INotification
    {

        private readonly Context _context;

        public NotificationRepository(Context context)
        {
            _context = context;
        }

        public void AddNotification(int userId, string message)
        {
            var notification = new Notification
            {
                UserId = userId,
                Message = message,
                CreatedAt = DateTime.Now
            };

            _context.Notifications.Add(notification);
            _context.SaveChanges();
        }

        public List<Notification> GetUserNotifications(int userId)
        {
            return _context.Notifications
                .Where(n => n.UserId == userId && !n.IsRead)
                .OrderByDescending(n => n.CreatedAt)
                .ToList();
        }

        public void MarkAsRead(int notificationId)
        {
            var notification = _context.Notifications.Find(notificationId);
            if (notification != null)
            {
                notification.IsRead = true;
                _context.SaveChanges();
            }
        }

        public IEnumerable<Notification> GetReadNotifications(int userId)
        {
            return _context.Notifications
                .Where(n => n.UserId == userId && n.IsRead)
                .ToList();
        }
    }
}
