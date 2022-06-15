using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using System;

namespace Model
{
    public class Appointment : ObservableObject
    {

        public static int idCounter = 0;

        #region Fields

        private int _appointmentId;
        private int _patientId;
        private int _doctorId;
        private string _roomId;
        private DateTime _apointmentDate;
        private TimeSpan _duration;
        private AppointmentType _type;
        private MedicalReport medicalReport;

        #endregion

        #region Properties

        public int Id
        {
            get
            {
                return _appointmentId;
            }
            set
            {
                if (value != _appointmentId)
                {
                    _appointmentId = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        public int PatientId
        {
            get
            {
                return _patientId;
            }
            set
            {
                if (value != _patientId)
                {
                    _patientId = value;
                    OnPropertyChanged("Patient");
                }
            }
        }
        public int DoctorId
        {
            get
            {
                return _doctorId;
            }
            set
            {
                if (value != _doctorId)
                {
                    _doctorId = value;
                    OnPropertyChanged("Doctor");
                }
            }
        }
        public string RoomId
        {
            get
            {
                return _roomId;
            }
            set
            {
                if (value != _roomId)
                {
                    _roomId = value;
                    OnPropertyChanged("Room");
                }
            }
        }
        public DateTime AppointmentDate
        {
            get
            {
                return _apointmentDate;
            }
            set
            {
                if (value != _apointmentDate)
                {
                    _apointmentDate = value;
                    OnPropertyChanged("AppointmentDate");
                }
            }
        }
        public TimeSpan Duration
        {
            get
            {
                return _duration;
            }
            set
            {
                if (value != _duration)
                {
                    _duration = value;
                    OnPropertyChanged("Duration");
                }
            }
        }
        public AppointmentType AppointmentType
        {
            get
            {
                return _type;
            }
            set
            {
                if (value != _type)
                {
                    _type = value;
                    OnPropertyChanged("AppointmentType");
                }
            }
        }

        public MedicalReport MedicalReport
        {
            get
            {
                return medicalReport;
            }
            set
            {
                if (value != medicalReport)
                {
                    medicalReport = value;
                    OnPropertyChanged("MedicalReport");
                }
            }
        }

        #endregion

        #region Constructor

        public Appointment(int patientId, int doctorId, string roomId, DateTime _apointmentDate, TimeSpan _duration, AppointmentType appointmentType, MedicalReport medicalReport = null)
        {
            Id = ++idCounter;
            PatientId = patientId;
            DoctorId = doctorId;
            RoomId = roomId;
            AppointmentDate = _apointmentDate;
            Duration = _duration;
            AppointmentType = appointmentType;
            MedicalReport = medicalReport;
        }
        public Appointment()
        {

        }

        #endregion

    }

}