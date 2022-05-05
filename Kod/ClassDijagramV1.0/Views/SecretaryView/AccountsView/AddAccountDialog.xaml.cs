using ClassDijagramV1._0.ViewModel.SecretaryViewModels.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ClassDijagramV1._0.Views.SecretaryView.AccountsView
{
    /// <summary>
    /// Interaction logic for AddAccountDialog.xaml
    /// </summary>
    public partial class AddAccountDialog : Window
    {
        AddAccountDialogViewModel _viewModel;

        public AddAccountDialog(ObservableCollection<AccountViewModel> accountViewModels)
        {
            InitializeComponent();

            this.DataContext = new AddAccountDialogViewModel(accountViewModels);

            _viewModel = (AddAccountDialogViewModel)this.DataContext;
        }

        /// <summary>
        /// Stavlja username i password na random string(Guset + 0 <= broj < 10001)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            string username = "Guest" + new Random().Next(10001);
            _viewModel.Username = username;
            _viewModel.Password = username;
        }

        /// <summary>
        /// Brise username i password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            _viewModel.Username = "";
            _viewModel.Password = "";
        }
    }
}
