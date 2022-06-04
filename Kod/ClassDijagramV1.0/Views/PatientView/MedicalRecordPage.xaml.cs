using Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace ClassDijagramV1._0.Views.PatientView
{
    /// <summary>
    /// Interaction logic for MedicalRecordPage.xaml
    /// </summary>
    public partial class MedicalRecordPage : Page
    {
        #region Fields
        private PatientMainWindow parent { get; set; }
        public ObservableCollection<AppointmentViewModel> OldAppointments { get; set; }
        #endregion
        public MedicalRecordPage(PatientMainWindow patientMain, ObservableCollection<AppointmentViewModel> oldAppointmentViewModels)
        {
            InitializeComponent();
            parent = patientMain;
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
