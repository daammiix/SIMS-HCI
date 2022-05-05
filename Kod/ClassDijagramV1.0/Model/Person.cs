using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using System;
using System.Text.Json.Serialization;

namespace Model
{
    public abstract class Person
    {

        // brojac id-ja
        public static int idCounter = 0;

        #region Properties

        public int Id { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public String Jmbg { get; set; }
        public String Gender { get; set; }
        public String PhoneNumber { get; set; }
        public String Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Address Address { get; set; }

        #endregion

        #region Constructor

        public Person(string name, string surname, string jmbg, string gender,
            string phoneNumber, string email, DateTime dateOfBirth, Address adr)
        {
            this.Id = ++idCounter;
            this.Name = name;
            this.Surname = surname;
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
            // uvecamo idCounter za svakog kog ucitamo
            // pravice problem dok imamo pravljenje dummy podataka kroz kod je ce se id-evi razlikovati prvi put
            // i posle kad pokrecemo zato sto kad imamo podatke u fajlu ovaj counter ce pre nego sto se naprave podaci
            // u kodu da se poveca i onda ce podaci da budu napravljeni sa vecim id-evima
            // idCounter++;
        }

        #endregion
    }
}