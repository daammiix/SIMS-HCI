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

        #endregion

        #region Properties

        public int Id { get; set; }
        public String Jmbg { get; set; }
        public Gender Gender { get; set; }
        public String PhoneNumber { get; set; }
        public String Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Address Address { get; set; }


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

        #endregion

        #region Constructor

        public Person(string _name, string _surname, string jmbg, Gender gender,
            string phoneNumber, string email, DateTime dateOfBirth, Address adr)
        {
            this.Id = ++idCounter;
            this.Name = _name;
            this.Surname = _surname;
            this.Jmbg = jmbg;
            this.Gender = gender;
            this.PhoneNumber = phoneNumber;
            this.Email = email;
            this.DateOfBirth = dateOfBirth;
            this.Address = adr;
        }

        [JsonConstructor]
        public Person()
        {

        }

        #endregion
    }
}