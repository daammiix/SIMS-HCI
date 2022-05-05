﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for PatientMainPage.xaml
    /// </summary>
    public partial class PatientMainPage : Page
    {
        private PatientMainWindow parent { get; set; }
        public PatientMainPage(PatientMainWindow patientMain)
        {
            InitializeComponent();
            parent = patientMain;
        }

        private void AppointmentsViewOpen(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new AppointmentsViewPage(parent.Patient);
        }
    }
}
