using ClassDijagramV1._0.FileHandlers;
using ClassDijagramV1._0.Model;
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

        private FileHandler<Notification> _notificationFileHandler;
        private FileHandler<Appointment> _appointmentFileHandler;

        public ObservableCollection<Appointment> Appointments;
        public ObservableCollection<Appointment> AppointmentsByPatient;
        public ObservableCollection<Doctor> Doctors
        {
            get;
            set;
        }

        public AppointmentRepo(FileHandler<Appointment> fileHandler)
        {
            _appointmentFileHandler = fileHandler;
            Appointments = new ObservableCollection<Appointment>(_appointmentFileHandler.GetItems());
            Doctors = new ObservableCollection<Doctor>();
        }

        public void UpdateAppointment(int oldAppointmentID, Appointment updatedAppointment)
        {
            Appointment oldAppointment = FindAppointmentById(oldAppointmentID);
            oldAppointment = updatedAppointment;
        }

        public ObservableCollection<Appointment> GetAppointments()
        {
            return Appointments;
        }

        public void SaveAppointments()
        {
            _appointmentFileHandler.SaveItems(Appointments.ToList());
        }

        /// <summary>
        /// Dodaje novi appointment ako ne postoji appointment sa istim id-em, u suprotnom ce da pregazi stari
        /// </summary>
        /// <param name="newAppointment"></param>
        /// <returns></returns>
        public void AddNewAppointment(Appointment newAppointment)
        {
            bool exists = false;
            Appointment? toUpdate = null;
            foreach (Appointment appointment in Appointments)
            {
                if (appointment.Id == newAppointment.Id)
                {
                    exists = true;
                    toUpdate = appointment;
                    break;
                }
            }

            if (!exists)
            {
                Appointments.Add(newAppointment);
            }
            else
            {
                toUpdate = newAppointment;
            }
        }

        public void RemoveAppointment(int appointmentID)
        {
            Appointments.Remove(FindAppointmentById(appointmentID));
        }


        public Appointment? FindAppointmentById(int appointmentID)
        {
            foreach (Appointment a in Appointments)
            {
                if (a.Id == appointmentID)
                {
                    return a;
                }
            }
            return null;
        }

        public ObservableCollection<Appointment> GetListOfAppointments()
        {
            return Appointments;
        }
    }
}