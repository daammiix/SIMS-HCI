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
using System.Windows.Media;

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
        private BanningPatientController _banningPatientController;

        #endregion

        public PatientMainWindow(Patient p, Account account)
        {
            InitializeComponent();
            Patient = p;
            Account = account;
            this.DataContext = this;
            App app = Application.Current as App;
            _notificationController = app.NotificationController;
            _banningPatientController = app.BanningPatientController;
            pmp = new PatientMainPage(this);
            startWindow.Content = pmp;
            _notificationController.RemoveOldNotification();
            _notificationController.RemoveReadNotification();
            StartNotifications();
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
            var aTimer = new Timer();
            // prvo se okida ovaj tajmer, 
            var span = newStart.AddSeconds(-1) - DateTime.Now;
            var timer = new Timer { Interval = span.TotalMilliseconds, AutoReset = false };
            timer.Elapsed += (sender, e) => { aTimer.Start(); };
            timer.Start();
            
            aTimer.Elapsed += (sender, e) =>
            {
                aTimer.Interval = medicine.Interval * 60 * 60 * 1000;
                if (medicine.StopTaking.AddMinutes(-1) <= DateTime.Now)
                {
                    aTimer.Enabled = false;
                }
                Notification n = new Notification("Popijte lijek " + medicine.MedicineName + " u " + DateTime.Now.AddMinutes(1), Patient.Id, false, newStart, 0);
                App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                {
                    _notificationController.AddNotificationForTherapy(n);
                });
            };

           
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

        private void pocetna(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            startWindow.Content = new PatientMainPage(this);
        }
        private void appoinmentViewClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            startWindow.Content = new AppointmentsViewPage(this, pmp._appointmentViewModels);
        }
        private void prioritetDoktor(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (_banningPatientController.CheckStatusOfPatient(Patient.Id, Account) == true)
            {
                prioritetdoktor.Text = "Banovani ste!";
                prioritetdoktor.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                AppointmentAddPage aap = new AppointmentAddPage(this, pmp._appointmentViewModels);
                startWindow.Content = aap;
                aap.prioritetFrame.Content = new PriorityDoctor(this, pmp._appointmentViewModels);
                aap.doctorRB.IsChecked = true;
            }
        }

        private void prioritetVrijeme(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (_banningPatientController.CheckStatusOfPatient(Patient.Id, Account) == true)
            {
                prioritetvrijeme.Text = "Banovani ste!";
                prioritetvrijeme.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                AppointmentAddPage aap = new AppointmentAddPage(this, pmp._appointmentViewModels);
                startWindow.Content = aap;
                aap.prioritetFrame.Content = new PriorityTime(this, pmp._appointmentViewModels);
                aap.doctorRB.IsChecked = false;
                aap.timeRB.IsChecked = true;
            }
        }

        private void licniPodaci(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            startWindow.Content = new PatientDataPage(this);
        }

        private void istorijaPregleda(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            startWindow.Content = new OldAppointmentsPage(this, pmp._oldAppointmentViewModels);
        }

        private void uvidTerapija(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            startWindow.Content = new TerapyPage(this, pmp._oldAppointmentViewModels);
        }

        private void ocjeniDoktor(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            RatingPage rp = new RatingPage(this, pmp._oldAppointmentViewModels);
            startWindow.Content = rp;
            rp.doctorRB.IsChecked = true;
            rp.ocjeniteFrame.Content = new RatingDoctor(this, pmp._oldAppointmentViewModels);
        }

        private void ocjeniBolnica(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            RatingPage rp = new RatingPage(this, pmp._oldAppointmentViewModels);
            startWindow.Content = rp;
            rp.doctorRB.IsChecked = false;
            rp.hospitalRB.IsChecked = true;
            rp.ocjeniteFrame.Content = new RatingHospital(this);
        }

        private void alarm(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            startWindow.Content = new AddAlarmPage(this);
        }

        private void izvjestaj(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            startWindow.Content = new GenerateReportPage(this, pmp._appointmentViewModels);
        }
    }
}
