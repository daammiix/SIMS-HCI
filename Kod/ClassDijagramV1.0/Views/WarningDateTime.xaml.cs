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

namespace ClassDijagramV1._0.Views
{
    /// <summary>
    /// Interaction logic for WarningDateTime.xaml
    /// </summary>
    public partial class WarningDateTime : Window
    {
        public WarningDateTime()
        {
            InitializeComponent();
        }

        private void WarningOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
