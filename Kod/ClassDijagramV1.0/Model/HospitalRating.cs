using ClassDijagramV1._0.Util;
using System.Collections.Generic;

namespace ClassDijagramV1._0.Model
{
    public class HospitalRating : ObservableObject
    {
        public static int idCounter = 0;

        private int id;
        private List<int> grades;
        private double averageGrade;
        private string comment;

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

        public HospitalRating(List<int> grades, double averageGrade, string comment)
        {
            Id = ++idCounter;
            Grades = grades;
            AverageGrade = averageGrade;
            Comment = comment;
        }

        public HospitalRating()
        {
        }
    }
}
