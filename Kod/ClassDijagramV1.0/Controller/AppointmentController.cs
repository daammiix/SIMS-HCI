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

        //public ObservableCollection<Appointment> GetAllAppointments(String username)
        //{
        //    return _appointmentService.GetAllAppointments(username);
        //}

        public Appointment GetOneAppointment(Appointment appointment)
        {
            // TODO: implement
            return null;
        }

        public void SaveAppointments()
        {
            _appointmentService.SaveAppointments();
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

    }
}