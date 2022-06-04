using Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace ClassDijagramV1._0.Views.PatientView
{
    /// <summary>
    /// Interaction logic for RatingPage.xaml
    /// </summary>
    public partial class RatingPage : Page
    {
        private Patient _logedPatient;
        private PatientMainWindow parent { get; set; }
        private ObservableCollection<AppointmentViewModel> _appointments;
        public RatingPage(PatientMainWindow patientMain,
            ObservableCollection<AppointmentViewModel> appointmentViewModels, Patient logedPatient)
        {
            InitializeComponent();
            parent = patientMain;
            _logedPatient = logedPatient;
            _appointments = appointmentViewModels;
            doctorRB.IsChecked = true;
        }

        private void doctorRB_Checked(object sender, RoutedEventArgs e)
        {
            ocjeniteFrame.Content = new RatingDoctor(_appointments, parent, _logedPatient);
        }

        private void hospitalRB_Checked(object sender, RoutedEventArgs e)
        {
            ocjeniteFrame.Content = new RatingHospital(parent, _logedPatient);
        }
    }
}
