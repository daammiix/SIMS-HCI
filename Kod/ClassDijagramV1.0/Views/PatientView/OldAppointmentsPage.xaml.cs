using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.ViewModel.PatientViewModels;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for OldAppointmentsPage.xaml
    /// </summary>
    public partial class OldAppointmentsPage : Page
    {
        private PatientMainWindow parent { get; set; }
        public ObservableCollection<AppointmentViewModel> OldAppointments
        {
            get;
            set;
        }
        public MedicineController _medicineController;
        public OldAppointmentsPage(PatientMainWindow patientMain, ObservableCollection<AppointmentViewModel> oldAppointmentViewModels)
        {
            InitializeComponent();
            this.DataContext = this;
            //this.DataContext = new OldAppointmentsViewModel();
            parent = patientMain;
            OldAppointments = oldAppointmentViewModels;
            App app = Application.Current as App;
            _medicineController = app.medicinesController;
        }

        private void openSpecificAppointmentClick(object sender, RoutedEventArgs e)
        {
            AppointmentViewModel selectedAppointment = (AppointmentViewModel)tabelaStariPregledi.SelectedItem;
           
            parent.startWindow.Content = new AppointmentReportPage(parent, selectedAppointment.Appointment);
        }
    }
}
