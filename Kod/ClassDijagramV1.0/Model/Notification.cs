using ClassDijagramV1._0.Util;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Model
{
    internal class Notification : ObservableObject
    {
        private String id;
        private String title;
        private String content;
        private String patientID;
        private Boolean isRead;
        private DateTime created;
        public NotificationType notificationType { get; set; }

        public String NotificationID
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
                    onPropertyChanged("NotificationID");
                }
            }
        }
        public String Title
        {
            get
            {
                return title;
            }
            set
            {
                if (value != title)
                {
                    title = value;
                    onPropertyChanged("Title");
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
                    onPropertyChanged("Content");
                }
            }
        }
        public String PatientID
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
                    //onPropertyChanged("PatientID");
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
                    onPropertyChanged("IsRead");
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
                    onPropertyChanged("Created");
                }
            }
        }
    }
}
