using Controller;
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

namespace ClassDijagramV1._0.Views
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
