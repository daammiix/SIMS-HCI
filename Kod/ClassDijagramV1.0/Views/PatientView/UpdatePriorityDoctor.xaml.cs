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

        public AppointmentController _appointmentController;
        public RoomController _roomController;
        public DoctorController _doctorController;
        public RoomAppointmentController _roomAppointmentController;
        public ActivityController _activityController;
        private NotificationController _notificationController;

        private ObservableCollection<AppointmentViewModel> _appointmentViewModels;
        private PatientMainWindow parent { get; set; }
        public ObservableCollection<Appointment> Appointments { get; set; }
        public BindingList<Room> Rooms { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }
        public ObservableCollection<String> DoctorsAppointmentsTime { get; set; }

        public UpdatePriorityDoctor(PatientMainWindow patientMain, ObservableCollection<AppointmentViewModel> appointmentViewModels)
        {
            InitializeComponent();
            this.DataContext = this;
            parent = patientMain;
            _appointmentViewModels = appointmentViewModels;

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

            int year = promjenaKalendar.SelectedDate.Value.Year;
            int month = promjenaKalendar.SelectedDate.Value.Month;
            int day = promjenaKalendar.SelectedDate.Value.Day;

            string[] getTimeCB = timeCB.SelectedItem.ToString().Split(":");
            int hour = Int32.Parse(getTimeCB[0]);
            int minutes = Int32.Parse(getTimeCB[1]);

            DateTime date = new DateTime(year, month, day, hour, minutes, 0);
            TimeSpan interval = date.AddMinutes(30) - date;

            Room r1 = _roomAppointmentController.GetFreeRoom(date, date + interval);
            Doctor d1 = (Doctor)izmjenaPregledaDoktor.SelectedItem;

            oldAppointment.Doctor = d1;
            oldAppointment.Room = r1;
            oldAppointment.AppointmentDate = date;

            updatedAppointment.DoctorId = d1.Id;
            updatedAppointment.RoomId = r1.RoomID;
            updatedAppointment.AppointmentDate = date;

            _appointmentController.UpdateAppointment(oldAppointment.Id, updatedAppointment);
            //activity
            ActivityLog activity = new ActivityLog(DateTime.Now, parent.Patient.Id, TypeOfActivity.editAppointment);
            _activityController.AddActivity(activity);
            _notificationController.RemoveNotificationByAppointment(oldAppointment.Id);
            _notificationController.AddNotificationForAppointment(updatedAppointment);

            parent.startWindow.Content = new AppointmentsViewPage(parent,_appointmentViewModels);
        }

        private void dodavanjPregledaDoktor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            DoctorsAppointmentsTime = new ObservableCollection<string>();
            List<Appointment> termini = new List<Appointment>();
           
            if (izmjenaPregledaDoktor.SelectedItem != null && promjenaKalendar.SelectedDate != null)
            {
                termini = GetDoctorsAppointmentsThatDay();    
            }

            FillDoctorsAppointmntsTime(termini);
            EnableButton();
        }

        private List<Appointment> GetDoctorsAppointmentsThatDay()
        {
            List<Appointment> termini = new List<Appointment>();
            Doctor l = (Doctor)izmjenaPregledaDoktor.SelectedItem;
            foreach (Appointment termin in Appointments)
            {
                if (l.Id == termin.DoctorId
                    && termin.AppointmentDate.Date.Equals(promjenaKalendar.SelectedDate))
                {
                    termini.Add(termin);
                }
            }
            return termini;
        }

        private void FillDoctorsAppointmntsTime(List<Appointment> termini)
        {
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
        }

        private void EnableButton()
        {
            if (izmjenaPregledaDoktor.SelectedItem != null && promjenaKalendar.SelectedDate != null && timeCB.SelectedItem != null)
            {
                addAppBtn.IsEnabled = true;
            }
            else
            {
                addAppBtn.IsEnabled = false;
            }
        }

        private void addDoctor(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new PatientDataPage(parent);
        }
    }
}
