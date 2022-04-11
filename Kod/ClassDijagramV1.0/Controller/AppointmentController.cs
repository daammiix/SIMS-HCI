/***********************************************************************
 * Module:  AppointmentController.cs
 * Author:  lipov
 * Purpose: Definition of the Class Controller.AppointmentController
 ***********************************************************************/

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

        public ObservableCollection<Appointment> AddAppointment(Appointment appointment)
        {
            return _appointmentService.AddAppointment(appointment);
        }

        public void RemoveAppointment(Appointment appointment)
        {
            _appointmentService.RemoveAppointment(appointment.Id);
        }

        public void UpdateAppointment(String oldAppointmentID, Appointment updatedAppointment)
        {
            _appointmentService.UpdateAppointment(oldAppointmentID, updatedAppointment);
        }

        public ObservableCollection<Appointment> GetAllAppointments(String username)
        {
            return _appointmentService.GetAllAppointments(username);
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

    }
}