using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using System;
using System.Collections.Generic;
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
            Boolean pon = ponedeljak.IsSelected;
            Boolean uto = utorak.IsSelected;
            Boolean sri = srijeda.IsSelected;
            Boolean cet = cetvrtak.IsSelected;
            Boolean pet = petak.IsSelected;
            Boolean sub = subota.IsSelected;
            Boolean ned = nedelja.IsSelected;

            Notification notification = new Notification(name,parent.Patient.Id,false,start,-1);
            _notificationController.AddManualNotification(notification);
            parent.startWindow.Content = new NotificationPage(parent,parent.Patient);
        }
    }
}
