using ClassDijagramV1._0.Model;
using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Controller
{
    public class AppointmentController
    {
        private AppointmentService _appointmentService;

        public AppointmentController(AppointmentService appService)
        {
            _appointmentService = appService;
        }

        public void AddAppointment(Appointment appointment)
        {
            _appointmentService.AddAppointment(appointment);
        }

        public void RemoveAppointment(int appointmentId)
        {
            _appointmentService.RemoveAppointment(appointmentId);
        }

        public void UpdateAppointment(int oldAppointmentID, Appointment updatedAppointment)
        {
            _appointmentService.UpdateAppointment(oldAppointmentID, updatedAppointment);
        }

        public ObservableCollection<Appointment> GetAllAppointmentsByPatient(int patientID)
        {
            return _appointmentService.GetAllAppointmentsByPatient(patientID);
        }

        public ObservableCollection<Appointment> GetAllAppointments()
        {
            return _appointmentService.GetAllAppointments();
        }

        public Appointment GetOneAppointment(Appointment appointment)
        {
            // TODO: implement
            return null;
        }

        public void SaveAppointments()
        {
            _appointmentService.SaveAppointments();
        }

        internal void AddNotification(Appointment appointment, Room r1, NotificationType notificationType)
        {
            _appointmentService.AddNotification(appointment, r1, notificationType);
        }

        internal ObservableCollection<Notification> GetAllNotifications()
        {
            return _appointmentService.GetAllNotifications();
        }

        /// <summary>
        /// Vraca sve appointmente
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Appointment> GetAppointments()
        {
            return _appointmentService.GetAppointments();
        }

        /// <summary>
        /// Vraca appointment sa zadatim id-em, ako ne postoji vraca null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Appointment GetAppointmentById(int id)
        {
            return _appointmentService.GetAppointmentById(id);
        }

        /// <summary>
        /// Proverava da li je termin zauzet odnosno da li vec postoji neki appointment u istoj sobi za dato vreme
        /// ili da li neki lekar vec ima appointment u to vreme
        /// </summary>
        /// <param name="start"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public bool CheckIsTerminFree(Appointment newAppointment)
        {
            return _appointmentService.CheckIsTerminFree(newAppointment);
        }


        public ObservableCollection<Appointment> GetListOfAppointments()
        {
            return _appointmentService.GetListOfAppointments();
        }

    }
}