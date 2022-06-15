using ClassDijagramV1._0.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.MeetingsViewModels
{
    public class ScheduleViewModel
    {
        #region Properties

        public ObservableCollection<MeetingViewModel> Meetings { get; set; }

        #endregion

        #region Constructor

        public ScheduleViewModel()
        {
            Meetings = new ObservableCollection<MeetingViewModel>();
        }

        #endregion
    }
}
