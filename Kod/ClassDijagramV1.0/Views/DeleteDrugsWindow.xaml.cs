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
    /// Interaction logic for DeleteDrugs.xaml
    /// </summary>
    public partial class DeleteDrugsWindow : Window
    {
        public DeleteDrugsWindow()
        {
            InitializeComponent();
        }

        private void DeleteYesDrugs_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DeleteNoDrugs_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
