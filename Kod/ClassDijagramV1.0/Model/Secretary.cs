using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Model.Enums;
using System;

namespace Model
{
    public class Secretary : Person
    {
        public Secretary(string name, string surname, string jmbg, Gender gender, string phoneNumber, string email,
            DateTime dateOfBirth, Address adr) : base(name, surname, jmbg, gender, phoneNumber, email, dateOfBirth, adr)
        {

        }

        // Zbog fileHandlera, mora da bude parameterless constructor
        public Secretary()
        {

        }
    }
}