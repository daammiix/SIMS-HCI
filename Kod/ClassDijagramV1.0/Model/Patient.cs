/***********************************************************************
 * Module:  Patient.cs
 * Author:  lipov
 * Purpose: Definition of the Class Model.Patient
 ***********************************************************************/

using System;
using System.Collections.Generic;

namespace Model
{
    public class Patient : Person
    {
        public List<Appointment> appointment;

        private String socialSecurityNumber;
        private DateTime dateOfBan;

        public Patient(string name, string surname, string jmbg, string gender, string phoneNumber, string email, DateTime dateOfBirth,
            List<Appointment> appointment, string socialSecurityNumber, DateTime dateOfBan)
            : base(name, surname, jmbg, gender, phoneNumber, email, dateOfBirth)
        {
            this.appointment = appointment;
            this.socialSecurityNumber = socialSecurityNumber;
            this.dateOfBan = dateOfBan;
        }
    }
}