using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Model.DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;
using System.Windows;

namespace ClassDijagramV1._0.Views.PatientView
{
    /// <summary>
    /// Interaction logic for PatientMainWindow.xaml
    /// </summary>
    public partial class PatientMainWindow : Window
    {

        #region Properties

        // Pacijent koji je ulogovan
        public Patient Patient { get; set; }
        public Account Account { get; set; }
        public PatientMainPage pmp;
        private NotificationController _notificationController;

        #endregion

        public PatientMainWindow(Patient p, Account account)
        {
            InitializeComponent();
            Patient = p;
            Account = account;
            this.DataContext = this;
            App app = Application.Current as App;
            _notificationController = app.NotificationController;
            pmp = new PatientMainPage(this);
            startWindow.Content = pmp;
            _notificationController.RemoveOldNotification();
            _notificationController.RemoveReadNotification();
            StartNotifications();
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            var a = new MainWindow();
            a.Show();
            Window.GetWindow(this).Close();
        }

        private void openNotificationClick(object sender, RoutedEventArgs e)
        {
            startWindow.Content = new NotificationPage(this);
        }

        private void pocetnaClick(object sender, RoutedEventArgs e)
        {
            startWindow.Content = new PatientMainPage(this);
        }

        private void appoinmentViewClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            startWindow.Content = new AppointmentsViewPage(this, pmp._appointmentViewModels);
        }

        public void StartNotifications()
        {
            foreach (AppointmentViewModel app in pmp._oldAppointmentViewModels)
            {
                foreach (MedicineDrug t in app.Appointment.MedicalReport.Medicine)
                {
                    DateTime iterator = t.StartTaking;
                    while (iterator < DateTime.Now)
                    {
                        iterator = iterator.AddHours(t.Interval);
                    }
                    addNotifications(t, iterator);
                }
            }
        }


        private void addNotifications(MedicineDrug medicine, DateTime newStart) // za terapije
        {
            //drug1 = new MedicineDrug("Lekadol", DateTime.Now.AddSeconds(10), DateTime.Now.AddSeconds(40), 8);
            //var start = new DateTime(2022, 4, 25, 20, 46 , 0)

            var aTimer = new Timer();   // tajmer notifikacija
            aTimer.Elapsed += (sender, e) =>
            {
                aTimer.Interval = medicine.Interval * 60 * 60 * 1000;
                //if (drug1.StopTaking.AddSeconds(-30) <= DateTime.Now) // dok ne dodje zadnja notifikacija salje se
                if (medicine.StopTaking.AddMinutes(-1) <= DateTime.Now)
                {
                    aTimer.Enabled = false;
                }

                Notification n = new Notification("Popijte lijek " + medicine.MedicineName + " u " + DateTime.Now.AddMinutes(1), Patient.Id, false, newStart, 0);
                // lijek se dodaje na notifikacije i pije se za 30 sekundi
                App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                {
                    _notificationController.AddNotificationForTherapy(n);
                });
            };

            // prvo se okida ovaj tajmer, 
            var span = newStart.AddMinutes(-1) - DateTime.Now;
            //var span = drug1.StartTaking.AddSeconds(-30) - DateTime.Now;    // pravi se tajmer od pravljenja recepta do prvog pica lijeka
            var timer = new Timer { Interval = span.TotalMilliseconds, AutoReset = false };
            timer.Elapsed += (sender, e) => { aTimer.Start(); };    // 30 sekundi prije dolazi notifikacija da se popije prvi lijek i pokrece se tajmer notifikacija
            timer.Start();

        }
    }
}
