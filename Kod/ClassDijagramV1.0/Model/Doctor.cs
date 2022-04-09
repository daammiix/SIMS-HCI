/***********************************************************************
 * Module:  Doctor.cs
 * Author:  lipov
 * Purpose: Definition of the Class Model.Doctor
 ***********************************************************************/

using System;

namespace Model
{
    public class Doctor : RegisteredUser
    {
        public Doctor(string name, string surname, string jmbg, string gender, string phoneNumber, string email, DateTime dateOfBirth, string username, string password) : base(name, surname, jmbg, gender, phoneNumber, email, dateOfBirth, username, password)
        {
        }

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}