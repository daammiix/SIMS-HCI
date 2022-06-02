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
        public AddAlarmPage(PatientMainWindow patientMain)
        {
            InitializeComponent();
            parent = patientMain;
            App app = Application.Current as App;

            AddTime();
        }

        private void AddTime()
        {
            List<String> hours = new List<String>();
            List<String> minutes = new List<String>();
            for (int i = 0; i < 60; i++)
                minutes.Add(i.ToString());
            for (int i = 0; i < 24; i++)
                hours.Add(i.ToString());
            hourCB.ItemsSource = hours;
            minutesCB.ItemsSource = minutes;
        }
    }
}
