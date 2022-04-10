using ClassDijagramV1._0.Model.Accounts;
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

namespace ClassDijagramV1._0.Views
{
    /// <summary>
    /// Interaction logic for Pacijent.xaml
    /// </summary>
    public partial class PatientView : UserControl
    {
        public AppointmentController _appointmentController;

        public ObservableCollection<Appointment> Appointments
        {
            get;
            set;
        }

        /*public ObservableCollection<Doctor> Doctors
        {
            get;
            set;
        }*/

        public PatientView()
        {
            InitializeComponent();
            _appointmentController = new AppointmentController();
            this.DataContext = this;

            //Appointments = new ObservableCollection<Appointment>();
            //Doctors = new ObservableCollection<Doctor>();

            //var app = Application.Current as App;
            //_appointmentController = app.appointmentController;
            //Appointments = new ObservableCollection<Appointment>(_appointmentController.GetAllAppointments());
            Appointments = _appointmentController.GetAllAppointments();
            //tabelaPregledi.ItemsSource = Appointments;

        }


        private void AddAppontment_Click(object sender, RoutedEventArgs e)
        {
            Room r1 = new Room();
            DateTime date1 = new DateTime(2008, 5, 1, 8, 30, 52);
            DateTime date2 = new DateTime(2010, 8, 18, 13, 30, 30);
            TimeSpan interval = date2 - date1;
            Doctor d1 = new Doctor("Drrrrjordje", "Lipovcic", "123", "musko", "3875432", "the292200", date1);
            Patient p1 = new Patient("Djordje", "Lipovcic", "123", "musko", "3875432", "the292200", date1, null, "1234", date1);
            Appointment a1 = new Appointment(p1, r1, d1, "3", date1, interval, AppointmentType.generalPractitionerCheckup);
            // Appointments.Add(a1);
            _appointmentController.AddAppointment(a1);
            //var a = new AddAppointmentDialog();
            //Application.Current.MainWindow = a;
            //a.Show();
            //TREBA DA SE BINDUJE IZ DRUGIH PROYORA
        }

        private void UpdateAppontment_Click(object sender, RoutedEventArgs e)
        {
            /*var a = new UpdateAppointmentDialog();
            a.Show();*/

        }

        private void RemoveAppontment_Click(object sender, RoutedEventArgs e)
        {
            if (tabelaPregledi.SelectedIndex != -1)
            {
                //Appointments.Remove((Appointment)tabelaPregledi.SelectedItem);
                _appointmentController.RemoveAppointment((Appointment)tabelaPregledi.SelectedItem);
            }

        }

        private void AutoColumns_Click(object sender, RoutedEventArgs e)
        {
        }

        private void AddAppontment_Click1(object sender, RoutedEventArgs e)
        {

        }
    }
}
