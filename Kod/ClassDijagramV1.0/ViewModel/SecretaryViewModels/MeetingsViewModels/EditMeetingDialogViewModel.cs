using ClassDijagramV1._0.FileHandlers;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.ViewModel.SecretaryViewModels.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.MeetingsViewModels
{
    public class EditMeetingDialogViewModel
    {
        #region Fields

        // Meeting koji editujemo
        private MeetingViewModel _meetingViewModel;

        #endregion

        #region Properties

        public ObservableCollection<MeetingAccountViewModel> AccountsInMeeting { get; private set; } = new ObservableCollection<MeetingAccountViewModel>();

        #endregion

        #region Constructor

        public EditMeetingDialogViewModel(MeetingViewModel meetingViewModel)
        {
            _meetingViewModel = meetingViewModel;

            LoadData();
        }

        #endregion

        #region Private Helpers

        private void LoadData()
        {

            _meetingViewModel.AccountsInMeeting.ForEach(meetingAccountVM =>
            {
                AccountsInMeeting.Add(meetingAccountVM);
            });

        }

        #endregion
    }
}
