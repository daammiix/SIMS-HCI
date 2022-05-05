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
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.Views.PatientView;
using Controller;
using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ClassDijagramV1._0.Views.PatientView
{
    /// <summary>
    /// Interaction logic for PriorityTime.xaml
    /// </summary>
    public partial class PriorityTime : Page, INotifyPropertyChanged
    {
        // pacijentovi appointmenti, lista appointmentViewModela prosledjena konstruktoru
        private ObservableCollection<AppointmentViewModel> _patientAppointments;

        // ulogovan pacijent
        private Patient _logedPatient;

        public AppointmentController _appointmentController;
        public RoomController _roomController;
        public DoctorController _doctorController;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(String propertyName)
        {
            PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
            PropertyChanged(this, e);
        }
        private PatientMainWindow parent { get; set; }

        public ObservableCollection<Appointment> Appointments
        {
            get;
            private set;
        }
        public BindingList<Room> Rooms
        {
            get;
            set;
        }
        public ObservableCollection<Doctor> Doctors
        {
            get;
            set;
        }
        public ObservableCollection<Doctor> DoctorsAppointmentsTime
        {
            get;
            set;
        }
        public PriorityTime(PatientMainWindow patientMain, ObservableCollection<AppointmentViewModel> patientAppointments,
            Patient logedPatient)
        {
            InitializeComponent();
            this.DataContext = this;
            parent = patientMain;

            _patientAppointments = patientAppointments;

            _logedPatient = logedPatient;

            App app = Application.Current as App;
            _appointmentController = app.AppointmentController;
            _roomController = app.roomController;
            _doctorController = app.DoctorController;

            Rooms = _roomController.GetAllRooms();
            Appointments = _appointmentController.GetAllAppointments(); // ulgovani korisnik ali ovo je za doktora OVDE TReBAJU SVI TERMINI
            Doctors = _doctorController.GetAllDoctors();
            DoctorsAppointmentsTime = new ObservableCollection<Doctor>();

            fillTime();
        }

        private void fillTime()
        {
            List<String> allTime = new List<string>();
            DateTime danas = DateTime.Today;

            for (DateTime tm = danas.AddHours(7); tm < danas.AddHours(15); tm = tm.AddMinutes(30))
            {
                allTime.Add(tm.ToString("HH:mm"));
            }
            timeCB.ItemsSource = allTime;
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
            // Patient p1 = new Patient("djordje", "djordje", "123", "musko", "3875432", "the292200", date1, new Address("Srbija", "Novi Sad", "Marije Kalas", "320"), "1234");

            Appointment a1 = new Appointment(_logedPatient.Id, d1.Id, r1.RoomID, date1, interval, AppointmentType.generalPractitionerCheckup);

            _appointmentController.AddAppointment(a1);
            // Da bi se updatovao i view
            _patientAppointments.Add(new AppointmentViewModel(a1));
            _appointmentController.AddNotification(a1, r1, NotificationType.addingAppointment);
            parent.startWindow.Content = new AppointmentsViewPage(_patientAppointments, parent, _logedPatient);

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

            DateTime danas = DateTime.Today;

            


            DoctorsAppointmentsTime = new ObservableCollection<Doctor>();
            List<Doctor> termini = new List<Doctor>();


            if (timeCB.SelectedItem != null && kalendar.SelectedDate != null)
            {
                string[] getTimeCB = timeCB.SelectedItem.ToString().Split(":");
                int hour = Int32.Parse(getTimeCB[0]);
                int minutes = Int32.Parse(getTimeCB[1]);
                foreach (Appointment termin in Appointments)
                {
                        if (termin.AppointmentDate.Date.Equals(kalendar.SelectedDate) && termin.AppointmentDate.Hour.Equals(hour) && termin.AppointmentDate.Minute.Equals(minutes))
                        {
                            Doctor doktor = _doctorController.GetDoctorById(termin.DoctorId);
                            termini.Add(doktor);
                        } 
                }
                foreach (Doctor dr1 in Doctors)
                {
                    bool slobodno = true;
                    foreach (Doctor dr2 in termini)
                    {
                        if (dr1.Id.Equals(dr2.Id))
                        {
                            slobodno = false;
                        }

                    }
                    if (slobodno)
                        DoctorsAppointmentsTime.Add(dr1);
                }
            }

                dodavanjPregledaDoktor.ItemsSource = DoctorsAppointmentsTime;
            

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
        
