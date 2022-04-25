using System;
using System.Collections.Generic;
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
using ClassDijagramV1._0.Util;
using Controller;
using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace ClassDijagramV1._0.Views.PatientView
{
    /// <summary>
    /// Interaction logic for AppointmentAddPage.xaml
    /// </summary>
    public partial class AppointmentAddPage : Page, INotifyPropertyChanged
    {
        public AppointmentController _appointmentController;
        public RoomController _roomController;
        public DoctorController _doctorController;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void onPropertyChanged(String propertyName)
        {
            PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
            PropertyChanged(this, e);
        }
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
        public ObservableCollection<Doctor> Doctors
        {
            get;
            set;
        }
        public ObservableCollection<String> DoctorsAppointmentsTime
        {
            get;
            set;
        }
        //private List<string> availableTimes;

        public AppointmentAddPage(PatientMainWindow patientMain)
        {
            InitializeComponent();
            this.DataContext = this;
            parent = patientMain;

            App app = Application.Current as App;
            _appointmentController = app.appointmentController;
            _roomController = app.roomController;
            _doctorController = app.doctorController;

            Rooms = _roomController.GetAllRooms();
            Appointments = _appointmentController.GetAllAppointments("djordje"); // ulgovani korisnik ali ovo je za doktora
            Doctors = _doctorController.GetAllDoctors();
            DoctorsAppointmentsTime = new ObservableCollection<String>();
        }


        private void AddAppointmentClick(object sender, RoutedEventArgs e)
        {

            Random rnd = new Random();
            int card = rnd.Next(50);

            //String appointmentID = (_appointmentController.GetAllAppointments("djordje").Count + 1).ToString();
            String appointmentID = rnd.Next(1000).ToString();
            int year = kalendar.SelectedDate.Value.Year;
            int month = kalendar.SelectedDate.Value.Month;
            int day = kalendar.SelectedDate.Value.Day;
            string[] getTimeCB = ((timeCB.SelectedItem).ToString()).Split(":");
            int hour = Int32.Parse(getTimeCB[0]);
            int minutes = Int32.Parse(getTimeCB[1]);
            DateTime date1 = new DateTime(year, month, day, hour, minutes, 0);

            DateTime date2 = date1.AddMinutes(30);
            TimeSpan interval = date2 - date1;

            Room r1 = getFreeRoom(date1, date2);
            Doctor d1 = (Doctor)dodavanjPregledaDoktor.SelectedItem;
            Patient p1 = new Patient("djordje", "djordje", "123", "musko", "3875432", "the292200", date1, "1234");

            Appointment a1 = new Appointment(appointmentID, p1, d1, r1, date1, interval, AppointmentType.generalPractitionerCheckup);
            _appointmentController.AddAppointment(a1);
            _appointmentController.AddNotification(a1.AppointmentID, NotificationType.addingAppointment, a1.Patient.Jmbg, a1.AppointmentDate);
            parent.startWindow.Content = new AppointmentsViewPage(parent);

        }

        private Room getFreeRoom(DateTime start, DateTime end) //FJA ZA DODJELU PRVE PRAZNE SOBE
        {
            //var freeRooms = new List<Room>;
            foreach (Room r in Rooms)
            {
                if (r.isFree(start, end)) // dodaj sebi polje isFree
                {
                    //freeRooms.Add(r);
                    return r;
                }

            }
            return null;
        }

        private void dodavanjPregledaDoktor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Doctor l = (Doctor)dodavanjPregledaDoktor.SelectedItem;

            DoctorsAppointmentsTime = new ObservableCollection<string>();
            List<Appointment> termini = new List<Appointment>();
            if (dodavanjPregledaDoktor.SelectedItem != null && kalendar.SelectedDate != null)
            {
                foreach (Appointment termin in Appointments)
                {
                    if (l.Name == termin.Doctor.Name)
                    {
                        if (termin.AppointmentDate.Date.Equals(kalendar.SelectedDate))
                        {
                            termini.Add(termin);
                        }
                    }
                }
            }

            DateTime danas = DateTime.Today;

            for (DateTime tm = danas.AddHours(7); tm < danas.AddHours(15); tm = tm.AddMinutes(30))
            {
                bool slobodno = true;
                foreach (Appointment termin in termini)
                {
                    DateTime start = DateTime.Parse(termin.AppointmentDate.ToString("HH:mm"));
                    DateTime end = DateTime.Parse(termin.AppointmentDate.AddMinutes(30).ToString("HH:mm"));
                    if (tm >= start && tm < end)
                    {
                        slobodno = false;
                    }
                }

                if (slobodno)
                    DoctorsAppointmentsTime.Add(tm.ToString("HH:mm"));

            }

            timeCB.ItemsSource = DoctorsAppointmentsTime;

            if (dodavanjPregledaDoktor.SelectedItem != null && kalendar.SelectedDate != null && timeCB.SelectedItem != null)
            {
                addAppBtn.IsEnabled = true;
            }
            else
            {
                addAppBtn.IsEnabled = false;
            }

        }
    }
}

