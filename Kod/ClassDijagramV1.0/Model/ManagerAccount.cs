using ClassDijagramV1._0.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Model
{
    public class ManagerAccount : ObservableObject
    {
        private String _name;
        private String _surname;
        private String _birthday;
        private String _email;
        private String _phone;

        public ManagerAccount(String Name, String Surname, String Birthday, String Email, String Phone)
        {
            this.Name = Name;
            this.Surname = Surname;
            this.Birthday = Birthday;
            this.Email = Email;
            this.Phone = Phone;
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
    }
}
