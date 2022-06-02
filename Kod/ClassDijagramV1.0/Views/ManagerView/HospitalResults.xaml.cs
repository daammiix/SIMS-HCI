using ClassDijagramV1._0.ViewModel;
using System.Windows;

namespace ClassDijagramV1._0.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for HospitalResults.xaml
    /// </summary>
    public partial class HospitalResults : Window
    {
        private PollsViewModel _pollsViewModel;
        public HospitalResults()
        {
            InitializeComponent();

            _pollsViewModel = new PollsViewModel(this);
            this.DataContext = _pollsViewModel;
        }
    }
}
