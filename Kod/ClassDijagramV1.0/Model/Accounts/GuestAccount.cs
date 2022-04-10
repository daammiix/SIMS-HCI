using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Model.Accounts
{
    public class GuestAccount : Account
    {
        #region Constructor
        public GuestAccount() : base()
        {
            Username = "Guest";
            Username += new Random().Next(0, 10001);
        }

        #endregion
    }
}
