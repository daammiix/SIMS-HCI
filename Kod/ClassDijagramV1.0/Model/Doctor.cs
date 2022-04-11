/***********************************************************************
 * Module:  Doctor.cs
 * Author:  lipov
 * Purpose: Definition of the Class Model.Doctor
 ***********************************************************************/

using System;

namespace Model
{
    public class Doctor : Person
    {
        public Doctor(string name, string surname, string jmbg, string gender, string phoneNumber, string email, DateTime dateOfBirth) : base(name, surname, jmbg, gender, phoneNumber, email, dateOfBirth)
        {
        }

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}