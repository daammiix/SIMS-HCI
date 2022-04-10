/***********************************************************************
 * Module:  Patient.cs
 * Author:  lipov
 * Purpose: Definition of the Class Model.Patient
 ***********************************************************************/

using ClassDijagramV1._0.Model;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Model
{
    public class Patient : Person
    {
        public List<Appointment>? Appointment { get; set; }

        public String SocialSecurityNumber { get; set; }

        public Patient(string name, string surname, string jmbg, string gender, string phoneNumber, string email, DateTime dateOfBirth,
                string socialSecurityNumber, Account? acc = null, List<Appointment>? appointment = null)
            : base(name, surname, jmbg, gender, phoneNumber, email, dateOfBirth, acc)
        {
            this.Appointment = appointment;
            this.SocialSecurityNumber = socialSecurityNumber;
        }

        [JsonConstructor]
        public Patient()
        {

        }

    }
}