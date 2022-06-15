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
    /// Interaction logic for GenerateNoticeWindow.xaml
    /// </summary>
    public partial class GenerateNoticeWindow : Window
    {
        public GenerateNoticeWindow()
        {
            InitializeComponent();
        }

        private void GenerateNoticeOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
