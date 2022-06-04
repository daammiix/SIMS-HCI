using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
    /// Interaction logic for NotificationPage.xaml
    /// </summary>
    public partial class NotificationPage : Page
    {
        private PatientMainWindow parent { get; set; }
        public Patient Patient { get; set; }

        private MedicineDrug drug1;
        public AppointmentController _appointmentController;
        public NotificationController _notificationController; 

        private ObservableCollection<MedicineDrug> _drugs;
        private ObservableCollection<Notification> _notification;
        public ObservableCollection<MedicineDrug> Drugs
        {
            get { return _drugs; }
            set
            {
                if (value != _drugs)
                {
                    _drugs = value;
                }
            }
        }

        public ObservableCollection<Notification> Notifications { get; set; }

        public NotificationPage(PatientMainWindow patientMain, Patient p)
        {
            InitializeComponent();
            this.DataContext = this;
            parent = patientMain;
            Patient = p;

            App app = Application.Current as App;
            _appointmentController = app.AppointmentController;
            _notificationController = app.NotificationController;

            Drugs = new ObservableCollection<MedicineDrug>();
            Notifications = _notificationController.GetAllNotifications();
            
        }

        private void addDrug(object sender, RoutedEventArgs e)
        {
            //drug1 = new MedicineDrug("Lekadol", DateTime.Now.AddSeconds(10), DateTime.Now.AddSeconds(40), 8);
            //var start = new DateTime(2022, 4, 25, 20, 46 , 0);
            var start = DateTime.Now.AddSeconds(5);
            drug1 = new MedicineDrug("Lekadol", start, start.AddMinutes(1), 8);    // doktor pravi recept, tj lijek koji se pije tad i tad
            Drugs.Add(drug1);

            const int interval = 5000;

            var aTimer = new Timer();   // tajmer notifikacija
            aTimer.Elapsed += (sender, e) =>
            {
                aTimer.Interval = interval;
                //if (drug1.StopTaking.AddSeconds(-30) <= DateTime.Now) // dok ne dodje zadnja notifikacija salje se
                if (drug1.StopTaking.AddSeconds(-5) <= DateTime.Now)
                {
                    aTimer.Enabled = false;
                }

                Notification n = new Notification("Popijte lijek " + drug1.MedicineName + " u " + DateTime.Now.AddSeconds(30), Patient.Id, false, drug1.StartTaking,0);
                // lijek se dodaje na notifikacije i pije se za 30 sekundi
                App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                {
                    Notifications.Add(n);
                });
            };

            // prvo se okida ovaj tajmer, 
            var span = drug1.StartTaking - DateTime.Now;
            //var span = drug1.StartTaking.AddSeconds(-30) - DateTime.Now;    // pravi se tajmer od pravljenja recepta do prvog pica lijeka
            var timer = new Timer { Interval = span.TotalMilliseconds, AutoReset = false };
            timer.Elapsed += (sender, e) => { aTimer.Start(); };    // 30 sekundi prije dolazi notifikacija da se popije prvi lijek i pokrece se tajmer notifikacija
            timer.Start();

        }

        private void addAlarmClick(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new AddAlarmPage(parent);
        }
    }
}
