using System;
using System.Collections.Generic;
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

namespace ClassDijagramV1._0.Views.SecretaryView.ProfileView
{
    /// <summary>
    /// Interaction logic for ProfileMainView.xaml
    /// </summary>
    public partial class ProfileMainView : UserControl
    {
        public ProfileMainView()
        {
            InitializeComponent();
            Focusable = true;
            Loaded += (s, e) => Keyboard.Focus(this);
        }
    }
}
