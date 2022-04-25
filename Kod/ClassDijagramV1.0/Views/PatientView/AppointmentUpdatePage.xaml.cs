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

using ClassDijagramV1._0.Views;
using ClassDijagramV1._0.Views.PatientView;
using Controller;
using Model;
using System.Collections.ObjectModel;

namespace ClassDijagramV1._0.Views.PatientView
{
    /// <summary>
    /// Interaction logic for AppointmentUpdatePage.xaml
    /// </summary>
    public partial class AppointmentUpdatePage : Page
    {
        public AppointmentController _appointmentController;
        public RoomController _roomController;
        public DoctorController _doctorController;
        private PatientMainWindow parent { get; set; }
        public ObservableCollection<Appointment> Appointments { get; set; }
        public ObservableCollection<Room> Rooms { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }
        public ObservableCollection<String> DoctorsAppointmentsTime { get; set; }
        public AppointmentUpdatePage(PatientMainWindow patientMain)
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

            BlackoutDates();
            //dodavanjPregledaDoktor.SelectedItem = (Doctor)AppointmentsViewPage.selectedAppointment.Doctor;
            promjenaKalendar.SelectedDate = AppointmentsViewPage.selectedAppointment.AppointmentDate;
            timeCB.SelectedItem = AppointmentsViewPage.selectedAppointment.AppointmentDate.ToString("HH:mm");
            dr.Text = AppointmentsViewPage.selectedAppointment.Doctor.Name + " " + AppointmentsViewPage.selectedAppointment.Doctor.Surname;
            date.Text = AppointmentsViewPage.selectedAppointment.AppointmentDate.ToString("HH:mm dd.MM.yyyy.");
            room.Text = AppointmentsViewPage.selectedAppointment.Room.RoomName.ToString();
        }

        private void BlackoutDates()
        {
            var oldDate = AppointmentsViewPage.selectedAppointment.AppointmentDate;
            var firstDate = DateTime.MinValue;
            var lastDate = DateTime.MaxValue;
            double NDays = 5;
            DateTime NDaysBefore = oldDate.AddDays(-NDays);
            DateTime NDaysAfter = oldDate.AddDays(NDays);
            promjenaKalendar.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, NDaysBefore));
            promjenaKalendar.BlackoutDates.Add(new CalendarDateRange(NDaysAfter, DateTime.MaxValue));
        }

        private void UpdateAppointmentClick(object sender, RoutedEventArgs e)
        {
            var oldAppointment = AppointmentsViewPage.selectedAppointment;
            var updatedAppointment = oldAppointment;

            //DateTime date1 = promjenaKalendar.SelectedDate.Value;
            Doctor d1 = (Doctor)dodavanjPregledaDoktor.SelectedItem;    
            
            int year = promjenaKalendar.SelectedDate.Value.Year;
            int month = promjenaKalendar.SelectedDate.Value.Month;
            int day = promjenaKalendar.SelectedDate.Value.Day;
            string[] getTimeCB = ((timeCB.SelectedItem).ToString()).Split(":");
            int hour = Int32.Parse(getTimeCB[0]);
            int minutes = Int32.Parse(getTimeCB[1]);
            DateTime date1 = new DateTime(year, month, day, hour, minutes, 0);
            
            Room r1 = getFreeRoom(date1, date1.AddMinutes(15));

            updatedAppointment.Doctor = d1;
            updatedAppointment.Room = r1;
            updatedAppointment.AppointmentDate = date1;


            _appointmentController.UpdateAppointment(oldAppointment.AppointmentID, updatedAppointment);
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
            if (dodavanjPregledaDoktor.SelectedItem != null && promjenaKalendar.SelectedDate != null)
            {
                foreach (Appointment termin in Appointments)
                {
                    if (l.Name == termin.Doctor.Name)
                    {
                        if (termin.AppointmentDate.Date.Equals(promjenaKalendar.SelectedDate))
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

            if (dodavanjPregledaDoktor.SelectedItem != null && promjenaKalendar.SelectedDate != null && timeCB.SelectedItem != null)
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
