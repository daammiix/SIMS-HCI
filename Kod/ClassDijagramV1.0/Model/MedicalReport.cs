using ClassDijagramV1._0.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Model
{
    public class MedicalReport : ObservableObject
    {
        public static int idCounter = 0;
        private int id;
        private string description;
        private List<MedicineDrug> medicine;
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

        public List<MedicineDrug> Medicine
        {
            get { return medicine; }
            set
            {
                if (medicine != value)
                {
                    medicine = value;
                    OnPropertyChanged("Medicine");
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

        public MedicalReport(string description, List<MedicineDrug> medicine, string note = null)
        {
            Id = ++idCounter;
            Description = description;
            Medicine = medicine;
            Note = note;
        }
    }
}
