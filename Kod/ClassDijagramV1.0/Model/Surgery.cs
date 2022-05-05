// File:    Surgery.cs
// Author:  x
// Created: Sunday, April 10, 2022 4:56:34 PM
// Purpose: Definition of Class Surgery

using System;
using System.ComponentModel;

namespace Model
{
    public class Surgery : INotifyPropertyChanged
    {
        private String surgeryID;
        private DateTime date;
        private TimeSpan duration;
        private Patient patient;
        private Room room;
        private Doctor doctor;

        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangedEventHandler PropertyChanging;

        /*public Surgery(string surgeryID, DateTime date, TimeSpan duration, Patient patient, Room room, Doctor doctor)
        {
            this.surgeryID = surgeryID;
            this.date = date;
            this.duration = duration;
            this.patient = patient;
            this.room = room;
            this.doctor = doctor;
        }*/



        /*private Patient patient;
        private Room room;
        private Doctor doctor;
        private String appointmentID;
        private DateTime appointmentDate;
        private TimeSpan duration;
        private AppointmentType type;*/

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
                return date;
            }
            set
            {
                if (value != date)
                {
                    date = value;
                    OnPropertyChanged("Date");
                }
            }
        }

        public string SurgeryID
        {
            get
            {
                return surgeryID;
            }
            set
            {
                if (value != surgeryID)
                {
                    surgeryID = value;
                    OnPropertyChanged("SurgeryID");
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

        public Surgery(string surgeryID, DateTime date, TimeSpan duration, Patient patient, Room room, Doctor doctor)
        {
            this.patient = patient;
            this.Room = room;
            this.doctor = doctor;
            this.surgeryID = surgeryID;
            this.date = date;
            this.duration = duration;
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