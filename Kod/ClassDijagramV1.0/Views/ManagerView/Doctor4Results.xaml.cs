using ClassDijagramV1._0.ViewModel;
using System.Windows;

namespace ClassDijagramV1._0.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for Doctor1Results.xaml
    /// </summary>
    public partial class Doctor4Results : Window
    {
        private PollsViewModel _pollsViewModel;
        public Doctor4Results()
        {
            InitializeComponent();
            _pollsViewModel = new PollsViewModel(this);
            this.DataContext = _pollsViewModel;
        }
    }
}
