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
    /// Interaction logic for PriorityTime.xaml
    /// </summary>
    public partial class PriorityTime : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(String propertyName)
        {
            PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
            PropertyChanged(this, e);
        }

        // pacijentovi appointmenti, lista appointmentViewModela prosledjena konstruktoru
        private ObservableCollection<AppointmentViewModel> _patientAppointments;
        // kontroleri
        private AppointmentController _appointmentController;
        private RoomController _roomController;
        private DoctorController _doctorController;
        private RoomAppointmentController _roomAppointmentController;
        private ActivityController _activityController;
        private NotificationController _notificationController;

        private PatientMainWindow parent { get; set; }
        public ObservableCollection<Appointment> Appointments { get; private set; }
        public BindingList<Room> Rooms { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }
        public ObservableCollection<Doctor> DoctorsAppointmentsTime { get; set; }

        public PriorityTime(PatientMainWindow patientMain, ObservableCollection<AppointmentViewModel> patientAppointments)
        {
            InitializeComponent();
            this.DataContext = this;
            parent = patientMain;
            _patientAppointments = patientAppointments;

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
            DoctorsAppointmentsTime = new ObservableCollection<Doctor>();

            fillTime();
        }

        private void fillTime()
        {
            List<String> allTime = new List<string>();
            for (DateTime tm = DateTime.Today.AddHours(7); tm < DateTime.Today.AddHours(15); tm = tm.AddMinutes(30))
            {
                allTime.Add(tm.ToString("HH:mm"));
            }
            timeCB.ItemsSource = allTime;
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

            Room r1 = _roomAppointmentController.GetFreeRoom(date, date + interval);
            Doctor d1 = (Doctor)dodavanjPregledaDoktor.SelectedItem;
            Appointment a1 = new Appointment(parent.Patient.Id, d1.Id, r1.RoomID, date, interval, AppointmentType.generalPractitionerCheckup);

            _appointmentController.AddAppointment(a1);
            _patientAppointments.Add(new AppointmentViewModel(a1));
            parent.Patient.Appointments.Add(a1);
            _notificationController.AddNotificationForAppointment(a1);
            ActivityLog activity = new ActivityLog(DateTime.Now, parent.Patient.Id, TypeOfActivity.makeAppointment);
            _activityController.AddActivity(activity);

            parent.startWindow.Content = new AppointmentsViewPage(parent, _patientAppointments);
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

