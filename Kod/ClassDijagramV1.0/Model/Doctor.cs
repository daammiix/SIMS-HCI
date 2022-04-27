// File:    Doctor.cs
// Author:  lipov
// Created: Tuesday, April 5, 2022 4:17:07 PM
// Purpose: Definition of Class Doctor

using ClassDijagramV1._0.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Model
{
    public class Doctor : Person, INotifyPropertyChanged
    {
        private DoctorType type;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(String propertyName)
        {
            PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
            PropertyChanged(this, e);
        }

        public List<Surgery> surgery { get; set; }

        public Doctor()
        {
        }

        public Doctor(string name, string surname, string jmbg, string gender, string phoneNumber, string email, DateTime dateOfBirth, DoctorType type, List<Surgery> surgery) : base(name, surname, jmbg, gender, phoneNumber, email, dateOfBirth)
        {
            this.surgery = surgery;
            this.type = type;

        }

    }
}