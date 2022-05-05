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
        public AppointmentController _appointmentController;
        public RoomController _roomController;
        public static Appointment selectedAppointment;
        public PatientController _patientController;
        private PatientMainWindow parent { get; set; }
        public ObservableCollection<Appointment> Appointments
        {
            get;
            set;
        }
        public Patient PatientsByID { get; set; }
  
        public BindingList<Room> Rooms
        {
            get;
            set;
        }
        public AppointmentsViewPage(PatientMainWindow patientMain)
        {
            InitializeComponent();
            this.DataContext = this;
            parent = patientMain;
            App app = Application.Current as App;
            _appointmentController = app.appointmentController;

            _roomController = app.roomController;
            _patientController = app.PatientController;

            Appointments = _appointmentController.GetAllAppointmentsByPatient(parent.patientID); // ulogovani korisnik
            PatientsByID = _patientController.GetPatientById(parent.patientID);
        }
        private void AddAppontment_Click(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new AppointmentAddPage(parent);
            tabelaPregledi.ItemsSource = _appointmentController.GetAllAppointmentsByPatient(parent.patientID);
        }

        private void UpdateAppontment_Click(object sender, RoutedEventArgs e)
        {

            if (tabelaPregledi.SelectedIndex != -1)
            {
                selectedAppointment = (Appointment)tabelaPregledi.SelectedItem;
                parent.startWindow.Content = new AppointmentUpdatePage(parent);
                tabelaPregledi.ItemsSource = _appointmentController.GetAllAppointmentsByPatient(parent.patientID);
            }
        }

        private void RemoveAppontment_Click(object sender, RoutedEventArgs e)
        {
            if (tabelaPregledi.SelectedIndex != -1)
            {
                _appointmentController.RemoveAppointment((Appointment)tabelaPregledi.SelectedItem);
                //_appointmentController.AddNotification((Appointment)tabelaPregledi.SelectedItem, NotificationType.deletingAppointment);
                tabelaPregledi.ItemsSource = _appointmentController.GetAllAppointmentsByPatient(parent.patientID);
            }
        }
    }
}
