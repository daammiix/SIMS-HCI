// File:    Doctor.cs
// Author:  lipov
// Created: Tuesday, April 5, 2022 4:17:07 PM
// Purpose: Definition of Class Doctor

using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Model
{
    public class Doctor : Person
    {
        #region Properties

        [JsonIgnore]
        public List<Appointment> Appointments { get; set; }

        public DoctorType Type { get; set; }


        #endregion

        #region Constructor

        [JsonConstructor]
        public Doctor()
        {

        }

        public Doctor(string name, string surname, string jmbg, Gender gender, string phoneNumber, string email,
            DateTime dateOfBirth, Address adr, DoctorType type, List<Appointment> appointments = null) : base(name, surname, jmbg,
                gender, phoneNumber, email, dateOfBirth, adr)
        {
            if (appointments == null)
            {
                Appointments = new List<Appointment>();
            }
            else
            {
                Appointments = appointments;
            }

            Type = type;
        }

        #endregion

    }
}