using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.FileHandlers;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Model.Enums;
using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.ViewModel.SecretaryViewModels.AccountViewModels;
using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.MeetingsViewModels
{
    public class AddMeetingViewModel : ObservableObject
    {
        #region Fields



        private ObservableCollection<MeetingViewModel> _meetings;

        private FileHandler<Meeting> _fileHandler;

        // Kontroleri

        private RoomController _roomController;

        private AccountController _accountController;


        // Liste za vreme

        private List<int> _hours;

        private List<int> _minutes;

        private ObservableCollection<MeetingAccountViewModel> _accountsInMeeting;

        // Komande

        private RelayCommand _addMeeting;

        #endregion

        #region Properties

        // Liste za vreme
        public List<int> Hours
        {
            get
            {
                if (_hours == null)
                {
                    _hours = new List<int>();
                    for (int i = 0; i < 24; i++)
                    {
                        _hours.Add(i);
                    }
                }
                return _hours;
            }
            private set { _hours = value; }
        }

        public List<int> Minutes
        {
            get
            {
                if (_minutes == null)
                {
                    _minutes = new List<int>();
                    for (int i = 0; i < 60; i++)
                    {
                        _minutes.Add(i);
                    }
                }
                return _minutes;
            }
            private set { _minutes = value; }
        }

        public DateTime SelectedDate { get; set; }
        public int? SelectedHour { set; get; }
        public int? SelectedMinute { set; get; }
        public List<Room> Rooms { get; set; }
        public Room? SelectedRoom { get; set; }
        public int? Duration { get; set; }
        public ObservableCollection<AccountViewModel> Accounts { get; private set; }
        public ObservableCollection<MeetingAccountViewModel> AccountsInMeeting
        {
            get
            {
                if (_accountsInMeeting == null)
                {
                    _accountsInMeeting = new ObservableCollection<MeetingAccountViewModel>();
                }
                return _accountsInMeeting;
            }
            set
            {
                _accountsInMeeting = value;
            }
        }


        #endregion

        #region Commands

        public RelayCommand AddMeeting
        {
            get
            {
                if (_addMeeting == null)
                {
                    _addMeeting = new RelayCommand(o => { AddMeetingExecute(o as Window); }, AddMeetingCanExecute);
                }

                return _addMeeting;
            }
        }

        #endregion

        #region Constructor

        public AddMeetingViewModel(ObservableCollection<MeetingViewModel> meetings, DateTime selectedDateTime)
        {
            _meetings = meetings;
            SelectedDate = selectedDateTime;

            _fileHandler = new FileHandler<Meeting>(MeetingsMainViewModel.path);

            // Instanciramo liste
            Accounts = new ObservableCollection<AccountViewModel>();
            _accountsInMeeting = new ObservableCollection<MeetingAccountViewModel>();

            // Ucitamo potrebne kontrolere
            App app = Application.Current as App;

            _accountController = app.AccountController;
            _roomController = app.roomController;

            // Ucitamo sve akaunte
            LoadAccountViewModels();

            // Ucitamo sve sobe za sastanke
            LoadMeetingsRooms();
        }

        #endregion

        #region Private Helpres

        /// <summary>
        /// Ucitava sve sobe namenjene za sastanke
        /// </summary>
        private void LoadMeetingsRooms()
        {

            Rooms = _roomController.GetAllRooms().ToList<Room>();

            // Izbacimo sobe koje nisu sobe za sastanke
            Rooms = Rooms.FindAll((room) =>
            {
                return room.RoomName.Equals(RoomName.meetingRoom);
            });
        }

        /// <summary>
        /// Ucitava sve accove i pravi od njih viewModele
        /// </summary>
        private void LoadAccountViewModels()
        {
            List<Account> accounts = _accountController.GetAccounts().ToList<Account>();

            accounts.ForEach((account) =>
            {
                if (account.Role != Role.Patient)
                {
                    Accounts.Add(new AccountViewModel(account));
                }

            });
        }

        /// <summary>
        /// Dodaje meeting i zatvara dialog
        /// </summary>
        /// <param name="dialog"></param>
        private void AddMeetingExecute(Window dialog)
        {
            TimeSpan duration = SelectedDate.AddMinutes(Duration.Value) - SelectedDate;
            List<MeetingAccount> requiredPeopleAccounts = GetRequiredPeopleAccounts();
            List<MeetingAccount> optionalPeopleAccounts = GetOptionalPeopleAccounts();

            Meeting newMeeting = new Meeting(SelectedDate, duration, SelectedRoom.RoomID, requiredPeopleAccounts, optionalPeopleAccounts);
            MeetingViewModel newMeetingVM = new MeetingViewModel(newMeeting);

            // Dodamo novi meeting, njegove dependencije sacuvamo i dodamo njegov ViewModel
            SaveMeetingDependencies(newMeeting);
            _fileHandler.AppendItem(newMeeting);
            _meetings.Add(newMeetingVM);

            // Posaljemo notifikacije
            SendNotifications(BuildNotifications(newMeeting));

            dialog.Close();
        }

        /// <summary>
        /// Upisuje u fajl meeting dependencies
        /// </summary>
        /// <param name="meeting"></param>
        private void SaveMeetingDependencies(Meeting meeting)
        {
            FileHandler<MeetingDependencies> meetingDependenciesFileHandler
                = new FileHandler<MeetingDependencies>(MeetingsMainViewModel.meetingsDependenciesPath);

            List<string> requiredPeopleUsernames = meeting.RequiredAccounts
                .Select(meetingAccount => meetingAccount.Account.Username).ToList();

            List<string> optionalPeopleUsernames = meeting.OptionalAccounts
                .Select(meetingAccount => meetingAccount.Account.Username).ToList();

            MeetingDependencies meetingDependencies = new MeetingDependencies(
                meeting.Id, requiredPeopleUsernames, optionalPeopleUsernames);

            meetingDependenciesFileHandler.AppendItem(meetingDependencies);
        }

        /// <summary>
        /// Proverava da li moze da se izvrsi komanda
        /// </summary>
        /// <returns></returns>
        private bool AddMeetingCanExecute()
        {
            if (AccountsInMeeting.Any() && SelectedRoom != null && Duration != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Vraca sve akaunte koji su obavezni za sastanak
        /// </summary>
        /// <returns></returns>
        private List<MeetingAccount> GetRequiredPeopleAccounts()
        {
            List<MeetingAccountViewModel> requiredMeetingAccounts = AccountsInMeeting
                .Where(account => account.Required == true)
                .ToList();

            List<AccountViewModel> requiredAccounts = requiredMeetingAccounts
                .Select<MeetingAccountViewModel, AccountViewModel>(meetingAccount => meetingAccount.AccountViewModel)
                .ToList();

            return requiredAccounts.Select<AccountViewModel, MeetingAccount>(accountVW =>
             new MeetingAccount(accountVW.Account, true)).ToList();
        }

        /// <summary>
        /// Vraca sve akaunte koji su opcioni za sastanak
        /// </summary>
        /// <returns></returns>
        private List<MeetingAccount> GetOptionalPeopleAccounts()
        {
            List<MeetingAccountViewModel> optionalMeetingAccounts = AccountsInMeeting
                .Where(account => account.Required == false)
                .ToList();

            List<AccountViewModel> optionalAccounts = optionalMeetingAccounts
                .Select<MeetingAccountViewModel, AccountViewModel>(meetingAccount => meetingAccount.AccountViewModel)
                .ToList();

            return optionalAccounts.Select<AccountViewModel, MeetingAccount>(accountVW =>
                    new MeetingAccount(accountVW.Account, false)).ToList();
        }

        /// <summary>
        /// Salje notifikacije, odnosno upisuje ih u fajl
        /// </summary>
        /// <param name="meetingNotifications"></param>
        private void SendNotifications(List<MeetingNotification> newMeetingNotifications)
        {
            FileHandler<MeetingNotification> _fileHandler = new FileHandler<MeetingNotification>(MeetingsMainViewModel.meetingNotificationsPath);
            List<MeetingNotification> meetingNotifications = _fileHandler.GetItems();

            meetingNotifications.AddRange(newMeetingNotifications);

            _fileHandler.SaveItems(meetingNotifications);
        }

        /// <summary>
        /// Vraca listu napravljenih notifikacija za sastanak
        /// </summary>
        /// <param name="meeting"></param>
        private List<MeetingNotification> BuildNotifications(Meeting meeting)
        {
            List<MeetingNotification> notificationsToSend = new List<MeetingNotification>();

            AccountsInMeeting.ToList().ForEach(meetingAccount =>
            {
                string content = BuildNotificationContent(meetingAccount, meeting);
                notificationsToSend.Add(new MeetingNotification
                    (meetingAccount.AccountViewModel.Username, content, meeting.Id, meetingAccount.Required));
            });

            return notificationsToSend;
        }

        private string BuildNotificationContent(MeetingAccountViewModel meetingAccount, Meeting meeting)
        {
            StringBuilder content = new StringBuilder();
            content.AppendLine("Imate zakazan sastanak");
            content.AppendLine($"Dan: {meeting.From.ToShortDateString()}");
            content.AppendLine($"Od: {meeting.From.ToShortTimeString()}");
            content.AppendLine($"Do: {(meeting.From + meeting.Duration).ToShortTimeString()}");
            content.AppendLine($"Dolazak obavezan: {(meetingAccount.Required ? "Da" : "Ne")}");

            Console.WriteLine(content.ToString());
            return content.ToString();
        }

        #endregion

    }
}
