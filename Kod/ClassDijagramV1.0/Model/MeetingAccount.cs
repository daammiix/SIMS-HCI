using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Model
{
    /// <summary>
    /// Dekorato, wrapuje acc i dodaje mu IsRequired
    /// </summary>
    public class MeetingAccount
    {
        #region Properties

        public Account Account { get; set; }

        public bool IsRequired { get; set; }

        #endregion

        #region Constructor

        public MeetingAccount(Account acc, bool isRequired)
        {
            Account = acc;
            IsRequired = isRequired;
        }

        #endregion
    }
}
