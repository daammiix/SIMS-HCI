using ClassDijagramV1._0.ViewModel.SecretaryViewModels.AppointmentsViewModels;
using Model;
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

namespace ClassDijagramV1._0.Views.SecretaryView.AppointmentsView
{
    /// <summary>
    /// Interaction logic for AddAppointmentDialog.xaml
    /// </summary>
    public partial class AddAppointmentDialog : Window
    {
        #region Fields

        private AddAppointmentDialogViewModel _viewModel;

        #endregion

        #region Constructor

        public AddAppointmentDialog(ObservableCollection<AppointmentViewModel> appointments, DateTime selectedDate)
        {
            _viewModel = new AddAppointmentDialogViewModel(appointments, selectedDate);

            this.DataContext = _viewModel;

            InitializeComponent();
        }

        #endregion

        #region Event Handlers


        private void RadioButtonAppointmentType_Checked(object sender, RoutedEventArgs e)
        {
            // Castujemo object u RadioButton
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                SetAppropriateAppointmentType(rb, _viewModel.SelectedAppointmentType);
            }
        }

        private void RadioButtonAppointmentTypeUrgent_Checked(object sender, RoutedEventArgs e)
        {
            // Castujemo object u RadioButton
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                SetAppropriateAppointmentType(rb, _viewModel.SelectedAppointmentTypeUrgent);
            }
        }

        #endregion

        #region Private Helpres

        /// <summary>
        /// Stavlja odgovarajuci appointmentType u viewModelu na osnovu selektovanog radioButtona
        /// </summary>
        /// <param name="appointmentType">Referenca na odgovarajuci appointmentType u ViewModelu </param>
        private void SetAppropriateAppointmentType(RadioButton rb, AppointmentType appointmentType)
        {
            switch (rb.Content.ToString())
            {
                case "general checkup":
                    {
                        appointmentType = AppointmentType.generalPractitionerCheckup;
                        break;
                    }
                case "specialist checkup":
                    {
                        appointmentType = AppointmentType.specialistCheckup;
                        break;
                    }
                case "operation":
                    {
                        appointmentType = AppointmentType.operation;
                        break;
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
