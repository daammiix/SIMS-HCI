using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.FileHandlers;
using ClassDijagramV1._0.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Timers;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.MeetingsViewModels
{
    public class MeetingsMainViewModel
    {
        #region Static Fields

        public static string path = @"../../../Data/meetings.json";

        public static string meetingsDependenciesPath = @"../../../Data/meetingsDependencies.json";

        public static string meetingNotificationsPath = @"../../../Data/meetingNotifications.json";

        #endregion

        #region Fields

        private FileHandler<Meeting> _fileHandler;

        private AccountController _accountController;

        #endregion

        #region Properties

        public ScheduleViewModel ScheduleViewModel { get; set; }

        #endregion

        #region Constructor

        public MeetingsMainViewModel()
        {
            ScheduleViewModel = new ScheduleViewModel();
            _fileHandler = new FileHandler<Meeting>(path);

            // Kontroleri
            App app = Application.Current as App;
            _accountController = app.AccountController;

            // Ucitamo sve meetinge
            loadMeetings();

            // Pokrenemo tajmer
            StartMeetingsTimer();
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Ucitama sve meetinge iz fajla, pravi od njih viewModele i ucitava u meetinge
        /// </summary>
        private void loadMeetings()
        {
            List<Meeting> meetings = _fileHandler.GetItems();
            meetings?.ForEach(item =>
            {
                // Popunimo meetinge
                LoadMeetingDependecies(item);

                // Dodamo ih
                ScheduleViewModel.Meetings.Add(new MeetingViewModel(item));
            });

            // Stavimo id counter na najveci iz liste ako ima uopste meetinga u listi
            if (meetings.Any())
            {
                int maxId = meetings.Max(meeting => meeting.Id);

                Meeting.idCounter = maxId;
            }
        }

        /// <summary>
        /// Ucitava dependesije meetinga
        /// </summary>
        /// <param name="meeting"></param>
        private void LoadMeetingDependecies(Meeting meeting)
        {
            FileHandler<MeetingDependencies> fileHandler = new FileHandler<MeetingDependencies>(meetingsDependenciesPath);

            List<MeetingDependencies> meetingsDependencies = fileHandler.GetItems();

            MeetingDependencies? meetingDependecies = meetingsDependencies.Find(mDepend => meeting.Id == mDepend.MeetingId);

            if (meetingDependecies != null)
            {
                LoadRequiredAccounts(meetingDependecies, meeting);
                LoadOptionalAccounts(meetingDependecies, meeting);
            }
        }

        /// <summary>
        /// Ucitava obavezne ljude
        /// </summary>
        /// <param name="meetingDependecies"></param>
        private void LoadRequiredAccounts(MeetingDependencies meetingDependecies, Meeting meeting)
        {
            meetingDependecies.RequiredAccountsUsernames.ForEach(username =>
            {
                meeting.RequiredAccounts.Add(new MeetingAccount(_accountController.GetAccount(username), true));
            });
        }

        /// <summary>
        /// Ucitava opcione ljude
        /// </summary>
        /// <param name="meetingDependecies"></param>
        /// <param name="meeting"></param>
        private void LoadOptionalAccounts(MeetingDependencies meetingDependecies, Meeting meeting)
        {
            meetingDependecies.OptionalAccountsUsernames.ForEach(username =>
            {
                meeting.OptionalAccounts.Add(new MeetingAccount(_accountController.GetAccount(username), false));
            });
        }

        private void StartMeetingsTimer()
        {
            Timer timer = new Timer(1000);
            timer.Elapsed += onTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void onTimedEvent(object? sender, ElapsedEventArgs e)
        {
            // Lista meetinga koji su prosli i mogu da se obrisu
            List<Meeting> meetingsToRemove = new List<Meeting>();

            List<Meeting> meetings = _fileHandler.GetItems();

            meetings.ForEach(meeting =>
            {
                if (meeting.From < DateTime.Now)
                {
                    meetingsToRemove.Add(meeting);
                }
            });

            // Ako ima meetinka koji su prosli 
            if (meetingsToRemove.Any())
            {

                // Izbrisemo sve mitinge koji su prosli

                meetingsToRemove.ForEach(meeting =>
                {
                    meetings.Remove(meeting);

                    // Izbrisemo ih i iz view-a
                    ScheduleViewModel.Meetings.ToList().RemoveAll(meetingVM => meetingVM.MeetingId == meeting.Id);
                });

                // Izbrisemo sve notifikacije i dependencije vezane za meetinge koji su prosli

                FileHandler<MeetingDependencies> fileHandlerDependencies = new FileHandler<MeetingDependencies>(meetingsDependenciesPath);
                FileHandler<MeetingNotification> fileHandlerNotifications = new FileHandler<MeetingNotification>(meetingNotificationsPath);

                List<MeetingDependencies> meetingDependencies = fileHandlerDependencies.GetItems();
                List<MeetingNotification> meetingNotifications = fileHandlerNotifications.GetItems();

                meetingsToRemove.ForEach(meeting =>
                {
                    meetingDependencies.RemoveAll(meetingDependencies => meetingDependencies.MeetingId == meeting.Id);
                    meetingNotifications.RemoveAll(meetingNotification => meetingNotification.MeetingId == meeting.Id);
                });

                // Sacuvamo sve u fajlove
                _fileHandler.SaveItems(meetings);
                fileHandlerDependencies.SaveItems(meetingDependencies);
                fileHandlerNotifications.SaveItems(meetingNotifications);
            }
        }

        #endregion
    }
}
