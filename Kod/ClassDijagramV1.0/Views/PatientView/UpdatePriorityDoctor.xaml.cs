using ClassDijagramV1._0.Controller;
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
    /// Interaction logic for UpdatePriorityDoctor.xaml
    /// </summary>
    public partial class UpdatePriorityDoctor : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(String propertyName)
        {
            PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
            PropertyChanged(this, e);
        }

        // lista pacijentovi appointmentViewModela
        private ObservableCollection<AppointmentViewModel> _appointmentViewModels;
        // ulogovan pacijent
        private Patient _logedPatient;
        //
        public AppointmentController _appointmentController;
        public RoomController _roomController;
        public DoctorController _doctorController;
        public RoomAppointmentController _roomAppointmentController;

        private PatientMainWindow parent { get; set; }
        public ObservableCollection<Appointment> Appointments { get; set; }
        public BindingList<Room> Rooms { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }
        public ObservableCollection<String> DoctorsAppointmentsTime { get; set; }

        public UpdatePriorityDoctor(PatientMainWindow patientMain,
            ObservableCollection<AppointmentViewModel> appointmentViewModels, Patient logedPatient)
        {
            InitializeComponent();
            parent = patientMain;
            _appointmentViewModels = appointmentViewModels;
            _logedPatient = logedPatient;

            App app = Application.Current as App;
            _appointmentController = app.AppointmentController;
            _roomController = app.roomController;
            _doctorController = app.DoctorController;
            _roomAppointmentController = app.roomAppointmentController;

            Rooms = _roomController.GetAllRooms();
            Appointments = _appointmentController.GetAppointments();
            Doctors = _doctorController.GetAllDoctors();
            DoctorsAppointmentsTime = new ObservableCollection<String>();

            BlackoutDates();
            izmjenaPregledaDoktor.ItemsSource = Doctors;
            izmjenaPregledaDoktor.SelectedItem = (Doctor)AppointmentsViewPage.SelectedAppointment.Doctor;
            promjenaKalendar.SelectedDate = AppointmentsViewPage.SelectedAppointment.AppointmentDate;
            //timeCB.SelectedItem = AppointmentsViewPage.SelectedAppointment.AppointmentDate.ToString("HH:mm");
        }
        private void BlackoutDates()
        {
            var oldDate = AppointmentsViewPage.SelectedAppointment.AppointmentDate;
            double NDays = 5;
            DateTime NDaysBefore = oldDate.AddDays(-NDays);
            DateTime NDaysAfter = oldDate.AddDays(NDays);
            promjenaKalendar.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, NDaysBefore));
            promjenaKalendar.BlackoutDates.Add(new CalendarDateRange(NDaysAfter, DateTime.MaxValue));
        }

        private void UpdateAppointmentClick(object sender, RoutedEventArgs e)
        {
            var oldAppointment = AppointmentsViewPage.SelectedAppointment;
            Appointment updatedAppointment = _appointmentController.GetAppointmentById(oldAppointment.Id);

            Doctor d1 = (Doctor)izmjenaPregledaDoktor.SelectedItem;

            int year = promjenaKalendar.SelectedDate.Value.Year;
            int month = promjenaKalendar.SelectedDate.Value.Month;
            int day = promjenaKalendar.SelectedDate.Value.Day;

            string[] getTimeCB = timeCB.SelectedItem.ToString().Split(":");
            int hour = Int32.Parse(getTimeCB[0]);
            int minutes = Int32.Parse(getTimeCB[1]);

            DateTime date = new DateTime(year, month, day, hour, minutes, 0);
            TimeSpan interval = date.AddMinutes(30) - date;

            Room r1 = getFreeRoom(date, interval);

            oldAppointment.Doctor = d1;
            oldAppointment.Room = r1;
            oldAppointment.AppointmentDate = date;

            updatedAppointment.DoctorId = d1.Id;
            updatedAppointment.RoomId = r1.RoomID;
            updatedAppointment.AppointmentDate = date;

            _appointmentController.UpdateAppointment(oldAppointment.Id, updatedAppointment);
            parent.startWindow.Content = new AppointmentsViewPage(_appointmentViewModels, parent, _logedPatient);
        }

        private Room getFreeRoom(DateTime start, TimeSpan interval)
        {
            foreach (Room room in Rooms)
            {
                if (_roomAppointmentController.GetFreeRoom(room, start, start + interval))
                {
                    return room;
                }
            }
            return null;
        }
        private void dodavanjPregledaDoktor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Doctor l = (Doctor)izmjenaPregledaDoktor.SelectedItem;

            DoctorsAppointmentsTime = new ObservableCollection<string>();
            List<Appointment> termini = new List<Appointment>();
            if (izmjenaPregledaDoktor.SelectedItem != null && promjenaKalendar.SelectedDate != null)
            {
                foreach (Appointment termin in Appointments)
                {
                    Doctor d = _doctorController.GetDoctorById(termin.DoctorId);
                    if (l.Id == d.Id)
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

            if (izmjenaPregledaDoktor.SelectedItem != null && promjenaKalendar.SelectedDate != null && timeCB.SelectedItem != null)
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
