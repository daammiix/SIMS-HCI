using ClassDijagramV1._0.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.MeetingsViewModels
{
    public class IsRequiredDialogViewModel : ObservableObject
    {
        #region Fields

        private MeetingAccountViewModel _meetingAccountViewModel;

        private bool _isRequired;

        // Commands
        private RelayCommand _okCommand;

        #endregion

        #region Properties

        public bool IsRequired
        {
            get
            {
                return _isRequired;
            }
            set
            {
                if (_isRequired != value)
                {
                    _isRequired = value;
                    OnPropertyChanged("IsRequired");
                }
            }
        }

        public RelayCommand OkCommand
        {
            get
            {
                if (_okCommand == null)
                {
                    _okCommand = new RelayCommand(o => { Finish(o as Window); });
                }

                return _okCommand;
            }
        }

        #endregion

        #region Constructor

        public IsRequiredDialogViewModel(MeetingAccountViewModel meetingAccountViewModel)
        {
            _meetingAccountViewModel = meetingAccountViewModel;
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Updatuje required accounta i gasi dialog
        /// </summary>
        /// <param name="dialog"></param>
        private void Finish(Window dialog)
        {
            _meetingAccountViewModel.Required = IsRequired;

            dialog?.Close();
        }

        #endregion

    }
}
