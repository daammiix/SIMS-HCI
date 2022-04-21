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
        private Patient patient;
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
                    onPropertyChanged("AppointmentID");
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
                    onPropertyChanged("Patient");
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
                    onPropertyChanged("Doctor");
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
                    onPropertyChanged("Room");
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
                    onPropertyChanged("AppointmentDate");
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
                    onPropertyChanged("Duration");
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
                    onPropertyChanged("AppointmentType");
                }
            }
        }



        public Appointment(String appointmentID, Patient patient, Doctor doctor, Room room, DateTime appointmentDate, TimeSpan duration, AppointmentType appointmentType)
        {
            AppointmentID = appointmentID;
            Patient = patient;
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