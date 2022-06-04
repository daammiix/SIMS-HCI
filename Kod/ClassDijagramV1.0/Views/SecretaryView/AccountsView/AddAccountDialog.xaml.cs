﻿using ClassDijagramV1._0.ViewModel.SecretaryViewModels.AccountViewModels;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace ClassDijagramV1._0.Views.SecretaryView.AccountsView
{
    /// <summary>
    /// Interaction logic for AddAccountDialog.xaml
    /// </summary>
    public partial class AddAccountDialog : Window
    {

        #region Constructor

        public AddAccountDialog(ObservableCollection<AccountViewModel> accountViewModels)
        {
            InitializeComponent();

            this.DataContext = new AddAccountDialogViewModel(accountViewModels);
        }

        #endregion

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
