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
    /// Interaction logic for UpdatePriorityTime.xaml
    /// </summary>
    public partial class UpdatePriorityTime : Page, INotifyPropertyChanged
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
        public ObservableCollection<Doctor> DoctorsAppointmentsTime { get; set; }
        public UpdatePriorityTime(PatientMainWindow patientMain, ObservableCollection<AppointmentViewModel> appointmentViewModels)
        {
            InitializeComponent();
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
            DoctorsAppointmentsTime = new ObservableCollection<Doctor>();

            BlackoutDates();
            fillTime();
            //izmjenaPregledaDoktor.SelectedItem = (Doctor)AppointmentsViewPage.SelectedAppointment.Doctor;
            promjenaKalendar.SelectedDate = AppointmentsViewPage.SelectedAppointment.AppointmentDate;
            timeCB.SelectedItem = AppointmentsViewPage.SelectedAppointment.AppointmentDate.ToString("HH:mm");
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

            DateTime danas = DateTime.Today;

            DoctorsAppointmentsTime = new ObservableCollection<Doctor>();
            List<Doctor> termini = new List<Doctor>();


            if (timeCB.SelectedItem != null && promjenaKalendar.SelectedDate != null)
            {
                string[] getTimeCB = timeCB.SelectedItem.ToString().Split(":");
                int hour = Int32.Parse(getTimeCB[0]);
                int minutes = Int32.Parse(getTimeCB[1]);
                foreach (Appointment termin in Appointments)
                {
                    if (termin.AppointmentDate.Date.Equals(promjenaKalendar.SelectedDate) && termin.AppointmentDate.Hour.Equals(hour) && termin.AppointmentDate.Minute.Equals(minutes))
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

            izmjenaPregledaDoktor.ItemsSource = DoctorsAppointmentsTime;


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
