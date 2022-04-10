
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
            this.DataContext = this;

            App app = Application.Current as App;
            _appointmentController = app.appointmentController;
            Appointments = _appointmentController.GetAllAppointments("djordje");
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddAppointmentClick(object sender, RoutedEventArgs e)
        {
            String appointmentID = (_appointmentController.GetAllAppointments("djordje").Count + 1).ToString();
            DateTime date11 = new DateTime(2008, 5, 1, 8, 30, 52);

            DateTime date1 = kalendar.SelectedDate.Value;
            DateTime date2 = new DateTime(2010, 8, 18, 13, 30, 30);
            TimeSpan interval = date2 - date1;
            Room r1 = new Room();
            //Room r2 = getFreeRoom(date1, date1 + interval);

            Doctor d1 = new Doctor("noviDoktor", "novidoktor", "123", "musko", "3875432", "the292200", date1);
            Patient p1 = new Patient("djordje", "djordje", "123", "musko", "3875432", "the292200", date1, null, "1234", date1);
            Appointment a1 = new Appointment(p1, r1, d1, appointmentID, date1, interval, AppointmentType.generalPractitionerCheckup);

            _appointmentController.AddAppointment(a1);
            this.Close();

        }

        /*private Room getFreeRoom(DateTime start, DateTime end) //FJA ZA DODJELU PRVE PRAZNE SOBE
        {
            var freeRooms = new List<Room>;
            foreach (Room r in _roomController.GetAllRooms())
            {
                if (r.isFree(start, end)) // dodaj sebi polje isFree
                {
                    freeRooms.Add(r);
                    break;
                }
            }
            return freeRooms.ElementAt(0);
        }*/
    }
}

