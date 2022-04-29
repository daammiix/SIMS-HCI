using System;

namespace Model
{
    public class Secretary : Person
    {
        public Secretary(string name, string surname, string jmbg, string gender, string phoneNumber, string email, DateTime dateOfBirth) : base(name, surname, jmbg, gender, phoneNumber, email, dateOfBirth)
        {
        }
    }
}