using ClassDijagramV1._0.Util;
using System;
using System.Collections.Generic;

namespace ClassDijagramV1._0.Model
{
    public class MedicalReport : ObservableObject
    {
        public static int idCounter = 0;
        private int id;
        private string description;
        private List<String> medicine;
        private string note;

        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        public List<String> Medicine
        {
            get { return medicine; }
            set
            {
                if (medicine != value)
                {
                    medicine = value;
                    OnPropertyChanged("Therapy");
                }
            }
        }

        public String Note
        {
            get
            {
                return note;
            }
            set
            {
                note = value;
                OnPropertyChanged("Note");
            }
        }

        public MedicalReport(string description, List<String> medicine, string note = null)
        {
            Id = ++idCounter;
            Description = description;
            Medicine = medicine;
            Note = note;
        }
    }
}
