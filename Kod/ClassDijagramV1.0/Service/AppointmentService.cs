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
                _appointmentRepo.RemoveAppointment(a.Id);
            }
            return _appointmentRepo.GetAppointments();
        }


        public Appointment GetOneAppointment(String appointmentID)
        {
            //var allAppointments = GetAllAppointments();
            return null;
        }

    }
}