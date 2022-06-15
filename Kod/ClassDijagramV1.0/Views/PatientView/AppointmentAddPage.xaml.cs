using Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;


namespace ClassDijagramV1._0.Views.PatientView
{
    /// <summary>
    /// Interaction logic for AppointmentAddPage.xaml
    /// </summary>
    public partial class AppointmentAddPage : Page
    {
        #region Fields
        private PatientMainWindow parent { get; set; }
        private ObservableCollection<AppointmentViewModel> _appointments;
        #endregion

        public AppointmentAddPage(PatientMainWindow patientMain, ObservableCollection<AppointmentViewModel> appointmentViewModels)
        {
            InitializeComponent();
            this.DataContext = this;
            parent = patientMain;
            _appointments = appointmentViewModels;
            doctorRB.IsChecked = true;
        }

        private void doctorRB_Checked(object sender, RoutedEventArgs e)
        {
            prioritetFrame.Content = new PriorityDoctor(parent, _appointments);
        }

        private void timeRB_Checked(object sender, RoutedEventArgs e)
        {
            prioritetFrame.Content = new PriorityTime(parent, _appointments);
        }
    }
}

