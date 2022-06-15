using System;

namespace ClassDijagramV1._0.Model
{
    public class ActivityLog
    {
        public DateTime Date { get; set; }
        public int PatientID { get; set; }
        public TypeOfActivity Type { get; set; }

        public ActivityLog()
        {
        }

        public ActivityLog(DateTime date, int patientID, TypeOfActivity type)
        {
            Date = date;
            PatientID = patientID;
            Type = type;
        }
    }
}
