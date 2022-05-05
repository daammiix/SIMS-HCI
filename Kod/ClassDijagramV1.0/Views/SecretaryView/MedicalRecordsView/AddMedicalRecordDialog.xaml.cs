using ClassDijagramV1._0.Model.Enums;
using ClassDijagramV1._0.ViewModel.SecretaryViewModels.MedicalRecordsViewModels;
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
