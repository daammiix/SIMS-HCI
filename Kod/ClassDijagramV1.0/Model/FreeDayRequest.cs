using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Model
{
    public class FreeDayRequest
    {
        #region Properties

        public int DoctorId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Description { get; set; }

        #endregion

        #region Constructor

        public FreeDayRequest(int doctorId, DateTime from, DateTime to, string description)
        {
            DoctorId = doctorId;
            From = from;
            To = to;
            Description = description;
        }

        public FreeDayRequest()
        {

        }

        #endregion
    }
}
