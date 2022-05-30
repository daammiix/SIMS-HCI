﻿using Model;
using System;
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
    /// Interaction logic for TerapyPage.xaml
    /// </summary>
    public partial class TerapyPage : Page
    {
        private Patient _logedPatient;
        private PatientMainWindow parent { get; set; }
        public TerapyPage(PatientMainWindow patientMain, Patient logedPatient)
        {
            InitializeComponent();
            parent = patientMain;
            _logedPatient = logedPatient;
        }
    }
}
