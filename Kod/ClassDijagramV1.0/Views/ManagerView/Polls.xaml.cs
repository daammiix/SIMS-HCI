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
