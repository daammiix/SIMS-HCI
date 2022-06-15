using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.ViewModel.SecretaryViewModels;
using System.Windows;
using System.Windows.Input;

namespace ClassDijagramV1._0.Views.SecretaryView
{
    /// <summary>
    /// Interaction logic for SecretaryWindow.xaml
    /// </summary>
    public partial class SecretaryMainWindow : Window
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="acc">Acc sekretara</param>
        public SecretaryMainWindow()
        {
            InitializeComponent();
        }

        #region Application State Event Handlers

        /// <summary>
        /// Minimizes application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Maximizes application or change it back to normal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WindowStateButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        /// <summary>
        /// Closes applications main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this?.Close();
        }


        private void Header_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        #endregion
    }
}
