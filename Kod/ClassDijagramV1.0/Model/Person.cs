/***********************************************************************
 * Module:  Person.cs
 * Author:  lipov
 * Purpose: Definition of the Class Model.Person
 ***********************************************************************/

using System;

namespace Model
{
   public class Person
   {
        public String name { get; set; }
        public String surname { get; set; }
        public String jmbg { get; set; }
        public String gender { get; set; }
        public String phoneNumber { get; set; }
        public String email { get; set; }
        public DateTime dateOfBirth { get; set; }

        public Person(string name, string surname, string jmbg, string gender, string phoneNumber, string email, DateTime dateOfBirth)
        {
            this.name = name;
            this.surname = surname;
            this.jmbg = jmbg;
            this.gender = gender;
            this.phoneNumber = phoneNumber;
            this.email = email;
            this.dateOfBirth = dateOfBirth;
        }
    }
}