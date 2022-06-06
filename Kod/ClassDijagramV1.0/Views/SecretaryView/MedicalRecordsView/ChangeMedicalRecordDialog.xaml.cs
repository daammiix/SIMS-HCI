using ClassDijagramV1._0.Model.Enums;
using ClassDijagramV1._0.ViewModel.SecretaryViewModels.MedicalRecordsViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ClassDijagramV1._0.Views.SecretaryView.MedicalRecordsView
{
    /// <summary>
    /// Interaction logic for ChangeMedicalRecordDialog.xaml
    /// </summary>
    public partial class ChangeMedicalRecordDialog : Window
    {
        #region Fields

        private ChangeMedicalRecordDialogViewModel _viewModel;

        #endregion

        #region Constructor

        public ChangeMedicalRecordDialog(ChangeMedicalRecordDialogViewModel viewModel)
        {
            _viewModel = viewModel;

            this.DataContext = _viewModel;

            InitializeComponent();
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

        #region EventHandlers

        private void RadioButtonMaritalStatus_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Content != null)
                {
                    switch (rb.Content.ToString())
                    {
                        case "Single":
                            {
                                _viewModel.MaritalStatus = MaritalStatus.Single;
                                break;
                            }
                        case "Married":
                            {
                                _viewModel.MaritalStatus = MaritalStatus.Married;
                                break;
                            }
                        case "Divorced":
                            {
                                _viewModel.MaritalStatus = MaritalStatus.Divorced;
                                break;
                            }
                        case "Widow":
                            {
                                _viewModel.MaritalStatus = MaritalStatus.Widow;
                                break;
                            }
                    }
                }
            }
        }

        private void RadioButtonBloodType_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                switch (rb.Content.ToString())
                {
                    case "0":
                        {
                            _viewModel.BloodType = BloodType.O;
                            break;
                        }
                    case "A":
                        {
                            _viewModel.BloodType = BloodType.A;
                            break;
                        }
                    case "B":
                        {
                            _viewModel.BloodType = BloodType.B;
                            break;
                        }
                    case "AB":
                        {
                            _viewModel.BloodType = BloodType.AB;
                            break;
                        }
                }
            }
        }

        #endregion
    }
}
