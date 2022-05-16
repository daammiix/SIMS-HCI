using ClassDijagramV1._0.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Model
{
    public class DoctorRating : ObservableObject
    {
        public static int idCounter = 0;

        private int id;
        private List<int> grades;
        private double averageGrade;
        private string comment;
        private int doctorId;

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                if (value != id)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        public int DoctorId
        {
            get
            {
                return doctorId;
            }
            set
            {
                if (value != doctorId)
                {
                    doctorId = value;
                    OnPropertyChanged("DoctorId");
                }
            }
        }
        public List<int> Grades
        {
            get
            {
                return grades;
            }
            set
            {
                if (value != grades)
                {
                    grades = value;
                    OnPropertyChanged("Grades");
                }
            }
        }
        public double AverageGrade
        {
            get
            {
                return averageGrade;
            }
            set
            {
                if (value != averageGrade)
                {
                    averageGrade = value;
                    OnPropertyChanged("AverageGrade");
                }
            }
        }
        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                if (value != comment)
                {
                    comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }

        public DoctorRating(int doctorID,List<int> grades, double averageGrade, string comment)
        {
            Id = ++idCounter;
            doctorId = doctorID;
            Grades = grades;
            AverageGrade = averageGrade;
            Comment = comment;
        }

        public DoctorRating()
        {
        }
    }
}
