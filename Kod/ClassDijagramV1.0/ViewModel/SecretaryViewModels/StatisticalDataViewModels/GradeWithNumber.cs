using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.StatisticalDataViewModels
{
    /// <summary>
    /// Za chartove
    /// </summary>
    public class GradeWithNumber
    {
        #region Properties

        public int Grade { get; set; }

        public int Number { get; set; }

        #endregion

        #region Constructor

        public GradeWithNumber(int grade, int number)
        {
            Grade = grade;
            Number = number;
        }

        #endregion

    }
}
