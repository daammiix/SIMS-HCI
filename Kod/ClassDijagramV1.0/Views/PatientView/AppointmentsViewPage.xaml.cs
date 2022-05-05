using ClassDijagramV1._0.Dialog;
using ClassDijagramV1._0.Util;
using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        #endregion


        #region Properties

        public AppointmentViewModel SelectedAppointment { get; set; }

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

        public Patient Patient { get; set; }

        #endregion

        /// <summary>
        /// Pacijent koji je ulogovan
        /// </summary>
        /// <param name="p"></param>
        public AppointmentsViewPage(Patient p)
        {
            InitializeComponent();

            Patient = p;

            this.DataContext = this;
            App app = Application.Current as App;
            _appointmentController = app.AppointmentController;

            _roomController = app.roomController;

            // Ucitamo pacijentove appointmente i napravimo AppointmentViewModel od svakog
            Appointments = new ObservableCollection<AppointmentViewModel>();
            p.Appointments.ForEach(a =>
            {
                Appointments.Add(new AppointmentViewModel(a));
            });

            //Appointments = _appointmentController.GetAllAppointments("djordje"); // ulogovani korisnik
            Rooms = _roomController.GetAllRooms();
        }
        private void AddAppontment_Click(object sender, RoutedEventArgs e)
        {
            //_appointmentController.AddAppointment(a1);
            // Prosledimo listu AppointmentViewModela jer tu dodajemo novi appointment kako bi se view azurirao
            var a = new AddAppointmentDialog(Appointments);
            a.Show();
        }

        private void UpdateAppontment_Click(object sender, RoutedEventArgs e)
        {

            if (tabelaPregledi.SelectedIndex != -1)
            {
                var a = new UpdateAppointmentDialog(SelectedAppointment);
                a.Show();
            }

        }

        private void RemoveAppontment_Click(object sender, RoutedEventArgs e)
        {
            if (tabelaPregledi.SelectedIndex != -1)
            {
                //Appointments.Remove((Appointment)tabelaPregledi.SelectedItem);
                AppointmentViewModel selectedAppointment = (AppointmentViewModel)tabelaPregledi.SelectedItem;
                // Izbrisemo i iz view-a i iz baze
                Appointments.Remove(selectedAppointment);
                _appointmentController.RemoveAppointment(selectedAppointment.Id);
            }

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
            _room = _roomController.GetARoom(_appointment.RoomId);
            _doctor = _doctorController.GetDoctorById(_appointment.DoctorId);

        }

        #endregion
    }
}
