using ClassDijagramV1._0.ViewModel.SecretaryViewModels.AppointmentsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.AppointmentsViewModels
{
    public class AppointmentsMainViewModel
    {
        #region Properties

        public ScheduleViewModel ScheduleViewModel { get; set; }

        #endregion

        #region Constructor

        public AppointmentsMainViewModel()
        {
            ScheduleViewModel = new ScheduleViewModel();
        }

        #endregion

    }
}
