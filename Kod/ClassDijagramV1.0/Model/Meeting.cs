using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Model
{
    public class Meeting
    {
        #region Static Fields

        public static int idCounter = 0;

        #endregion

        #region Properties

        public int Id { get; set; }
        public DateTime From { get; set; }
        public TimeSpan Duration { get; set; }
        public string RoomId { get; set; }

        [JsonIgnore]
        public List<MeetingAccount> RequiredAccounts { get; set; }
        [JsonIgnore]
        public List<MeetingAccount> OptionalAccounts { get; set; }


        #endregion

        #region Constructor

        public Meeting(DateTime from, TimeSpan duration, string roomId, List<MeetingAccount> requiredAccounts,
            List<MeetingAccount> optionalAccounts)
        {
            this.Id = ++idCounter;
            this.From = from;
            this.Duration = duration;
            this.RoomId = roomId;
            this.RequiredAccounts = requiredAccounts;
            this.OptionalAccounts = optionalAccounts;
        }

        [JsonConstructor]
        public Meeting()
        {
            RequiredAccounts = new List<MeetingAccount>();
            OptionalAccounts = new List<MeetingAccount>();
        }

        #endregion
    }
}
