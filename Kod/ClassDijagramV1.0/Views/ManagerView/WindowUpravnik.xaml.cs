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

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            _mainViewModel.ChangeToAccountView();
            kalendar.IsChecked = false;
            izvestaji.IsChecked = false;
            radnoOsoblje.IsChecked = false;
            sale.IsChecked = false;
            magacin.IsChecked = false;
        }

        private void ComboBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {
            WarningExit warningExit = new WarningExit(this);
            warningExit.Show();
        }

        private void ComboBoxItem_Selected_2(object sender, RoutedEventArgs e)
        {
            TutorialWindow tutorialWindow = new TutorialWindow();
            tutorialWindow.Show();
        }
    }
}
