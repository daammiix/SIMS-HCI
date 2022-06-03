using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.ViewModel.SecretaryViewModels.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.MeetingsViewModels
{
    public class MeetingAccountViewModel : ObservableObject
    {
        #region Fields

        private bool _isRequired;

        #endregion

        #region Properties

        public AccountViewModel AccountViewModel { get; set; }

        public bool Required
        {
            get { return _isRequired; }
            set
            {
                if (_isRequired != value)
                {
                    _isRequired = value;
                    OnPropertyChanged("isRequired");
                }
            }
        }

        #endregion

        #region Constructor

        public MeetingAccountViewModel(AccountViewModel accountViewModel, bool required)
        {
            AccountViewModel = accountViewModel;
            _isRequired = required;
        }

        #endregion

    }
}
