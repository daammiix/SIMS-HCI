using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using Controller;
using Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace ClassDijagramV1._0.Views.PatientView
{
    /// <summary>
    /// Interaction logic for AppointmentsView.xaml
    /// </summary>
    public partial class AppointmentsViewPage : Page
    {
        #region Fields

        private AppointmentController _appointmentController;
        private RoomController _roomController;
        private PatientController _patientController;
        private ActivityController _activityController;
        private BanningPatientController _banningPatientController;
        private NotificationController _notificationController;
        private PatientMainWindow parent { get; set; }


        private PrintDialog _printDialog = new PrintDialog();

        #endregion


        #region Properties

        public static AppointmentViewModel? SelectedAppointment { get; set; }
        public ObservableCollection<AppointmentViewModel> Appointments { get; set; }
        public BindingList<Room> Rooms { get; set; }

        #endregion

        public AppointmentsViewPage(PatientMainWindow patientMain, ObservableCollection<AppointmentViewModel> appointments)
        {
            InitializeComponent();
            this.DataContext = this;
            parent = patientMain;

            App app = Application.Current as App;
            _appointmentController = app.AppointmentController;
            _roomController = app.roomController;
            _activityController = app.ActivityController;
            _banningPatientController = app.BanningPatientController;
            _notificationController = app.NotificationController;

            Appointments = appointments;
            // Pokupimo sve sobe
            Rooms = _roomController.GetAllRooms();

        }
        private void AddAppontment_Click(object sender, RoutedEventArgs e)
        {
            if (_banningPatientController.CheckStatusOfPatient(parent.Patient.Id, parent.Account) == true)
            {
                errorBan.Content = "Banovani ste!";
            }
            else
            {
                parent.startWindow.Content = new AppointmentAddPage(parent, Appointments);
            }
        }

        private void UpdateAppontment_Click(object sender, RoutedEventArgs e)
        {
            if (_banningPatientController.CheckStatusOfPatient(parent.Patient.Id, parent.Account) == true)
            {
                errorBan.Content = "Banovani ste!";
            }
            else
            {
                if (tabelaPregledi.SelectedIndex != -1)
                {
                    parent.startWindow.Content = new AppointmentUpdatePage(parent, Appointments);
                }
            }
        }

        private void RemoveAppontment_Click(object sender, RoutedEventArgs e)
        {
            if (_banningPatientController.CheckStatusOfPatient(parent.Patient.Id, parent.Account) == true)
            {
                errorBan.Content = "Banovani ste!";
            }
            else
            {
                if (tabelaPregledi.SelectedIndex != -1)
                {
                    AppointmentViewModel selectedAppointment = (AppointmentViewModel)tabelaPregledi.SelectedItem;
                    _notificationController.RemoveNotificationByAppointment(selectedAppointment.Id);
                    Appointments.Remove(selectedAppointment);
                    _appointmentController.RemoveAppointment(selectedAppointment.Id);
                    ActivityLog activity = new ActivityLog(DateTime.Now, parent.Patient.Id, TypeOfActivity.cancelAppointment);
                    _activityController.AddActivity(activity);
                }
            }


        }

        private void generatePDFClick(object sender, RoutedEventArgs e)
        {
            //generateReport.Visibility = Visibility.Hidden;
            _printDialog.PrintVisual(this, "izvjestaj");
            //generateReport.Visibility = Visibility.Visible;
        }
    }

    // Reprezentuje appointment u view-u
    public class AppointmentViewModel : ObservableObject
    {

        #region Fields

        // Controllers
        private PatientController _patientController;
        private RoomController _roomController;
        private DoctorController _doctorController;

        private Appointment _appointment;
        private Patient _patient;
        private Room _room;
        private Doctor _doctor;

        #endregion

        #region Properties

        public Appointment Appointment
        {
            get { return _appointment; }
            set { _appointment = value; }
        }

        public Patient Patient
        {
            get { return _patient; }
            set { _patient = value; }
        }

        public Room Room
        {
            get { return _room; }
            set { _room = value; }
        }

        public Doctor Doctor
        {
            get { return _doctor; }
            set { _doctor = value; }
        }

        // Za dataGrid

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

        public string PatientName
        {
            get
            {
                return _patient.Name;
            }
            set
            {
                if (!_patient.Name.Equals(value))
                {
                    _patient.Name = value;
                    OnPropertyChanged("PatientName");
                }
            }
        }

        public string DoctorName
        {
            get
            {
                return _doctor.Name;
            }
            set
            {
                if (!_doctor.Name.Equals(value))
                {
                    _doctor.Name = value;
                    OnPropertyChanged("DoctorName");
                }
            }
        }

        public string RoomId
        {
            get
            {
                return _room.RoomID;
            }
            set
            {
                if (_room.RoomID != value)
                {
                    _room.RoomID = value;
                    OnPropertyChanged("RoomId");
                }
            }
        }

        public TimeSpan Duration
        {
            get
            {
                return _appointment.Duration;
            }
            set
            {
                if (_appointment.Duration != value)
                {
                    _appointment.Duration = value;
                    OnPropertyChanged("Duration");
                }
            }
        }

        public DateTime AppointmentDate
        {
            get
            {
                return _appointment.AppointmentDate;
            }
            set
            {
                if (_appointment.AppointmentDate != value)
                {
                    _appointment.AppointmentDate = value;
                    OnPropertyChanged("AppointmentDate");
                }
            }
        }

        #endregion

        #region Constructor

        public AppointmentViewModel(Appointment a)
        {
            Appointment = a;

            // Pokupimo controllere
            App app = Application.Current as App;

            if (app != null)
            {
                _patientController = app.PatientController;
                _roomController = app.roomController;
                _doctorController = app.DoctorController;
            }

            // Ucitamo reference vezane za appointment
            _patient = _patientController.GetPatientById(_appointment.PatientId);
            _room = _roomController.GetRoom(_appointment.RoomId);
            _doctor = _doctorController.GetDoctorById(_appointment.DoctorId);

        }

        #endregion
    }
}
