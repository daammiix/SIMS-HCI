using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private PatientMainWindow parent { get; set; }
        public ObservableCollection<Appointment> Appointments
        {
            get;
            set;
        }
        public ObservableCollection<Room> Rooms
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

            Appointments = _appointmentController.GetAllAppointments("djordje"); // ulogovani korisnik
            Rooms = _roomController.GetAllRooms();
        }
        private void AddAppontment_Click(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new AppointmentAddPage(parent);
        }

        private void UpdateAppontment_Click(object sender, RoutedEventArgs e)
        {

            if (tabelaPregledi.SelectedIndex != -1)
            {
                selectedAppointment = (Appointment)tabelaPregledi.SelectedItem;
                parent.startWindow.Content = new AppointmentUpdatePage(parent);
            }
        }

        private void RemoveAppontment_Click(object sender, RoutedEventArgs e)
        {
            if (tabelaPregledi.SelectedIndex != -1)
            {
                _appointmentController.RemoveAppointment((Appointment)tabelaPregledi.SelectedItem);
            }

        }
    }
}
