/***********************************************************************
 * Module:  AppointmentRepo.cs
 * Author:  lipov
 * Purpose: Definition of the Class Repository.AppointmentRepo
 ***********************************************************************/

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

        public ObservableCollection<Appointment> Appointments;

        public ObservableCollection<Doctor> Doctors
        {
            get;
            set;
        }



        public AppointmentRepo()
        {
            Appointments = new ObservableCollection<Appointment>();
            Doctors = new ObservableCollection<Doctor>();

            Room r1 = new Room();
            DateTime date1 = new DateTime(2008, 5, 1, 8, 30, 52);
            DateTime date2 = new DateTime(2010, 8, 18, 13, 30, 30);
            TimeSpan interval = date2 - date1;
            Doctor d1 = new Doctor("Drrrrjordje", "Lipovcic", "123", "musko", "3875432", "the292200", date1);
            Doctors.Add(d1);
            Patient p1 = new Patient("Djordje", "Lipovcic", "123", "musko", "3875432", "the292200", date1, null, "1234", date1);

            Appointment a1 = new Appointment(p1, r1, d1, "1", date1, interval, AppointmentType.generalPractitionerCheckup);
            Appointment a2 = new Appointment(p1, r1, d1, "2", date1, interval, AppointmentType.generalPractitionerCheckup);
            Appointments.Add(a1);
            Appointments.Add(a2);


        }



        public ObservableCollection<Appointment> GetAppointments()
        {
            // TODO: implement
            return Appointments;
        }

        public void SetAppointment(List<Appointment> appointments)
        {
            // TODO: implement
        }

        public ObservableCollection<Appointment> AddNewAppointment(Appointment newAppointment)
        {
            //List<Appointment> appointments = GetAppointments();
            //appointments.Add(newAppointment);
            //SetAppointment(appointments);
            foreach (Appointment p in Appointments)
                MessageBox.Show(p.Id);
            Appointments.Add(newAppointment);
            foreach (Appointment p in Appointments)
                MessageBox.Show(p.Id);
            //GetAppointments();
            //return null;
            return GetAppointments();
        }

        public void RemoveAppointment(String appointmentID)
        {
            // TODO: implement
            foreach (Appointment p in Appointments)
                MessageBox.Show(p.Id);

            Appointments.Remove(FindAppointmentById(appointmentID));

            foreach (Appointment p in Appointments)
                MessageBox.Show(p.Id);

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