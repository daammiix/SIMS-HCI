using ClassDijagramV1._0.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace ClassDijagramV1._0.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for Polls.xaml
    /// </summary>
    public partial class Polls : UserControl
    {
        private PollsViewModel _pollsViewModel;
        public Polls()
        {
            InitializeComponent();

            Window window = new Window();
            _pollsViewModel = new PollsViewModel(window = null);
            this.DataContext = _pollsViewModel;
        }

    }
}
