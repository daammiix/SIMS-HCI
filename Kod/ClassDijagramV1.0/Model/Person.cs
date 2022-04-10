/***********************************************************************
 * Module:  Person.cs
 * Author:  lipov
 * Purpose: Definition of the Class Model.Person
 ***********************************************************************/

using ClassDijagramV1._0.Model.Accounts;
using System;

namespace Model
{
    public class Person
    {
        public String Name { get; set; }
        public String Surname { get; set; }
        public String Jmbg { get; set; }
        public String Gender { get; set; }
        public String PhoneNumber { get; set; }
        public String Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Account? Account { get; set; }

        public Person(string name, string surname, string jmbg, string gender,
            string phoneNumber, string email, DateTime dateOfBirth)
        {
            this.Name = name;
            this.Surname = surname;
            this.Jmbg = jmbg;
            this.Gender = gender;
            this.PhoneNumber = phoneNumber;
            this.Email = email;
            this.DateOfBirth = dateOfBirth;
            Account = null;
        }
    }
}