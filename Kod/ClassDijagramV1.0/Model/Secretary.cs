/***********************************************************************
 * Module:  Secretary.cs
 * Author:  lipov
 * Purpose: Definition of the Class Model.Secretary
 ***********************************************************************/

using System;

namespace Model
{
    public class Secretary : RegisteredUser
    {
        public Secretary(string name, string surname, string jmbg, string gender, string phoneNumber, string email, DateTime dateOfBirth, string username, string password) : base(name, surname, jmbg, gender, phoneNumber, email, dateOfBirth, username, password)
        {
        }
    }
}