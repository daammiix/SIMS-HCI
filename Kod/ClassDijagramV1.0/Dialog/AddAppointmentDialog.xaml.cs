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
using System.Windows.Shapes;

namespace ClassDijagramV1._0.Dialog
{
    /// <summary>
    /// Interaction logic for AddAppointmentDialog.xaml
    /// </summary>
    public partial class AddAppointmentDialog : Window
    {
        public AppointmentController _appointmentController;

        public ObservableCollection<Appointment> Appointments
        {
            get;
            set;
        }

        public AddAppointmentDialog()
        {
            InitializeComponent();
            _appointmentController = new AppointmentController();
            this.DataContext = this;
            Appointments = _appointmentController.GetAllAppointments();
            //Appointments = new ObservableCollection<Appointment>(_appointmentController.GetAllAppointments().ToList()); // od koga pacijenta
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddAppointmentClick(object sender, RoutedEventArgs e)
        {
            Room r1 = new Room();
            DateTime date1 = new DateTime(2008, 5, 1, 8, 30, 52);
            DateTime date2 = new DateTime(2010, 8, 18, 13, 30, 30);
            TimeSpan interval = date2 - date1;
            Doctor d1 = new Doctor("Drrrrjordje", "Lipovcic", "123", "musko", "3875432", "the292200", date1);
            Patient p1 = new Patient("Djordje", "Lipovcic", "123", "musko", "3875432", "the292200", date1, null, "1234", date1);
            Appointment a1 = new Appointment(p1, r1, d1, "3", date1, interval, AppointmentType.generalPractitionerCheckup);

            //Appointments.Add(a1);

            _appointmentController.AddAppointment(a1);
            Appointments = _appointmentController.GetAllAppointments();
        }


    }
}

