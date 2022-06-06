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

        /// <summary>
        /// Dodaje manuelno napravljeni podsjetnik
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        public void AddManualNotification(Notification notification)
        {
            _notificationService.AddManualNotification(notification);
        }

        /// <summary>
        /// Dodaje notifikaciju kad zakayujemo pregled
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns></returns>
        public void AddNotificationForAppointment(Appointment appointment)
        {
            _notificationService.AddNotificationForAppointment(appointment);
        }

        /// <summary>
        /// Brise notifikaciju kad otkayujemo pregled
        /// </summary>
        /// <param name="appointmentID"></param>
        /// <returns></returns>
        public void RemoveNotificationByAppointment(int appointmentID)
        {
            _notificationService.RemoveNotificationByAppointment(appointmentID);
        }

        /// <summary>
        /// Dodaje notifikaciju za terapiju
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns></returns>
        public void AddNotificationForTherapy(Notification notification)
        {
            _notificationService.AddNotificationForTherapy(notification);
        }

        /// <summary>
        /// Brise notifikaciju ako je procitana
        /// </summary>
        /// <param name="appointmentID"></param>
        /// <returns></returns>
        public void RemoveReadNotification()
        {
            _notificationService.RemoveReadNotification();
        }

        /// <summary>
        /// Brise notifikaciju kad prodje jer je stara
        /// </summary>
        /// <param name="appointmentID"></param>
        /// <returns></returns>
        public void RemoveOldNotification()
        {
            _notificationService.RemoveOldNotification();
        }

        /// <summary>
        /// Vraca sve notifikacije
        /// </summary>
        /// <returns></returns>
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
