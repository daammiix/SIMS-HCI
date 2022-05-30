using ClassDijagramV1._0.ViewModel;
using System.Windows;

namespace ClassDijagramV1._0.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for WindowUpravnik.xaml
    /// </summary>
    public partial class WindowUpravnik : Window
    {
        private MainViewModel _mainViewModel;
        public WindowUpravnik()
        {
            InitializeComponent();
            _mainViewModel = new MainViewModel();
            this.DataContext = _mainViewModel;
        }
    }
}
