using ClassDijagramV1._0.FileHandlers;
using ClassDijagramV1._0.Model;
using System.Collections.ObjectModel;
using System.Linq;

namespace ClassDijagramV1._0.Repository
{
    public class NotificationRepo
    {
        private FileHandler<Notification> _notificationFileHandler;

        public ObservableCollection<Notification> Notifications;
        public NotificationRepo(FileHandler<Notification> fileHandler)
        {
            _notificationFileHandler = fileHandler;
            Notifications = new ObservableCollection<Notification>(_notificationFileHandler.GetItems());
        }

        public void AddNotification(Notification n)
        {
            Notifications.Add(n);
        }

        public void RemoveNotification(Notification note)
        {
            Notifications.Remove(note);
        }


        public ObservableCollection<Notification> GetAllNotifications()
        {
            return Notifications;
        }

        public void SaveNotifications()
        {
            _notificationFileHandler.SaveItems(Notifications.ToList());
        }
    }
}
