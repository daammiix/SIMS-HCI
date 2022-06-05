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

        public void AddManualNotification(Notification notification)
        {
            _notificationRepo.AddNotification(notification);
        }

        public void AddNotificationForAppointment(Appointment appointment)
        {
                var content = "Imate zakazan pregled " + appointment.AppointmentDate + " u sobi " + appointment.RoomId;
                Notification n = new Notification(content, appointment.PatientId, false, appointment.AppointmentDate, appointment.Id);
                _notificationRepo.AddNotification(n);
        }

        public void RemoveNotificationByAppointment(int appointmentID)
        {
            _notificationRepo.RemoveNotification(FindNotificationByAppointmentID(appointmentID));
        }

        public void RemoveReadNotification(Notification note)
        {
            _notificationRepo.RemoveNotification(note);
        }

        public void RemoveOldNotification(Notification note)
        {
            _notificationRepo.RemoveNotification(note);
        }

        public void AddNotificationForTherapy(Notification notification)
        {
            _notificationRepo.AddNotification(notification);
        }

        public ObservableCollection<Notification> GetAllNotifications()
        {
            return _notificationRepo.GetAllNotifications();
        }

        public Notification FindNotificationByAppointmentID(int appointmentID)
        {
            foreach (Notification notification in _notificationRepo.GetAllNotifications())
            {
                if (notification.AppointmentID == appointmentID)
                {
                    return notification;
                }
            }
            return null;
        }

        public void SaveNotifications()
        {
            _notificationRepo.SaveNotifications();
        }
    }
}
