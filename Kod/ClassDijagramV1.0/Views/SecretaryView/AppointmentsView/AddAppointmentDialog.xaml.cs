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
                switch (rb.Content.ToString())
                {
                    case "general checkup":
                        {
                            _viewModel.SelectedAppointmentType = AppointmentType.generalPractitionerCheckup;
                            break;
                        }
                    case "specialist checkup":
                        {
                            _viewModel.SelectedAppointmentType = AppointmentType.specialistCheckup;
                            break;
                        }
                    case "operation":
                        {
                            _viewModel.SelectedAppointmentType = AppointmentType.operation;
                            break;
                        }
                }
            }
        }

        #endregion

    }
}
