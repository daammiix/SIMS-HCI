using ClassDijagramV1._0.ViewModel;
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

namespace ClassDijagramV1._0.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for QuarterlyReports.xaml
    /// </summary>
    public partial class QuarterlyReports : UserControl
    {
        private QuarterlyReportsViewModel _quarterlyReportsViewModel;
        public QuarterlyReports()
        {
            InitializeComponent();

            Window window = new Window();
            _quarterlyReportsViewModel = new QuarterlyReportsViewModel(window = null);
            this.DataContext = _quarterlyReportsViewModel;
        }
    }
}
