using ClassDijagramV1._0.Model;
using System;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public Account? Account { get; set; }

        // Kljuc za account
        public string AccountUsername { get; set; }

        public Person(string name, string surname, string jmbg, string gender,
            string phoneNumber, string email, DateTime dateOfBirth, Account account = null)
        {
            this.Name = name;
            this.Surname = surname;
            this.Jmbg = jmbg;
            this.Gender = gender;
            this.PhoneNumber = phoneNumber;
            this.Email = email;
            this.DateOfBirth = dateOfBirth;
            this.Account = account;
            if (account != null)
                this.AccountUsername = Account.Username;
        }

        [JsonConstructor]
        public Person()
        {

        }
    }
}