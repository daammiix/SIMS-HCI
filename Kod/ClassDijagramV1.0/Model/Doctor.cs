// File:    Doctor.cs
// Author:  lipov
// Created: Tuesday, April 5, 2022 4:17:07 PM
// Purpose: Definition of Class Doctor

using ClassDijagramV1._0.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Model
{
    public class Doctor : Person
    {
        #region Properties

        public DoctorType Type;

        [JsonIgnore]
        public List<Surgery> Surgery { get; set; }

        #endregion

        #region Constructor

        [JsonConstructor]
        public Doctor()
        {

        }

        public Doctor(string name, string surname, string jmbg, string gender, string phoneNumber, string email,
            DateTime dateOfBirth, Address adr, DoctorType type, List<Surgery> surgery = null) : base(name, surname, jmbg,
                gender, phoneNumber, email, dateOfBirth, adr)
        {
            if (surgery == null)
            {
                Surgery = new List<Surgery>();
            }
            else
            {
                Surgery = surgery;
            }
            Type = type;
        }

        #endregion

    }
}