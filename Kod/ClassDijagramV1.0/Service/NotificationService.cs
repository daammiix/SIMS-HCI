using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Repository;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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

        public void RemoveReadNotification()
        {
            List<Notification> notes = _notificationRepo.GetAllNotifications().ToList();
            foreach (Notification note in notes)
            {
                if (note.IsRead)
                {
                    _notificationRepo.RemoveNotification(note);
                }
            }
        }

        public void RemoveOldNotification()
        {
            List<Notification> notes = _notificationRepo.GetAllNotifications().ToList();
            foreach (Notification note in notes)
            {
                if (note.Created < DateTime.Now)
                {
                    _notificationRepo.RemoveNotification(note);
                }
            }
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
