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
        #region Fields
        private PatientMainWindow parent { get; set; }
        private ObservableCollection<AppointmentViewModel> _oldAppointmentViewModels;
        #endregion
        public RatingPage(PatientMainWindow patientMain, ObservableCollection<AppointmentViewModel> appointmentViewModels)
        {
            InitializeComponent();
            parent = patientMain;
            _oldAppointmentViewModels = appointmentViewModels;
            doctorRB.IsChecked = true;
        }

        private void doctorRB_Checked(object sender, RoutedEventArgs e)
        {
            ocjeniteFrame.Content = new RatingDoctor(parent, _oldAppointmentViewModels);
        }

        private void hospitalRB_Checked(object sender, RoutedEventArgs e)
        {
            ocjeniteFrame.Content = new RatingHospital(parent);
        }
    }
}
