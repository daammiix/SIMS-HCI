using ClassDijagramV1._0.Model;
using System;

namespace Model
{
    public class Secretary : Person
    {
        public Secretary(string name, string surname, string jmbg, string gender, string phoneNumber, string email,
            DateTime dateOfBirth, Address adr) : base(name, surname, jmbg, gender, phoneNumber, email, dateOfBirth, adr)
        {

        }

        // Zbog fileHandlera, mora da bude parameterless constructor
        public Secretary()
        {

        }
    }
}