using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.StatisticalDataViewModels
{
    /// <summary>
    /// Za chart
    /// </summary>
    public class DoctorWithAverageGrade
    {
        #region Properties

        public string FullName { get; set; }

        public double AverageGrade { get; set; }

        #endregion

        #region Constructor

        public DoctorWithAverageGrade(string fullName, double averageGrade)
        {
            FullName = fullName;
            AverageGrade = averageGrade;
        }

        #endregion
    }
}
