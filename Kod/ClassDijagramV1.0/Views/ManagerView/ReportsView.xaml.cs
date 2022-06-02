using ClassDijagramV1._0.ViewModel;
using System.Windows.Controls;

namespace ClassDijagramV1._0.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class ReportsView : UserControl
    {
        private ReportsViewModel _reportsViewModel;
        public ReportsView()
        {
            InitializeComponent();

            _reportsViewModel = new ReportsViewModel();
            this.DataContext = _reportsViewModel;
        }
    }
}
