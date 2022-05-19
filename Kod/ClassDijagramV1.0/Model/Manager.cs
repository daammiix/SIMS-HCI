/***********************************************************************
 * Module:  Manager.cs
 * Author:  lipov
 * Purpose: Definition of the Class Model.Manager
 ***********************************************************************/

using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Model.Enums;
using System;

namespace Model
{
    public class Manager : Person
    {
        public Manager(string name, string surname, string jmbg, Gender gender, string phoneNumber, string email,
            DateTime dateOfBirth, Address adr) : base(name, surname, jmbg, gender, phoneNumber, email, dateOfBirth, adr)
        {

        }

        public Manager()
        {

        }
    }
}