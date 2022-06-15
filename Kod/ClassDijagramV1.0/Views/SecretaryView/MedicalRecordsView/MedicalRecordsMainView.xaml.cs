using System.Windows.Controls;
using System.Windows.Input;

namespace ClassDijagramV1._0.Views.SecretaryView.MedicalRecordsView
{
    /// <summary>
    /// Interaction logic for MedicalRecordsMainView.xaml
    /// </summary>
    public partial class MedicalRecordsMainView : UserControl
    {
        public MedicalRecordsMainView()
        {
            InitializeComponent();
            Focusable = true;
            Loaded += (s, e) => Keyboard.Focus(this);
        }
    }
}
