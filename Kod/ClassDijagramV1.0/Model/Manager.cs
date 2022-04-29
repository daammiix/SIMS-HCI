/***********************************************************************
 * Module:  Manager.cs
 * Author:  lipov
 * Purpose: Definition of the Class Model.Manager
 ***********************************************************************/

using System;

namespace Model
{
    public class Manager : Person
    {
        public Manager(string name, string surname, string jmbg, string gender, string phoneNumber, string email, DateTime dateOfBirth) : base(name, surname, jmbg, gender, phoneNumber, email, dateOfBirth)
        {
        }

        public Manager()
        {

        }
    }
}