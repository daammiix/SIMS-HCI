using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Repository;
using Model;
using System;
using System.Collections.ObjectModel;

namespace ClassDijagramV1._0.Service
{
    public class NotificationService
    {
        NotificationRepo _notificationRepo;

        public NotificationService(NotificationRepo notificationRepo)
        {
            _notificationRepo = notificationRepo;
        }

        public void AddNotification(Appointment appointment, Room r1, NotificationType notificationType)
        {
            if (NotificationType.addingAppointment.Equals(notificationType))
            {
                var notificationID = "1";
                var content = "Imate zakazan pregled u " + appointment.AppointmentDate + " u sobi " + r1.RoomName;
                DateTime created = appointment.AppointmentDate;
                Notification n = new Notification(content, appointment.PatientId, false, created, NotificationType.addingAppointment);
                _notificationRepo.AddNotification(n);
            }
            else if (NotificationType.deletingAppointment.Equals(notificationType))
            {
                // _appointmentRepo.RemoveNotification()
                // mora se povezati apojntment sa notifikacijom
            }
        }

        public ObservableCollection<Notification> GetAllNotifications()
        {
            return _notificationRepo.GetAllNotifications();
        }

        public void SaveNotifications()
        {
            _notificationRepo.SaveNotifications();
        }
    }
}
