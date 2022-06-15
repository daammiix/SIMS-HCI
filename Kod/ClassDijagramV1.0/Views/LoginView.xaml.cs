using ClassDijagramV1._0.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace ClassDijagramV1._0.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
            this.DataContext = new LoginViewModel();
        }

        /// <summary>
        /// Updatuje sifru kad god se promeni
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel loginViewModel)
            {
                loginViewModel.Password = PasswordBox.Password;
            }
        }
    }
}
