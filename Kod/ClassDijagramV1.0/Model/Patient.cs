using ClassDijagramV1._0.Model;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Model
{
    public class Patient : Person
    {

        #region Properties

        // Liste ne cuvamo
        [JsonIgnore]
        public List<Appointment> Appointments { get; set; }

        public String SocialSecurityNumber { get; set; }

        public int? MedicalRecordNumber { get; set; }

        #endregion

        public Patient(string name, string surname, string jmbg, string gender, string phoneNumber, string email, DateTime dateOfBirth,
                Address adr, string socialSecurityNumber, List<Appointment>? appointments = null)
            : base(name, surname, jmbg, gender, phoneNumber, email, dateOfBirth, adr)
        {
            // Ako je appointment null napravimo praznu listu appointmenta
            this.Appointments = appointments ?? new List<Appointment>();
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