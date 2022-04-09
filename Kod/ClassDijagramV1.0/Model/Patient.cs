/***********************************************************************
 * Module:  Patient.cs
 * Author:  lipov
 * Purpose: Definition of the Class Model.Patient
 ***********************************************************************/

using System;
using System.Collections.Generic;

namespace Model
{
   public class Patient : RegisteredUser
   {
      public List<Appointment> appointment;
   
      private String socialSecurityNumber;
      private Boolean banned = false;
      private DateTime dateOfBan;

        public Patient(string name, string surname, string jmbg, string gender, string phoneNumber, string email, DateTime dateOfBirth, string username, string password,
            List<Appointment> appointment, string socialSecurityNumber, bool banned, DateTime dateOfBan)
            : base(name, surname, jmbg, gender, phoneNumber, email, dateOfBirth, username, password)
        {
            this.appointment = appointment;
            this.socialSecurityNumber = socialSecurityNumber;
            this.banned = banned;
            this.dateOfBan = dateOfBan;
        }
    }
}