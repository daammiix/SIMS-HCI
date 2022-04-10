using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Model.Accounts
{
    public class RegularAccount : Account
    {
        #region Properties
        public string Password { get; set; }

        public Person Person { get; set; }

        #endregion

        #region Constructor
        public RegularAccount(string username, string password, Person p) : base(username)
        {
            Password = password;
            Person = p;
        }

        #endregion
    }
}
