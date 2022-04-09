/***********************************************************************
 * Module:  Manager.cs
 * Author:  lipov
 * Purpose: Definition of the Class Model.Manager
 ***********************************************************************/

using System;

namespace Model
{
    public class Manager : RegisteredUser
    {
        public Manager(string name, string surname, string jmbg, string gender, string phoneNumber, string email, DateTime dateOfBirth, string username, string password) : base(name, surname, jmbg, gender, phoneNumber, email, dateOfBirth, username, password)
        {
        }
    }
}