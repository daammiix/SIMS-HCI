using ClassDijagramV1._0.ViewModel.SecretaryViewModels.MeetingsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClassDijagramV1._0.Views.SecretaryView.MeetingsView
{
    /// <summary>
    /// Interaction logic for MeetingsMainView.xaml
    /// </summary>
    public partial class MeetingsMainView : UserControl
    {
        #region Fields

        private MeetingsMainViewModel _viewModel;

        #endregion

        #region Constructor

        public MeetingsMainView()
        {
            InitializeComponent();

            _viewModel = this.DataContext as MeetingsMainViewModel;

            Schedule.DaysViewSettings.TimeInterval = new System.TimeSpan(0, 30, 0);
        }

        #endregion

        #region Event Handler

        private void Schedule_AppointmentEditorOpening(object sender, Syncfusion.UI.Xaml.Scheduler.AppointmentEditorOpeningEventArgs e)
        {
            e.Cancel = true;
            if (e.Appointment != null)
            {
                // Editovanje
                // Posto editovanje nije trazeno kao funkcionalnost iskorisceno je za prikaz ljudi koji su u sastanku
                // eventualno dodavanje novih i izbacivanje ljudi iz sastanka
                MeetingViewModel meeting = e.Appointment.Data as MeetingViewModel;
                EditMeetingDialog editMeetingDialog = new EditMeetingDialog(meeting);
                editMeetingDialog.Owner = Application.Current.MainWindow;
                editMeetingDialog.Show();
            }
            else
            {
                // Dodavanje
                DateTime selectedDate = e.DateTime;

                // Ako je selektovani dan pre danasnjeg ne otvorimo dialog
                if (selectedDate.Ticks > DateTime.Now.Subtract(TimeSpan.FromDays(1)).Ticks)
                {
                    AddMeetingDialog addMeetingDialog = new AddMeetingDialog();
                    addMeetingDialog.DataContext = new AddMeetingViewModel(_viewModel.ScheduleViewModel.Meetings, selectedDate);
                    addMeetingDialog.Owner = Application.Current.MainWindow;
                    addMeetingDialog.ShowDialog();
                }
            }
        }

        #endregion
    }
}
