using Model;
using System.Windows.Controls;

namespace ClassDijagramV1._0.Views.PatientView
{
    /// <summary>
    /// Interaction logic for TerapyPage.xaml
    /// </summary>
    public partial class TerapyPage : Page
    {
        private Patient _logedPatient;
        private PatientMainWindow parent { get; set; }
        public TerapyPage(PatientMainWindow patientMain, Patient logedPatient)
        {
            InitializeComponent();
            parent = patientMain;
            _logedPatient = logedPatient;
        }
    }
}
