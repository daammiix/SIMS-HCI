using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace ClassDijagramV1._0.Views.PatientView
{
    /// <summary>
    /// Interaction logic for AddAlarmPage.xaml
    /// </summary>
    public partial class AddAlarmPage : Page
    {
        private PatientMainWindow parent { get; set; }

        public NotificationController _notificationController;
        public AddAlarmPage(PatientMainWindow patientMain)
        {
            InitializeComponent();
            parent = patientMain;
            App app = Application.Current as App;
            _notificationController = app.NotificationController;
        }

        private void addAlarmClick(object sender, RoutedEventArgs e)
        {
            String name = naziv.Text;
            DateTime start = (DateTime)kalendarOD.SelectedDate;
            DateTime end = (DateTime)kalendarOD.SelectedDate;
            DateTime time = (DateTime)vrijeme.SelectedTime;
            DateTime start1 = new DateTime(start.Year, start.Month, start.Day, time.Hour, time.Minute, 0);
            DateTime end1 = new DateTime(end.Year, end.Month, end.Day, time.Hour, time.Minute, 0);
            Boolean pon = ponedeljak.IsSelected;
            Boolean uto = utorak.IsSelected;
            Boolean sri = srijeda.IsSelected;
            Boolean cet = cetvrtak.IsSelected;
            Boolean pet = petak.IsSelected;
            Boolean sub = subota.IsSelected;
            Boolean ned = nedelja.IsSelected;
            List<Boolean> daysSelected = new List<Boolean>() { pon, uto, sri, cet, pet, sub, ned };
            Notification notification = new Notification(name + " u " + start1, parent.Patient.Id, false, start1, 0);

            List<Boolean> DaysSelected = NoDaysSelected(daysSelected);

            addNotifications(notification, end1, DaysSelected);
            //_notificationController.AddManualNotification(notification);
            parent.startWindow.Content = new NotificationPage(parent);
        }

        private void addNotifications(Notification notification, DateTime end1, List<Boolean> DaysSelected)
        {
            var aTimer = new Timer();   // tajmer notifikacija
            aTimer.Elapsed += (sender, e) =>
            {
                aTimer.Interval = 24 * 60 * 60 * 1000;
                if (end1.AddSeconds(-1) <= DateTime.Now)
                {
                    aTimer.Enabled = false;
                }

                DaySelected(notification, DaysSelected);
            };

            // prvo se okida ovaj tajmer, 
            var span = notification.Created.AddSeconds(-1) - DateTime.Now;
            var timer = new Timer { Interval = span.TotalMilliseconds, AutoReset = false };
            timer.Elapsed += (sender, e) => { aTimer.Start(); };    // 30 sekundi prije dolazi notifikacija da se popije prvi lijek i pokrece se tajmer notifikacija
            timer.Start();
        }

        private List<Boolean> NoDaysSelected(List<Boolean> DaysSelected)
        {
            int brojac = 0;
            foreach (Boolean day in DaysSelected)
            {
                if (day)
                {
                    brojac++;
                }
            }
            if (brojac == 0)
            {
                List<Boolean> newDaysSelected = new List<Boolean>() { true,true,true,true,true,true,true};
                return newDaysSelected;
            }
            return DaysSelected;
        }
        private void DaySelected(Notification notification, List<Boolean> daysSelected)
        {
            if (System.DateTime.Now.DayOfWeek.ToString().Equals("Monday") && daysSelected[0])
            {
                PushNotification(notification);
            }
            if (System.DateTime.Now.DayOfWeek.ToString().Equals("Tuesday") && daysSelected[1])
            {
                PushNotification(notification);
            }
            if (System.DateTime.Now.DayOfWeek.ToString().Equals("Wednesday") && daysSelected[2])
            {
                PushNotification(notification);
            }
            if (System.DateTime.Now.DayOfWeek.ToString().Equals("Thursday") && daysSelected[3])
            {
                PushNotification(notification);
            }
            if (System.DateTime.Now.DayOfWeek.ToString().Equals("Friday") && daysSelected[4])
            {
                PushNotification(notification);
            }
            if (System.DateTime.Now.DayOfWeek.ToString().Equals("Saturday") && daysSelected[5])
            {
                PushNotification(notification);
            }
            if (System.DateTime.Now.DayOfWeek.ToString().Equals("Sunday") && daysSelected[6])
            {
                PushNotification(notification);
            }

        }

        private void PushNotification(Notification notification)
        {
            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
            {
                _notificationController.AddManualNotification(notification);
            });
        }
    }
}
