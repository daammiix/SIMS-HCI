using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Service;
using Model;
using System.Collections.ObjectModel;

namespace ClassDijagramV1._0.Controller
{
    public class NotificationController
    {
        private NotificationService _notificationService;

        public NotificationController(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public void AddNotification(Appointment appointment, Room r1, NotificationType notificationType)
        {
            _notificationService.AddNotification(appointment, r1, notificationType);
        }

        public ObservableCollection<Notification> GetAllNotifications()
        {
            return _notificationService.GetAllNotifications();
        }

        public void SaveNotifications()
        {
            _notificationService.SaveNotifications();
        }
    }
}
