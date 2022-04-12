/***********************************************************************
 * Module:  AppointmentRepo.cs
 * Author:  lipov
 * Purpose: Definition of the Class Repository.AppointmentRepo
 ***********************************************************************/

using ClassDijagramV1._0.FileHandlers;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Repository
{
    public class AppointmentRepo
    {

        private String Path;
        private AppointmentFileHandler _appointmentFileHandler;

        public ObservableCollection<Appointment> Appointments;

        public ObservableCollection<Doctor> Doctors
        {
            get;
            set;
        }



        public AppointmentRepo(AppointmentFileHandler apointmentFileHandler)
        {
            _appointmentFileHandler = apointmentFileHandler;
            Appointments = new ObservableCollection<Appointment>(_appointmentFileHandler.GetAppointments());
            Doctors = new ObservableCollection<Doctor>();

            /*Room r1 = new Room();
            DateTime date1 = new DateTime(2008, 5, 1, 8, 30, 52);
            DateTime date2 = new DateTime(2010, 8, 18, 13, 30, 30);
            TimeSpan interval = date2 - date1;
            Doctor d1 = new Doctor("doktor", "doktor", "123", "musko", "3875432", "the292200", date1);
            Doctor d2 = new Doctor("lekar", "lekar", "123", "musko", "3875432", "the292200", date1);
            Doctors.Add(d1);

            Patient p1 = new Patient("djordje", "lipovcic", "123", "musko", "3875432", "the292200", date1, "1234");
            Patient p2 = new Patient("sandra", "brkic", "123", "musko", "3875432", "the292200", date1, "1234");

            Appointment a1 = new Appointment(p1, r1, d1, "1", date1, interval, AppointmentType.generalPractitionerCheckup);
            Appointment a2 = new Appointment(p2, r1, d2, "2", date1, interval, AppointmentType.generalPractitionerCheckup);
            Appointments.Add(a1);
            Appointments.Add(a2);*/
        }

        public AppointmentRepo()
        {
        }

        public void UpdateAppointment(string oldAppointmentID, Appointment updatedAppointment)
        {
            var oldAppointment = FindAppointmentById(oldAppointmentID);
            oldAppointment = updatedAppointment;
        }

        public ObservableCollection<Appointment> GetAppointments()
        {
            return Appointments;
        }

        /*public void SetAppointment(List<Appointment> appointments)
        {
            // TODO: implement
            _appointmentFileHandler.SaveAppointments(Appointments.ToList());
        }*/

        public void SaveAppointments()
        {
            _appointmentFileHandler.SaveAppointments(Appointments.ToList());
        }

        public ObservableCollection<Appointment> AddNewAppointment(Appointment newAppointment)
        {
            Appointments.Add(newAppointment);
            return GetAppointments();
        }

        public void RemoveAppointment(String appointmentID)
        {
            Appointments.Remove(FindAppointmentById(appointmentID));
        }


        public Appointment FindAppointmentById(String appointmentID)
        {
            foreach (Appointment a in Appointments)
            {
                if (a.Id.Equals(appointmentID))
                {
                    return a;
                }
            }
            return null;
        }
    }
}