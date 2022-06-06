using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Model.Enums;
using ClassDijagramV1._0.Util;
using System;
using System.Text.Json.Serialization;

namespace Model
{
    public abstract class Person : ObservableObject
    {
        // brojac id-ja
        public static int idCounter = 0;

        #region Fields

        private String _name;
        private String _surname;
        private String _jmbg;
        private Gender _gender;
        private String _phonenumber;
        private String _email;
        private DateTime _dateOfBirth;
        private Address _address;

        #endregion

        #region Properties

        public int Id { get; set; }
        /*public String Jmbg { get; set; }
        public Gender Gender { get; set; }
        public String PhoneNumber { get; set; }
        public String Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Address Address { get; set; }*/


        public String Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
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
                if (value != _surname)
                {
                    _surname = value;
                    OnPropertyChanged("Surname");
                }
            }
        }

        public String Jmbg
        {
            get
            {
                return _jmbg;
            }
            set
            {
                if (value != _jmbg)
                {
                    _jmbg = value;
                    OnPropertyChanged("Jmbg");
                }
            }
        }
        public Gender Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                if (value != _gender)
                {
                    _gender = value;
                    OnPropertyChanged("Gender");
                }
            }
        }
        public String PhoneNumber
        {
            get
            {
                return _phonenumber;
            }
            set
            {
                if (value != _phonenumber)
                {
                    _phonenumber = value;
                    OnPropertyChanged("PhoneNumber");
                }
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
                if (value != _email)
                {
                    _email = value;
                    OnPropertyChanged("Email");
                }
            }
        }

        public DateTime DateOfBirth
        {
            get
            {
                return _dateOfBirth;
            }
            set
            {
                if (value != _dateOfBirth)
                {
                    _dateOfBirth = value;
                    OnPropertyChanged("DateOfBirth");
                }
            }
        }
        public Address Address
        {
            get
            {
                return _address;
            }
            set
            {
                if (value != _address)
                {
                    _address = value;
                    OnPropertyChanged("Address");
                }
            }
        }

        #endregion

        #region Constructor

        public Person(string _name, string _surname, string _jmbg, Gender _gender,
            string _phoneNumber, string _email, DateTime _dateOfBirth, Address _address)
        {
            Id = ++idCounter;
            Name = _name;
            Surname = _surname;
            Jmbg = _jmbg;
            Gender = _gender;
            PhoneNumber = _phoneNumber;
            Email = _email;
            DateOfBirth = _dateOfBirth;
            Address = _address;
        }

        [JsonConstructor]
        public Person()
        {

        }

        #endregion
    }
}