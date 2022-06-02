using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace ClassDijagramV1._0.Views.PatientView
{
    /// <summary>
    /// Interaction logic for PriorityDoctor.xaml
    /// </summary>
    public partial class PriorityDoctor : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(String propertyName)
        {
            PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
            PropertyChanged(this, e);
        }

        // pacijentovi appointmenti, lista appointmentViewModela prosledjena konstruktoru
        private ObservableCollection<AppointmentViewModel> _patientAppointments;
        // ulogovan pacijent
        private Patient _logedPatient;
        // kontroleri
        public AppointmentController _appointmentController;
        public RoomController _roomController;
        public RoomAppointmentController _roomAppointmentController;
        public DoctorController _doctorController;
        public ActivityController _activityController;
        public NotificationController _notificationController;

        private PatientMainWindow parent { get; set; }
        public ObservableCollection<Appointment> Appointments { get; private set; }
        public BindingList<Room> Rooms { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }
        public ObservableCollection<String> DoctorsAppointmentsTime { get; set; }

        public PriorityDoctor(PatientMainWindow patientMain, ObservableCollection<AppointmentViewModel> patientAppointments,
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
            _roomAppointmentController = app.roomAppointmentController;
            _activityController = app.ActivityController;
            _notificationController = app.NotificationController;

            Rooms = _roomController.GetAllRooms();
            Appointments = _appointmentController.GetAppointments();
            Doctors = _doctorController.GetAllDoctors();
            DoctorsAppointmentsTime = new ObservableCollection<String>();
        }

        private void AddAppointmentClick(object sender, RoutedEventArgs e)
        {
            int year = kalendar.SelectedDate.Value.Year;
            int month = kalendar.SelectedDate.Value.Month;
            int day = kalendar.SelectedDate.Value.Day;

            string[] getTimeCB = timeCB.SelectedItem.ToString().Split(":");
            int hour = Int32.Parse(getTimeCB[0]);
            int minutes = Int32.Parse(getTimeCB[1]);

            DateTime date = new DateTime(year, month, day, hour, minutes, 0);
            TimeSpan interval = date.AddMinutes(30) - date;

            Room r1 = getFreeRoom(date, interval);
            Doctor d1 = (Doctor)dodavanjPregledaDoktor.SelectedItem;

            Appointment a1 = new Appointment(_logedPatient.Id, d1.Id, r1.RoomID, date, interval, AppointmentType.generalPractitionerCheckup);

            _appointmentController.AddAppointment(a1);
            // Da bi se updatovao i view
            _patientAppointments.Add(new AppointmentViewModel(a1));
            _notificationController.AddNotification(a1, r1, NotificationType.addingAppointment);
            //activity
            ActivityLog activity = new ActivityLog(DateTime.Now, _logedPatient.Id, TypeOfActivity.makeAppointment);
            _activityController.AddActivity(activity);
            parent.startWindow.Content = new AppointmentsViewPage(_patientAppointments, parent, _logedPatient, parent.Account);
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

            Doctor l = (Doctor)dodavanjPregledaDoktor.SelectedItem;

            DoctorsAppointmentsTime = new ObservableCollection<string>();
            List<Appointment> termini = new List<Appointment>();

            if (dodavanjPregledaDoktor.SelectedItem != null && kalendar.SelectedDate != null)
            {
                foreach (Appointment termin in Appointments)
                {
                    Doctor doktor = _doctorController.GetDoctorById(termin.DoctorId);
                    if (l.Name == doktor.Name)
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
