/***********************************************************************
 * Module:  Appointment.cs
 * Author:  lipov
 * Purpose: Definition of the Class Model.Appointment
 ***********************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Model
{
   public class Appointment : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangedEventHandler PropertyChanging;
        /*public Patient patient { get; set; }
      public Room room { get; set; }
      public Doctor doctor { get; set; }
      public String appointmentID { get; set; }
      public DateTime appointmentDate { get; set; }
      public TimeSpan duration { get; set; }
      public AppointmentType type { get; set; }*/

        private Patient patient;
        private Room room;
        private Doctor doctor;
        private String appointmentID;
        private DateTime appointmentDate;
        private TimeSpan duration;
        private AppointmentType type;

        public Room Room
        {
            get
            {
                return room;
            }
            set
            {
                if (value != room)
                {
                    room = value;
                    OnPropertyChanged("Room");
                }
            }
        }

        public DateTime Date
        {
            get
            {
                return appointmentDate;
            }
            set
            {
                if (value != appointmentDate)
                {
                    appointmentDate = value;
                    OnPropertyChanged("Date");
                }
            }
        }

        public string Id
        {
            get
            {
                return appointmentID;
            }
            set
            {
                if (value != appointmentID)
                {
                    appointmentID = value;
                    OnPropertyChanged("AppointmentID");
                }
            }
        }

        public TimeSpan Duration
        {
            get
            {
                return duration;
            }
            set
            {
                if (value != duration)
                {
                    duration = value;
                    OnPropertyChanged("Duration");
                }
            }
        }

        public Patient Patient
        {
            get
            {
                return patient;
            }
            set
            {
                if (value != patient)
                {
                    patient = value;
                    OnPropertyChanged("Patient");
                }
            }
        }

        public Doctor Doctor
        {
            get
            {
                return doctor;
            }
            set
            {
                if (value != doctor)
                {
                    doctor = value;
                    OnPropertyChanged("Doctor");
                }
            }
        }

        public Appointment(Patient patient, Room room, Doctor doctor, string appointmentID, DateTime appointmentDate, TimeSpan duration, AppointmentType type)
        {
            this.patient = patient;
            this.Room = room;
            this.doctor = doctor;
            this.appointmentID = appointmentID;
            this.appointmentDate = appointmentDate;
            this.duration = duration;
            this.type = type;
        }

        public Appointment()
        {
        }

        protected virtual void OnPropertyChanged(string name)
         {
             if (PropertyChanged != null)
             {
                 PropertyChanged(this, new PropertyChangedEventArgs(name));
             }
         }

         protected virtual void OnPropertyChanging(string name)
         {
             if (PropertyChanging != null)
             {
                 PropertyChanging(this, new PropertyChangedEventArgs(name));
             }
         }

    }

}