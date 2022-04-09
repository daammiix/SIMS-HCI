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
        private AppointmentService _appointmentService = new AppointmentService();
        public ObservableCollection<Appointment> AddAppointment(Appointment appointment)
      {
            // TODO: implement
            return _appointmentService.AddAppointment(appointment);
      }
      
      public void RemoveAppointment(Model.Appointment appointment)
      {
         // TODO: implement
         _appointmentService.RemoveAppointment(appointment.Id);
      }
      
      public void UpdateAppointment(Model.Appointment appointment)
      {
         // TODO: implement
      }
      
      public ObservableCollection<Appointment> GetAllAppointments()
      {
         // TODO: implement
         return _appointmentService.GetAllAppointments();
      }
      
      public Model.Appointment GetOneAppointment(Model.Appointment appointment)
      {
         // TODO: implement
         return null;
      }
   
   }
}