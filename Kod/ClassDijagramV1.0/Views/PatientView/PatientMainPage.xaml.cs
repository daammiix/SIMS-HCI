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
    /// Interaction logic for PatientMainPage.xaml
    /// </summary>
    public partial class PatientMainPage : Page
    {
        #region Fields

        private Patient _logedPatient;

        private ObservableCollection<AppointmentViewModel> _appointmentViewModels;

        #endregion

        private PatientMainWindow parent { get; set; }
        public PatientMainPage(PatientMainWindow patientMain, Patient logedPatient)
        {
            InitializeComponent();
            parent = patientMain;
            _logedPatient = logedPatient;

            // napravimo listu appointmentViewModela od svakog appointmenta pacijenta
            _appointmentViewModels = new ObservableCollection<AppointmentViewModel>();
            foreach (Appointment a in _logedPatient.Appointments)
            {
                _appointmentViewModels.Add(new AppointmentViewModel(a));
            }

        }

        private void AppointmentsViewOpen(object sender, RoutedEventArgs e)
        {
            // Proslledimo appointmentViewModels da bi mogli da prikazemo appointmente i ulogovanog pacijenta
            parent.startWindow.Content = new AppointmentsViewPage(_appointmentViewModels, parent, _logedPatient);
        }

        private void AddAppointmetClick(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new AppointmentAddPage(parent, _appointmentViewModels, _logedPatient);
        }
    }
}
