using System.Windows.Controls;
using System.Windows.Input;

namespace ClassDijagramV1._0.Views.SecretaryView.AccountsView
{
    /// <summary>
    /// Interaction logic for NaloziView.xaml
    /// </summary>
    public partial class AccountsMainView : UserControl
    {
        public AccountsMainView()
        {
            InitializeComponent();
            Focusable = true;
            Loaded += (s, e) => Keyboard.Focus(this);
        }
    }
}
