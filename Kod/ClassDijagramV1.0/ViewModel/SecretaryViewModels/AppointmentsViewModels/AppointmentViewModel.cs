using ClassDijagramV1._0.Util;
using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Media;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.AppointmentsViewModels
{
    public class AppointmentViewModel : ObservableObject
    {
        #region Fields

        private Appointment _appointment;
        private Patient _patient;
        private Doctor _doctor;
        private Room _room;

        private PatientController _patientController;
        private DoctorController _doctorController;
        private RoomController _roomController;

        private Brush _color;
        private string _appointmentStatus;

        #endregion

        #region Constructor

        public AppointmentViewModel(Appointment appointment)
        {
            _appointment = appointment;

            // Ucitamo kontrolere
            App app = Application.Current as App;
            _patientController = app.PatientController;
            _doctorController = app.DoctorController;
            _roomController = app.roomController;

            // Ucitamo reference na sobu,doktora i pacijenta posto imamo samo id
            _patient = _patientController.GetPatientById(_appointment.PatientId);
            _room = _roomController.GetARoom(_appointment.RoomId);
            _doctor = _doctorController.GetDoctorById(_appointment.DoctorId);

            SetAppropriateColor();

            // Timer koji na svakih pola sekunde proverava da li se appointment zavrsio
            SetAppointmentStatusTimer();
        }

        #endregion

        #region Properties

        public DateTime From
        {
            get { return _appointment.AppointmentDate; }
            set
            {
                if (_appointment.AppointmentDate != value)
                {
                    _appointment.AppointmentDate = value;
                    OnPropertyChanged("From");
                }
            }
        }

        public DateTime To
        {
            get { return _appointment.AppointmentDate + _appointment.Duration; }
            set
            {
                if (_appointment.AppointmentDate + _appointment.Duration != value)
                {
                    _appointment.Duration = value - _appointment.AppointmentDate;
                    OnPropertyChanged("To");
                }
            }
        }

        public int Id
        {
            get
            {
                return _appointment.Id;
            }
            set
            {
                if (_appointment.Id != value)
                {
                    _appointment.Id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        // Zbog schedulera kad se stvalja Subject mora da bude string
        public string IdStr
        {
            get
            {
                return Id.ToString();
            }
        }

        public AppointmentType AppointmentType
        {
            get { return _appointment.AppointmentType; }
            set
            {
                if (_appointment.AppointmentType != value)
                {
                    _appointment.AppointmentType = value;
                    OnPropertyChanged("AppointmentType");
                }
            }
        }

        public string AppointmentTypeStr
        {
            get
            {
                switch (_appointment.AppointmentType)
                {
                    case AppointmentType.operation:
                        {
                            return "operation";
                        }
                    case AppointmentType.generalPractitionerCheckup:
                        {
                            return "practitioner checkup";
                        }
                    case AppointmentType.specialistCheckup:
                        {
                            return "specialist checkup";
                        }
                }

                return "";
            }
        }

        public Brush Color
        {
            get { return _color; }
            set
            {
                _color = value;
                OnPropertyChanged("Color");
            }
        }

        public double TimeIntervalSize
        {
            get { return _appointment.Duration.TotalMinutes; }
        }

        public string DoctorName
        {
            get { return _doctor.Name; }
        }

        public string PatientName
        {
            get { return _patient.Name; }
        }

        public string StartTime
        {
            get
            {
                return _appointment.AppointmentDate.ToShortTimeString();
            }
        }

        public string EndTime
        {
            get
            {
                return (_appointment.AppointmentDate + _appointment.Duration).ToShortTimeString();
            }
        }

        public string AppointmentStatus
        {
            get
            {
                return _appointmentStatus;
            }
            set
            {
                if (_appointmentStatus != value)
                {
                    _appointmentStatus = value;
                    OnPropertyChanged("AppointmentStatus");
                }
            }
        }

        // Soba gde se odrzava pregled
        public int RoomNumber
        {
            get
            {
                return _room.RoomNumber;
            }
            set
            {
                if (_room.RoomNumber != value)
                {
                    _room.RoomNumber = value;
                    OnPropertyChanged("RoomNumber");
                }
            }
        }

        public Room Room
        {
            get
            {
                return _room;
            }
            set
            {
                if (_room != value)
                {
                    _room = value;
                    OnPropertyChanged("Room");
                }
            }
        }

        public Doctor Doctor
        {
            get
            {
                return _doctor;
            }
            set
            {
                if (_doctor != value)
                {
                    _doctor = value;
                    OnPropertyChanged("Doctor");
                }
            }
        }


        #endregion

        #region Private Helpers

        /// <summary>
        /// Stavlja odgovarajucu boju u zavisnosti od tipa pregleda
        /// </summary>
        private void SetAppropriateColor()
        {
            switch (_appointment.AppointmentType)
            {
                case AppointmentType.operation:
                    {
                        // Crimson
                        Color = new SolidColorBrush(System.Windows.Media.Color.FromRgb(220, 20, 60));
                        break;
                    }
                case AppointmentType.generalPractitionerCheckup:
                    {
                        // Electric blue
                        Color = new SolidColorBrush(System.Windows.Media.Color.FromRgb(125, 249, 255));
                        break;
                    }
                case AppointmentType.specialistCheckup:
                    {
                        // Mint green
                        Color = new SolidColorBrush(System.Windows.Media.Color.FromRgb(152, 251, 152));
                        break;
                    }
            }
        }

        private void SetAppointmentStatusTimer()
        {
            // Timer koji okida event na svakih 500 ms
            Timer timer = new Timer(500);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        /// <summary>
        /// Updatuje AppointmentStatus u zavisnosti od toga da li se zavrsio
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (To < DateTime.Now)
            {
                AppointmentStatus = "Completed";
            }
            else
            {
                AppointmentStatus = "Not Completed";
            }
        }

        #endregion
    }
}
