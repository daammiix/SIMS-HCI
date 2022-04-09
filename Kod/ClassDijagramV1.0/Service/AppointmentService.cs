/***********************************************************************
 * Module:  AppointmentService.cs
 * Author:  lipov
 * Purpose: Definition of the Class Service.AppointmentService
 ***********************************************************************/

using ClassDijagramV1._0;
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
        private AppointmentRepo _appointmentRepo = new AppointmentRepo();
       // private PatientRepo _patientRepo = new PatientRepo();

        public ObservableCollection<Appointment> AddAppointment(Appointment appointment)
      {
            // TODO: implement
            return _appointmentRepo.AddNewAppointment(appointment);
      }
      
      public void RemoveAppointment(String appointmentID)
      {
         // TODO: implement
         _appointmentRepo.RemoveAppointment(appointmentID);
      }
      
      public void UpdateAppointment(Model.Appointment appointment)
      {
         // TODO: implement
      }
      
      public ObservableCollection<Appointment> GetAllAppointments()
      {
         // TODO: implement
         return _appointmentRepo.GetAppointments();
      }
      
      public Appointment GetOneAppointment(String appointmentID)
      {
         // TODO: implement
         return null;
      }
   
   }
}