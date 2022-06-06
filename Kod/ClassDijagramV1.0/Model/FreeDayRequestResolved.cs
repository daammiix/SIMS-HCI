using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Model
{
    public class FreeDayRequestResolved
    {
        #region Properties
        public int DoctorId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public bool Accepted { get; set; }
        public string Description { get; set; }

        #endregion

        #region Constructor

        public FreeDayRequestResolved(int doctorId, DateTime from, DateTime to, bool accepted, string description)
        {
            DoctorId = doctorId;
            From = from;
            To = to;
            Accepted = accepted;
            Description = description;
        }

        [JsonConstructor]
        public FreeDayRequestResolved()
        {

        }

        #endregion
    }
}
