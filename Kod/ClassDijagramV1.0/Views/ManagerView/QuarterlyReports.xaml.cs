using ClassDijagramV1._0.ViewModel;
using System.Windows;
using System.Windows.Controls;

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
