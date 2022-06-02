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

        // prosledjena lista u koju se dodaj appointmentViewModel kako bi se view azurirao
        private ObservableCollection<AppointmentViewModel> _appointments;

        // ulogovan pacijent
        private Patient _logedPatient;

        #endregion


        private PatientMainWindow parent { get; set; }

        public AppointmentAddPage(PatientMainWindow patientMain,
            ObservableCollection<AppointmentViewModel> appointmentViewModels, Patient logedPatient)
        {
            InitializeComponent();
            _logedPatient = logedPatient;

            this.DataContext = this;
            parent = patientMain;

            _appointments = appointmentViewModels;


            doctorRB.IsChecked = true;
        }

        private void doctorRB_Checked(object sender, RoutedEventArgs e)
        {
            prioritetFrame.Content = new PriorityDoctor(parent, _appointments, _logedPatient);
        }

        private void timeRB_Checked(object sender, RoutedEventArgs e)
        {
            prioritetFrame.Content = new PriorityTime(parent, _appointments, _logedPatient);
        }
    }
}

