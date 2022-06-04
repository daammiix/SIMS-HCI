using ClassDijagramV1._0.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Model
{
    public class MedicineDrug : ObservableObject
    {
        public static int idCounter = 0;
        private int id;
        private String medicineName;
        private DateTime startTaking;
        private DateTime stopTaking;
        private int interval;

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
        public String MedicineName
        {
            get
            {
                return medicineName;
            }
            set
            {
                if (value != medicineName)
                {
                    medicineName = value;
                    OnPropertyChanged("MedicineName");
                }
            }
        }
        public DateTime StartTaking
        {
            get
            {
                return startTaking;
            }
            set
            {
                if (value != startTaking)
                {
                    startTaking = value;
                    OnPropertyChanged("StartTaking");
                }
            }
        }
        public DateTime StopTaking
        {
            get
            {
                return stopTaking;
            }
            set
            {
                if (value != stopTaking)
                {
                    stopTaking = value;
                    OnPropertyChanged("StopTaking");
                }
            }
        }
        public int Interval
        {
            get
            {
                return interval;
            }
            set
            {
                if (value != interval)
                {
                    interval = value;
                    OnPropertyChanged("Interval");
                }
            }
        }

        public MedicineDrug(string medicineName, DateTime startTaking, DateTime stopTaking, int interval)
        {
            Id = ++idCounter;
            MedicineName = medicineName;
            StartTaking = startTaking;
            StopTaking = stopTaking;
            Interval = interval;
        }

        public MedicineDrug()
        {
        }
    }
}
