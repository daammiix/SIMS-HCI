using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.ViewModel
{
    public class SecretaryHomeViewModel
    {
        #region Properties

        public Patient? Patient { get; set; }

        #endregion


        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sc">Sekretar koji se prijavio</param>
        public SecretaryHomeViewModel(Patient pt)
        {
            Patient = pt;
        }

        #endregion
    }
}
