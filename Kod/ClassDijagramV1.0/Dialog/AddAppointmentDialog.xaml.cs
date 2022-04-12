
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
            Appointments = _appointmentController.GetAllAppointments("djordje"); // ulgovani korisnik ali ovo je za doktora
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddAppointmentClick(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            int card = rnd.Next(50);

            //String appointmentID = (_appointmentController.GetAllAppointments("djordje").Count + 1).ToString();
            String appointmentID = rnd.Next(1000).ToString();
            DateTime date1 = kalendar.SelectedDate.Value;
            DateTime date2 = new DateTime(2022, 8, 18, 13, 30, 30);
            TimeSpan interval = date2 - date1;
            Room r1 = new Room();
            Doctor d1 = new Doctor("noviDoktor" + card.ToString(), "novidoktor", "123", "musko", "3875432", "the292200", date1, DoctorType.general, null);
            Patient p1 = new Patient("djordje", "djordje", "123", "musko", "3875432", "the292200", date1, "1234");
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

