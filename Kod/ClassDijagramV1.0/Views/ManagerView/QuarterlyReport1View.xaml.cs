using ClassDijagramV1._0.ViewModel;
using System.Windows;

namespace ClassDijagramV1._0.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for QuarterlyReport1View.xaml
    /// </summary>
    public partial class QuarterlyReport1View : Window
    {
        private QuarterlyReportsViewModel _quarterlyReportsViewModel;
        public QuarterlyReport1View()
        {
            InitializeComponent();

            _quarterlyReportsViewModel = new QuarterlyReportsViewModel(this);
            this.DataContext = _quarterlyReportsViewModel;
        }
    }
}
