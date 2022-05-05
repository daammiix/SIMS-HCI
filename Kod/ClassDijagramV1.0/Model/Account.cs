using ClassDijagramV1._0.Model.Enums;
using ClassDijagramV1._0.Util;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Model
{
    public class Account : ObservableObject
    {
        #region Fields

        private string _username;
        private string _password;
        private bool _banned;
        private bool _isGuest;
        private int _personId;
        private Role _role;

        #endregion

        #region Properties

        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged("Username");
                }
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        public bool Banned
        {
            get
            {
                return _banned;
            }
            set
            {
                if (_banned != value)
                {
                    _banned = value;
                    OnPropertyChanged("Banned");
                }
            }
        }

        public bool IsGuest
        {
            get
            {
                return _isGuest;
            }
            set
            {
                if (_isGuest != value)
                {
                    _isGuest = value;
                    OnPropertyChanged("IsGuest");
                }
            }
        }

        public int PersonId
        {
            get { return _personId; }
            set
            {
                if (_personId != value)
                {
                    _personId = value;
                    OnPropertyChanged("PersonId");
                }
            }
        }

        public Role Role
        {
            get { return _role; }
            set
            {
                if (_role != value)
                {
                    _role = value;
                    OnPropertyChanged("Role");
                }
            }
        }

        #endregion

        #region Constructor

        public Account(int personId, Role role, string username = "", string password = "", bool isGuest = false, bool banned = false)
        {
            Role = role;
            Username = username;
            Password = password;
            IsGuest = isGuest;
            Banned = banned;
            PersonId = personId;
        }

        [JsonConstructor]
        public Account()
        {

        }

        #endregion
    }

}

