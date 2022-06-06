using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.Views.ManagerView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.ViewModel
{
    public class ChangeManagerAccountViewModel : ObservableObject
    {
        private String _name;
        private String _surname;
        private String _birthday;
        private String _email;
        private String _phone;

        public ManagerAccount manager;

        private RelayCommand _saveChangedAccount;
        private RelayCommand _cancelChangedAccount;

        ChangedAccountView changedAccountView;
        AccountViewModel accountViewModel;

        public ChangeManagerAccountViewModel(ChangedAccountView changedAccountView, AccountViewModel accountViewModel, ManagerAccount manager)
        {
            this.changedAccountView = changedAccountView;
            this.accountViewModel = accountViewModel;
            this.manager = manager;

            this.Name = manager.Name;
            this.Surname = manager.Surname;
            this.Birthday = manager.Birthday;
            this.Email = manager.Email;
            this.Phone = manager.Phone;
        }

        public RelayCommand SaveChangedAccount
        {
            get
            {
                _saveChangedAccount = new RelayCommand(o =>
                {
                    SaveManagerAccountAction();
                });

                return _saveChangedAccount;
            }
        }

        public RelayCommand CancelChangedAccount
        {
            get
            {
                _cancelChangedAccount = new RelayCommand(o =>
                {
                    changedAccountView.Close();
                });

                return _cancelChangedAccount;
            }
        }

        public String Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name == value)
                    return;
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public String Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                if (_surname == value)
                    return;
                _surname = value;
                OnPropertyChanged("Surname");
            }
        }

        public String Birthday
        {
            get
            {
                return _birthday;
            }
            set
            {
                if (_birthday == value)
                    return;
                _birthday = value;
                OnPropertyChanged("Birthday");
            }
        }

        public String Email
        {
            get
            {
                return _email;
            }
            set
            {
                if (_email == value)
                    return;
                _email = value;
                OnPropertyChanged("Email");
            }
        }

        public String Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                if (_phone == value)
                    return;
                _phone = value;
                OnPropertyChanged("Phone");
            }
        }

        public void SaveManagerAccountAction()
        {
            var account = new ManagerAccount(Name, Surname, Birthday, Email, Phone);
            accountViewModel.ChangedAccount(account);
            changedAccountView.Close();
        }
    }
}
