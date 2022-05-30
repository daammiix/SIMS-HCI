using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for MedicalRecordPage.xaml
    /// </summary>
    public partial class MedicalRecordPage : Page
    {
        private Patient _logedPatient;
        private PatientMainWindow parent { get; set; }
        public ObservableCollection<AppointmentViewModel> OldAppointments
        {
            get;
            set;
        }
        public MedicalRecordPage(PatientMainWindow patientMain, Patient logedPatient, ObservableCollection<AppointmentViewModel> oldAppointmentViewModels)
        {
            InitializeComponent();
            parent = patientMain;
            _logedPatient = logedPatient;
            OldAppointments = oldAppointmentViewModels;
        }

        private void personalDataClick(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new PatientDataPage(parent);
        }

        private void oldAppointmentsClick(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new OldAppointmentsPage(parent, OldAppointments);
        }
    }
}
