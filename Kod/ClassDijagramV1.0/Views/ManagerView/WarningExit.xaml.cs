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

namespace ClassDijagramV1._0.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for WarningExit.xaml
    /// </summary>
    public partial class WarningExit : Window
    {
        private WindowUpravnik windowUpravnik;
        public WarningExit(WindowUpravnik windowUpravnik)
        {
            InitializeComponent();
            this.windowUpravnik = windowUpravnik;
        }

        private void ExitManagerWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            windowUpravnik.Close();

        }

        private void CancelExitManagerWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
