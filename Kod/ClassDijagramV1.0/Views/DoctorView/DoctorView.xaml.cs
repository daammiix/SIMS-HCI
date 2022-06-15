using Controller;
using Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace ClassDijagramV1._0.Views.DoctorView
{
    /// <summary>
    /// Interaction logic for DoctorView.xaml
    /// </summary>
    /// 

    public partial class DoctorView : UserControl
    {


        public SurgeryController _surgeryController;
        public PatientController _pc;

        public ObservableCollection<Surgery> Surgeries { get; set; }
        public ObservableCollection<Patient> Patients { get; set; }
        public DoctorView()
        {
            InitializeComponent();
            this.DataContext = this;

            App app = Application.Current as App;
            _surgeryController = app.surgeryController;
            _pc = app.PatientController;
            //Surgeries = _surgeryController.GetAllSurgeries(); // ulogovani korisnik
            Patients = _pc.GetPatients();

        }
    }
}
