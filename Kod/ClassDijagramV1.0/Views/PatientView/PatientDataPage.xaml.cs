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

namespace ClassDijagramV1._0.Views.PatientView
{
    /// <summary>
    /// Interaction logic for PatientDataPage.xaml
    /// </summary>
    public partial class PatientDataPage : Page
    {
        private PatientMainWindow parent { get; set; }

        private MedicalRecordController _medicalRecordController;
        public Patient LoggedPatient { get; set; }
        public MedicalRecord medicalRecord { get; set; }
        public PatientDataPage(PatientMainWindow patientMain)
        {
            InitializeComponent();
            App app = Application.Current as App;
            _medicalRecordController = app.MedicalRecordController;
            parent = patientMain;
            LoggedPatient = parent.Patient;
            this.DataContext = LoggedPatient;

            MedicalRecord mr = _medicalRecordController.GetPatientsMedicalRecord(LoggedPatient.Id);
            alergeni.ItemsSource = mr.Allergens;
        }
    }
}
