/***********************************************************************
 * Module:  RegisteredUser.cs
 * Author:  lipov
 * Purpose: Definition of the Class Model.RegisteredUser
 ***********************************************************************/

using System;

namespace Model
{
    public class RegisteredUser : Person
    {


        public String username { get; set; }
        public String password { get; set; }

        public RegisteredUser(string name, string surname, string jmbg, string gender, string phoneNumber, string email, DateTime dateOfBirth,
            string username, string password)
            : base(name, surname, jmbg, gender, phoneNumber, email, dateOfBirth)
        {
            this.username = username;
            this.password = password;
        }
    }
}
