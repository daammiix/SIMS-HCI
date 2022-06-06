using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Model
{
    public class MeetingNotification
    {
        #region Properties

        // Username akaunta za koji je vezana notifikacija
        public string Username { get; set; }
        public string Content { get; set; }
        public int MeetingId { get; set; }

        // Da li je obavezna osoba iza acc-a
        public bool IsRequired { get; set; }

        #endregion

        #region Constructor

        public MeetingNotification(string username, string content, int meetingId, bool isRequired)
        {
            Username = username;
            Content = content;
            MeetingId = meetingId;
            IsRequired = isRequired;
        }

        [JsonConstructor]
        public MeetingNotification()
        {

        }

        #endregion
    }
}
