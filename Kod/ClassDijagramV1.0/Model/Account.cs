using ClassDijagramV1._0.Model.Enums;
using ClassDijagramV1._0.Util;
using System.Text.Json.Serialization;

namespace ClassDijagramV1._0.Model
{
    public class Account : ObservableObject
    {
        #region Static Fields

        public static int idCounter = 0;

        #endregion

        #region Fields

        private string _username;
        private string _password;
        private bool _banned;
        private bool _isGuest;
        private int _personId;
        private Role _role;

        #endregion

        #region Properties

        public int Id { get; private set; }

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
            Id = ++idCounter;
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

