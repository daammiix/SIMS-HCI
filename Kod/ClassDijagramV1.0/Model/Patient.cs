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

        #region Properties

        public List<Appointment>? Appointment { get; set; }

        public String SocialSecurityNumber { get; set; }

        #endregion

        public Patient(string name, string surname, string jmbg, string gender, string phoneNumber, string email, DateTime dateOfBirth,
                string socialSecurityNumber, List<Appointment>? appointment = null)
            : base(name, surname, jmbg, gender, phoneNumber, email, dateOfBirth)
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