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

namespace ClassDijagramV1._0.Views.PatientView
{
    /// <summary>
    /// Interaction logic for PrivremenoLoginDugme.xaml
    /// </summary>
    public partial class PrivremenoLoginDugme : UserControl
    {
        public PrivremenoLoginDugme()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var PacijentHome = new PatientMainWindow();
            PacijentHome.Show();
            Window.GetWindow(this).Close();
            //MainWindow.Close();
        }


    }
}
