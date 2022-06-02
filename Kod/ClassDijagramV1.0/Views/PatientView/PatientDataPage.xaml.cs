using System.Windows.Controls;

namespace ClassDijagramV1._0.Views.PatientView
{
    /// <summary>
    /// Interaction logic for PatientDataPage.xaml
    /// </summary>
    public partial class PatientDataPage : Page
    {
        private PatientMainWindow parent { get; set; }
        public PatientDataPage(PatientMainWindow patientMain)
        {
            InitializeComponent();
            parent = patientMain;
        }
    }
}
