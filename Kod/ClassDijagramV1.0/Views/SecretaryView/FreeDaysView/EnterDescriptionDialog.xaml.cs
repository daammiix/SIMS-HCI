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
using System.Windows.Shapes;

namespace ClassDijagramV1._0.Views.SecretaryView.FreeDaysView
{
    /// <summary>
    /// Interaction logic for EnterDescriptionDialog.xaml
    /// </summary>
    public partial class EnterDescriptionDialog : Window
    {
        public EnterDescriptionDialog()
        {
            InitializeComponent();
        }

        #region Application State Event Handlers

        private void Header_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        #endregion
    }
}
