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
        public ActivityController _activityController;
        public BanningPatientController _banningPatientController;
        public NotificationController _notificationController;
        private PatientMainWindow parent { get; set; }

        // Ulogovan pacijent
        private Patient _logedPatient;
        private Account _account;

        private PrintDialog _printDialog = new PrintDialog();

        #endregion


        #region Properties

        public static AppointmentViewModel? SelectedAppointment { get; set; }
        public ObservableCollection<AppointmentViewModel> Appointments
        {
            get;
            set;
        }

        public BindingList<Room> Rooms
        {
            get;
            set;
        }

        #endregion

        /// <summary>
        /// Pacijent koji je ulogovan
        /// </summary>
        /// <param name="p"></param>
        public AppointmentsViewPage(ObservableCollection<AppointmentViewModel> appointments,
            PatientMainWindow patientMain, Patient logedPatient, Account account)
        {
            InitializeComponent();

            _logedPatient = logedPatient;
            _account = account;

            this.DataContext = this;
            parent = patientMain;
            App app = Application.Current as App;
            _appointmentController = app.AppointmentController;

            _roomController = app.roomController;
            _patientController = app.PatientController;
            _activityController = app.ActivityController;
            _banningPatientController = app.BanningPatientController;
            _notificationController = app.NotificationController;

            Appointments = appointments;
            // Pokupimo sve sobe
            Rooms = _roomController.GetAllRooms();

        }
        private void AddAppontment_Click(object sender, RoutedEventArgs e)
        {
            if (_banningPatientController.CheckStatusOfPatient(_logedPatient.Id, _account) == true)
            {
                errorBan.Content = "Banovani ste!";
            }
            else
            {
                // Prosledimo listu AppointmentViewModela jer tu dodajemo novi appointment kako bi se view azurirao
                parent.startWindow.Content = new AppointmentAddPage(parent, Appointments, _logedPatient);
            }
        }

        private void UpdateAppontment_Click(object sender, RoutedEventArgs e)
        {
            if (_banningPatientController.CheckStatusOfPatient(_logedPatient.Id, _account) == true)
            {
                errorBan.Content = "Banovani ste!";
            }
            else
            {
                if (tabelaPregledi.SelectedIndex != -1)
                {
                    parent.startWindow.Content = new AppointmentUpdatePage(parent, Appointments, _logedPatient);
                }
            }

        }

        private void RemoveAppontment_Click(object sender, RoutedEventArgs e)
        {
            if (_banningPatientController.CheckStatusOfPatient(_logedPatient.Id, _account) == true)
            {
                errorBan.Content = "Banovani ste!";
            }
            else
            {
                if (tabelaPregledi.SelectedIndex != -1)
                {
                    //_appointmentController.AddNotification((Appointment)tabelaPregledi.SelectedItem, NotificationType.deletingAppointment);
                    //tabelaPregledi.ItemsSource = _appointmentController.GetAllAppointmentsByPatient(parent.patientID);
                    //Appointments.Remove((Appointment)tabelaPregledi.SelectedItem);
                    AppointmentViewModel selectedAppointment = (AppointmentViewModel)tabelaPregledi.SelectedItem;
                    _notificationController.RemoveNotificationByAppointment(selectedAppointment.Id);
                    // Izbrisemo i iz view-a i iz baze
                    Appointments.Remove(selectedAppointment);
                    _appointmentController.RemoveAppointment(selectedAppointment.Id);
                    //activity
                    ActivityLog activity = new ActivityLog(DateTime.Now, _logedPatient.Id, TypeOfActivity.cancelAppointment);
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
