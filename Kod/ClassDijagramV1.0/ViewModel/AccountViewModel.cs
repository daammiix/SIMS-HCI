using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.Views.ManagerView;
using System;

namespace ClassDijagramV1._0.ViewModel
{
    public class AccountViewModel : ObservableObject
    {
        private String _name;
        private String _surname;
        private String _birthday;
        private String _email;
        private String _phone;

        public ManagerAccount manager;

        private RelayCommand _openChangeAccount;

        MainViewModel mainViewModel;

        public AccountViewModel()
        {
            this.Name = "Sofija";
            this.Surname = "Stojanović";
            this.Birthday = "12.07.1968.";
            this.Email = "stojanovic.sofija@gmail.com";
            this.Phone = "064/685-2290";

            manager = new ManagerAccount("Sofija", "Stojanović", "12.07.1968.", "stojanovic.sofija@gmail.com", "064/685-2290");

            this.mainViewModel = mainViewModel;
        }
        public RelayCommand OpenChangeAccount
        {
            get
            {
                _openChangeAccount = new RelayCommand(o =>
                {
                    ChangedAccountAction();
                });

                return _openChangeAccount;
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

        private void ChangedAccountAction()
        {
            ChangedAccountView changedAccountView = new ChangedAccountView(this, manager);
            changedAccountView.Show();
        }

        public void ChangedAccount(ManagerAccount managerAccount)
        {
            this.Name = managerAccount.Name;
            this.Surname = managerAccount.Surname;
            this.Birthday = managerAccount.Birthday;
            this.Email = managerAccount.Email;
            this.Phone = managerAccount.Phone;
        }
    }
}
