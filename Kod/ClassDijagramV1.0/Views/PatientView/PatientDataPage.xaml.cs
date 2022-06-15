using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using Model;
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
﻿using System.Windows.Controls;
using ClassDijagramV1._0.ViewModel.PatientViewModels;

namespace ClassDijagramV1._0.Views.PatientView
{
    /// <summary>
    /// Interaction logic for PatientDataPage.xaml
    /// </summary>
    public partial class PatientDataPage : Page
    {
        #region Fields
        private PatientMainWindow parent { get; set; }
        private MedicalRecordController _medicalRecordController;
        private Patient LoggedPatient { get; set; }
        #endregion
        public PatientDataPage(PatientMainWindow patientMain)
        {
            InitializeComponent();
            parent = patientMain;
            LoggedPatient = parent.Patient;
            //this.DataContext = LoggedPatient;
            //VIewMODEL
            this.DataContext = new PatientDataViewModel(parent);

            App app = Application.Current as App;
            _medicalRecordController = app.MedicalRecordController;

            MedicalRecord mr = _medicalRecordController.GetPatientsMedicalRecord(LoggedPatient.Id);
            alergeni.ItemsSource = mr.Allergens;
        }
    }
}
