// File:    Doctor.cs
// Author:  lipov
// Created: Tuesday, April 5, 2022 4:17:07 PM
// Purpose: Definition of Class Doctor

using ClassDijagramV1._0.Model;
using System;
using System.Collections.Generic;

namespace Model
{
   public class Doctor : Person
   {
      private DoctorType type;
      
      public List<Surgery> surgery { get; set; }

        public Doctor()
        {
        }

        public Doctor(string name, string surname, string jmbg, string gender, string phoneNumber, string email, DateTime dateOfBirth, DoctorType type, List<Surgery> surgery, Account account = null) : base(name, surname, jmbg, gender, phoneNumber, email, dateOfBirth, account)
        {
            this.surgery = surgery;
            this.type = type;

        }
    }
}