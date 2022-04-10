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
                    onPropertyChanged("Username");
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
                    onPropertyChanged("Password");
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
                    onPropertyChanged("Banned");
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
                    onPropertyChanged("IsGuest");
                }
            }
        }

        #endregion

        #region Constructor

        public Account(string username = "", string password = "", bool isGuest = false, bool banned = false)
        {
            Username = username;
            Password = password;
            IsGuest = isGuest;
            Banned = banned;
        }

        [JsonConstructor]
        public Account()
        {

        }

        #endregion
    }

}

