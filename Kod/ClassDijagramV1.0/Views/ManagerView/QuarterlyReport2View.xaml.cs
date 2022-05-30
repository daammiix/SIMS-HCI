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
using System.Windows.Shapes;

namespace ClassDijagramV1._0.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for QuarterlyReport2View.xaml
    /// </summary>
    public partial class QuarterlyReport2View : Window
    {
        private QuarterlyReportsViewModel _quarterlyReportsViewModel;
        public QuarterlyReport2View()
        {
            InitializeComponent();

            _quarterlyReportsViewModel = new QuarterlyReportsViewModel(this);
            this.DataContext = _quarterlyReportsViewModel;
        }
    }
}
