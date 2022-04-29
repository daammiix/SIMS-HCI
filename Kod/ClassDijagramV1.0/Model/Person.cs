using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using System;
using System.Text.Json.Serialization;

namespace Model
{
    public abstract class Person
    {
        // brojac id-ja
        private static int _id = 0;
        public int Id { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public String Jmbg { get; set; }
        public String Gender { get; set; }
        public String PhoneNumber { get; set; }
        public String Email { get; set; }
        public DateTime DateOfBirth { get; set; }

        public Person(string name, string surname, string jmbg, string gender,
            string phoneNumber, string email, DateTime dateOfBirth)
        {
            this.Id = ++_id;
            this.Name = name;
            this.Surname = surname;
            this.Jmbg = jmbg;
            this.Gender = gender;
            this.PhoneNumber = phoneNumber;
            this.Email = email;
            this.DateOfBirth = dateOfBirth;
        }

        [JsonConstructor]
        public Person()
        {

        }
    }
}