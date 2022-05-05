/***********************************************************************
 * Module:  Appointment.cs
 * Author:  lipov
 * Purpose: Definition of the Class Model.Appointment
 ***********************************************************************/

using ClassDijagramV1._0.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Model
{
    public class Appointment : ObservableObject
    {
        private String appointmentID;
        private int patientID;
        private Doctor doctor;
        private Room room;
        private DateTime appointmentDate;
        private TimeSpan duration;
        private AppointmentType type;

        public String AppointmentID
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
        public int PatientID
        {
            get
            {
                return patientID;
            }
            set
            {
                if (value != patientID)
                {
                    patientID = value;
                    OnPropertyChanged("PatientID");
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
        public DateTime AppointmentDate
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
                    OnPropertyChanged("AppointmentDate");
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
        public AppointmentType AppointmentType
        {
            get
            {
                return type;
            }
            set
            {
                if (value != type)
                {
                    type = value;
                    OnPropertyChanged("AppointmentType");
                }
            }
        }



        public Appointment(String appointmentID, int patientID, Doctor doctor, Room room, DateTime appointmentDate, TimeSpan duration, AppointmentType appointmentType)
        {
            AppointmentID = appointmentID;
            PatientID = patientID;
            Doctor = doctor;
            Room = room;
            AppointmentDate = appointmentDate;
            Duration = duration;
            AppointmentType = appointmentType;
        }
        public Appointment()
        {
        }
    }

}