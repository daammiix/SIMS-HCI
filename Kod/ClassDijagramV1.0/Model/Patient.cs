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

        public int? MedicalRecordNumber { get; set; }

        #endregion

        public Patient(string name, string surname, string jmbg, string gender, string phoneNumber, string email, DateTime dateOfBirth,
                Address adr, string socialSecurityNumber, List<Appointment>? appointment = null)
            : base(name, surname, jmbg, gender, phoneNumber, email, dateOfBirth, adr)
        {
            this.Appointment = appointment;
            this.SocialSecurityNumber = socialSecurityNumber;
            // Kad napravimo zdravstveni karton onda ih vezemo
            this.MedicalRecordNumber = null;
        }

        [JsonConstructor]
        public Patient()
        {

        }

    }
}