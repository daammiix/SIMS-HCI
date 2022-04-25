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
        private String medicineName;
        private DateTime startTaking;
        private DateTime stopTaking;
        private int interval;

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
                    onPropertyChanged("MedicineName");
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
                    onPropertyChanged("StartTaking");
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
                    onPropertyChanged("StopTaking");
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
                    onPropertyChanged("Interval");
                }
            }
        }

        public MedicineDrug(string medicineName, DateTime startTaking, DateTime stopTaking, int interval)
        {
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
