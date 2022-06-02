using ClassDijagramV1._0.Model.Enums;
using ClassDijagramV1._0.ViewModel.SecretaryViewModels.MedicalRecordsViewModels;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ClassDijagramV1._0.Views.SecretaryView.MedicalRecordsView
{
    /// <summary>
    /// Interaction logic for AddMedicalRecordDialog.xaml
    /// </summary>
    public partial class AddMedicalRecordDialog : Window
    {
        #region Fields

        private AddMedicalRecordDialogViewModel _viewModel;

        #endregion

        #region Constructor

        public AddMedicalRecordDialog(ObservableCollection<MedicalRecordViewModel> medicalRecordViewModels)
        {

            _viewModel = new AddMedicalRecordDialogViewModel(medicalRecordViewModels);

            this.DataContext = _viewModel;

            InitializeComponent();

        }

        #endregion

        #region Events Handler

        private void RadioButtonGender_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Content.Equals("Male"))
                {
                    _viewModel.Gender = Gender.Male;
                }
                else
                {
                    _viewModel.Gender = Gender.Female;
                }
            }
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
