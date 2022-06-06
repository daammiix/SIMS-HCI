using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Model
{
    /// <summary>
    /// Za cuvanje lista kljuceva ljudi koji us obavezni i opcini za sastanak kako bi se prilikom ucitavanja
    /// mogle popuniti liste
    /// </summary>
    public class MeetingDependencies
    {
        #region Properties

        public int MeetingId { get; set; }
        public List<string> RequiredAccountsUsernames { get; set; }
        public List<string> OptionalAccountsUsernames { get; set; }

        #endregion

        #region Constructor

        public MeetingDependencies(int meetingId, List<string> requiredAccountUsernames, List<string> optionalAccountUsernames)
        {
            MeetingId = meetingId;
            RequiredAccountsUsernames = requiredAccountUsernames;
            OptionalAccountsUsernames = optionalAccountUsernames;
        }

        public MeetingDependencies()
        {

        }

        #endregion
    }
}
