using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Model.Enums;
using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.ViewModel.SecretaryViewModels.AccountViewModels;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.MeetingsViewModels
{
    public class MeetingViewModel : ObservableObject
    {
        #region Fields

        private Meeting _meeting;
        private Brush _color;

        private List<AccountViewModel> _accounts;
        private List<MeetingAccountViewModel> _accountsInMeeting;

        private AccountController _accountController;

        #endregion

        #region Properties

        public int MeetingId
        {
            get
            {
                return _meeting.Id;
            }
        }

        public DateTime From
        {
            get { return _meeting.From; }
            set
            {
                if (_meeting.From != value)
                {
                    _meeting.From = value;
                    OnPropertyChanged("From");
                }
            }
        }

        public DateTime To
        {
            get
            {
                return _meeting.From + _meeting.Duration;
            }
            set
            {
                if (_meeting.From + _meeting.Duration != value)
                {
                    _meeting.Duration = value - _meeting.From;
                    OnPropertyChanged("To");
                }
            }
        }

        public string MeetingName
        {
            get
            {
                return "Sastanak";
            }
        }

        public Brush Color
        {
            get
            {
                return _color;
            }
        }

        public TimeSpan Duration
        {
            get
            {
                return _meeting.Duration;
            }
        }

        public List<AccountViewModel> Accounts
        {
            get
            {
                return _accounts;
            }
            set
            {
                _accounts = value;
            }
        }

        public List<MeetingAccountViewModel> AccountsInMeeting
        {
            get
            {
                return _accountsInMeeting;
            }
            set
            {
                _accountsInMeeting = value;

                // Osvezimo required i optinal akaunte
                RefreshRequiredAndOptionalAccounts();

            }
        }

        public string RoomId
        {
            get
            {
                return _meeting.RoomId;
            }
        }

        public List<MeetingAccount> RequiredAccounts
        {
            get
            {
                return _meeting.RequiredAccounts;
            }
        }

        public List<MeetingAccount> OptionalAccounts
        {
            get
            {
                return _meeting.OptionalAccounts;
            }
        }

        #endregion

        #region Constructor

        public MeetingViewModel(Meeting meeting)
        {
            App app = Application.Current as App;

            _accountController = app.AccountController;

            _meeting = meeting;

            LoadAccountsInMeeting();
            LoadAccounts();

            // Raspberry
            _color = new SolidColorBrush(System.Windows.Media.Color.FromRgb(227, 11, 92));
        }

        public MeetingViewModel()
        {

        }

        #endregion

        #region Private Helpers 

        /// <summary>
        /// Ucita sve akaunte koji su u meetingu
        /// </summary>
        private void LoadAccountsInMeeting()
        {
            _accountsInMeeting = new List<MeetingAccountViewModel>();
            _meeting.OptionalAccounts.ForEach(meetingAccount =>
            {
                _accountsInMeeting.Add(new MeetingAccountViewModel(new AccountViewModel(meetingAccount.Account), false));
            });

            _meeting.RequiredAccounts.ForEach(meetingAccount =>
            {
                _accountsInMeeting.Add(new MeetingAccountViewModel(new AccountViewModel(meetingAccount.Account), true));
            });
        }

        /// <summary>
        /// Ucita sve akaunte koji nisu u meetingu
        /// </summary>
        private void LoadAccounts()
        {
            _accounts = new List<AccountViewModel>();

            _accountController.GetAccounts().ToList().ForEach(account =>
            {
                if (account.Role != Role.Patient)
                {
                    _accounts.Add(new AccountViewModel(account));
                }
            });

            _accountsInMeeting.ForEach(accountMVM =>
            {
                foreach (AccountViewModel accountVM in _accounts)
                {
                    AccountViewModel accToRemove = null;
                    if (accountVM.Username.Equals(accountMVM.AccountViewModel.Username))
                    {
                        accToRemove = accountVM;
                    }

                    if (accToRemove != null)
                    {
                        _accounts.Remove(accToRemove);
                        break;
                    }
                }
            });
        }

        /// <summary>
        /// Osvezava required i optinal akaunte
        /// </summary>
        private void RefreshRequiredAndOptionalAccounts()
        {
            RequiredAccounts.Clear();
            OptionalAccounts.Clear();
            _accountsInMeeting.ForEach(meetingAccountVW =>
            {
                if (meetingAccountVW.Required == true)
                {
                    RequiredAccounts.Add(new MeetingAccount(meetingAccountVW.AccountViewModel.Account, true));
                }
                else
                {
                    OptionalAccounts.Add(new MeetingAccount(meetingAccountVW.AccountViewModel.Account, false));
                }
            });
        }

        #endregion
    }
}
