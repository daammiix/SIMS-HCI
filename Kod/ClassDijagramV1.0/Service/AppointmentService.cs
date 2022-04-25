/***********************************************************************
 * Module:  AppointmentService.cs
 * Author:  lipov
 * Purpose: Definition of the Class Service.AppointmentService
 ***********************************************************************/

using ClassDijagramV1._0;
using ClassDijagramV1._0.Model;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Service
{
    public class AppointmentService
    {
        private AppointmentRepo _appointmentRepo;

        public AppointmentService(AppointmentRepo repo)
        {
            _appointmentRepo = repo;
        }

        public ObservableCollection<Appointment> AddAppointment(Appointment appointment)
        {
            return _appointmentRepo.AddNewAppointment(appointment);
        }

        public void RemoveAppointment(String appointmentID)
        {
            _appointmentRepo.RemoveAppointment(appointmentID);
        }

        public void UpdateAppointment(String oldAppointmentID, Appointment updatedAppointment)
        {
            _appointmentRepo.UpdateAppointment(oldAppointmentID, updatedAppointment);
        }

        public ObservableCollection<Appointment> GetAllAppointments(String username) // obrisi iz bajdinga preglede koji mi ne trebaju, tj nisu od tog pacijenta
        {
            List<Appointment> otherPatients = new List<Appointment>();
            foreach (Appointment a in _appointmentRepo.GetAppointments())
            {
                if (!a.Patient.Name.Equals(username))
                {
                    otherPatients.Add(a);
                }
            }

            foreach (Appointment a in otherPatients)
            {
                _appointmentRepo.RemoveAppointment(a.AppointmentID);
            }
            return _appointmentRepo.GetAppointments();
        }

        internal void AddNotification(Appointment appointment, NotificationType notificationType)
        {
            
            Notification note;

            if (NotificationType.addingAppointment.Equals(notificationType))
            {
                var notificationID = "1";
                var content = "Imate zakazan pregled u " + appointment.AppointmentDate + " u sobi " + appointment.Room.RoomName;
                DateTime created = appointment.AppointmentDate;
                Notification n = new Notification(notificationID, content, "djordje", false, created, NotificationType.addingAppointment);
                _appointmentRepo.AddNotification(n);
            }
            else if (NotificationType.deletingAppointment.Equals(notificationType))
            {
                // obrisi notifikaciju pomocu idija
            }
            

            //note.Title  pusi kurac ne znam
        }

        internal ObservableCollection<Notification> GetAllNotifications()
        {
            return _appointmentRepo.GetAllNotifications();
        }

        public Appointment GetOneAppointment(String appointmentID)
        {
            return _appointmentRepo.GetOneAppointment(appointmentID);
        }

        public void SaveAppointments()
        {
            _appointmentRepo.SaveAppointments();
        }
    }
}