using ClassDijagramV1._0.Util;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Model
{
    public class Notification : ObservableObject
    {
        public static int idCounter = 0;
        private int id;
        private String content;
        private int patientID;
        private Boolean isRead;
        private DateTime created;
        private NotificationType notificationType;

        public int NotificationID
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
                    OnPropertyChanged("NotificationID");
                }
            }
        }
        public String Content
        {
            get
            {
                return content;
            }
            set
            {
                if (value != content)
                {
                    content = value;
                    OnPropertyChanged("Content");
                }
            }
        }
        public int PatientID
        {
            get
            {
                return patientID;
            }
            set
            {
                if (value != patientID)
                {
                    patientID = value;
                    OnPropertyChanged("PatientID");
                }
            }
        }
        public Boolean IsRead
        {
            get
            {
                return isRead;
            }
            set
            {
                if (value != isRead)
                {
                    isRead = value;
                    OnPropertyChanged("IsRead");
                }
            }
        }
        public DateTime Created
        {
            get
            {
                return created;
            }
            set
            {
                if (value != created)
                {
                    created = value;
                    OnPropertyChanged("Created");
                }
            }
        }
        public NotificationType NotificationType
        {
            get
            {
                return notificationType;
            }
            set
            {
                if (value != notificationType)
                {
                    notificationType = value;
                    OnPropertyChanged("NotificationType");
                }
            }
        }

        public Notification(string content, int patientID, bool isRead, DateTime created, NotificationType notificationType)
        {
            
            NotificationID = ++idCounter;
            Content = content;
            PatientID = patientID;
            IsRead = isRead;
            Created = created;
            NotificationType = notificationType;
        }

        public Notification()
        {
        }
    }
}
