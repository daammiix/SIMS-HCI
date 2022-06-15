using System.Windows.Controls;
using System.Windows.Input;

namespace ClassDijagramV1._0.Views.SecretaryView.EquipmentView
{
    /// <summary>
    /// Interaction logic for EquipmentMainView.xaml
    /// </summary>
    public partial class EquipmentMainView : UserControl
    {
        public EquipmentMainView()
        {
            InitializeComponent();
            Focusable = true;
            Loaded += (s, e) => Keyboard.Focus(this);
        }
    }
}
