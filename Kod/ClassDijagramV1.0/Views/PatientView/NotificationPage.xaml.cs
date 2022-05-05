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
        private static Timer aTimer;
        private int brojac;
        private MedicineDrug drug1;
        public AppointmentController _appointmentController;

        //ObservableCollection<MedicineDrug> Drugs;
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
                    //OnPropertyChanged("AppointmentID");
                }
            }
        }

        public ObservableCollection<Notification> Notifications
        {
            get { return _notification; }
            set
            {
                if (value != _notification)
                {
                    _notification = value;
                    //OnPropertyChanged("AppointmentID");
                }
            }
        }
        
        //ObservableCollection<MedicineDrug> Drugs = new ObservableCollection<MedicineDrug>();

        private PatientMainWindow parent { get; set; }
        public NotificationPage(PatientMainWindow patientMain)
        {
            InitializeComponent(); 
            this.DataContext = this;
            App app = Application.Current as App;
            _appointmentController = app.appointmentController;

            Drugs = new ObservableCollection<MedicineDrug>();
            //Notifications = new ObservableCollection<Notification>();
            Notifications = _appointmentController.GetAllNotifications();
            parent = patientMain; 
        }

        private void addDrug(object sender, RoutedEventArgs e)
        {
            //drug1 = new MedicineDrug("Lekadol", DateTime.Now.AddSeconds(10), DateTime.Now.AddSeconds(40), 8);
            //var start = new DateTime(2022, 4, 25, 20, 46 , 0);
            var start = DateTime.Now;
            drug1 = new MedicineDrug("Lekadol", start , start.AddMinutes(1), 8);    // doktor pravi recept, tj lijek koji se pije tad i tad
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

                Notification n = new Notification("1", "Popijte lijek " + drug1.MedicineName + " u " + DateTime.Now.AddSeconds(30), "djordje", false, drug1.StartTaking, NotificationType.doctorPrescription);
                // lijek se dodaje na notifikacije i pije se za 30 sekundi
                App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                {
                    Notifications.Add(n);
                });
            };

            var span = drug1.StartTaking.AddSeconds(5) - DateTime.Now;
            //var span = drug1.StartTaking.AddSeconds(-30) - DateTime.Now;    // pravi se tajmer od pravljenja recepta do prvog pica lijeka
            var timer = new Timer { Interval = span.TotalMilliseconds, AutoReset = false };
            timer.Elapsed += (sender, e) => { aTimer.Start(); };    // 30 sekundi prije dolazi notifikacija da se popije prvi lijek i pokrece se tajmer notifikacija
            timer.Start();

        }

        private void DataGridCheckBoxColumn_CopyingCellClipboardContent(object sender, DataGridCellClipboardEventArgs e)
        {

        }
    }
}
