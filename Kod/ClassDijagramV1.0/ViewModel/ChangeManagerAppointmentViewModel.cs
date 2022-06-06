using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Helpers;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.Views.ManagerView;
using System;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel
{
    public class ChangeManagerAppointmentViewModel
    {
        ManagerAppointmentController managerAppointmentController;
        public BindingList<ManagerAppointment> ManagerAppointments
        {
            get;
            set;
        }

        private readonly Random _random = new Random();

        readonly private String format = "dd/MM/yyyyTHH:mm:ss";

        private IRefreshableManagerAppointmentView managerAppointmentView;

        ChangeManagerAppointment changeManagerAppointment;

        private String _name { get; set; }
        private String FromDate { get; set; }
        private String FromTime { get; set; }
        private String ToDate { get; set; }
        private String ToTime { get; set; }

        ManagerAppointment managerAppointment;

        private RelayCommand _saveChandegManagerAppointment;
        private RelayCommand _cancelManagerAppointment;

        public ChangeManagerAppointmentViewModel(ChangeManagerAppointment changeManagerAppointment, ManagerAppointment managerAppointment, IRefreshableManagerAppointmentView managerAppointmentView)
        {
            var app = Application.Current as App;

            FromDate = DateTime.Now.ToString("dd/MM/yyyy");
            FromTime = DateTime.Now.ToString("HH:mm");
            ToDate = DateTime.Now.ToString("dd/MM/yyyy");
            ToTime = DateTime.Now.ToString("HH:mm");

            this.changeManagerAppointment = changeManagerAppointment;
            this.managerAppointment = managerAppointment;

            managerAppointmentController = app.managerAppointmentController;
            ManagerAppointments = managerAppointmentController.GetAllManagerAppointments();

            this.managerAppointmentView = managerAppointmentView;

            this.Name = managerAppointment.Name;
            this.FromDate = managerAppointment.Start.ToShortDateString();
            this.FromTime = managerAppointment.Start.TimeOfDay.ToString();
            this.ToDate = managerAppointment.End.Date.ToShortDateString();
            this.ToTime = managerAppointment.End.TimeOfDay.ToString();
        }

        public String Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name == value)
                    return;
                _name = value;
            }
        }

        public String selectedFromDate
        {
            get
            {
                return FromDate;
            }
            set
            {
                if (FromDate == value)
                    return;
                FromDate = value;
            }
        }

        public String selectedFromTime
        {
            get
            {
                return FromTime;
            }
            set
            {
                if (FromTime == value)
                    return;
                FromTime = value;
            }
        }

        public String selectedToDate
        {
            get
            {
                return ToDate;
            }
            set
            {
                if (ToDate == value)
                    return;
                ToDate = value;
            }
        }

        public String selectedToTime
        {
            get
            {
                return ToTime;
            }
            set
            {
                if (ToTime == value)
                    return;
                ToTime = value;
            }
        }
        public RelayCommand SaveChangedManagerAppointment
        {
            get
            {
                _saveChandegManagerAppointment = new RelayCommand(o =>
                {
                    ChangeManagerAppointmentAction();
                });

                return _saveChandegManagerAppointment;
            }
        }

        public RelayCommand CancelManagerAppointment
        {
            get
            {
                _cancelManagerAppointment = new RelayCommand(o =>
                {
                    changeManagerAppointment.Close();
                });

                return _cancelManagerAppointment;
            }
        }

        public void ChangeManagerAppointmentAction()
        {
            DateTime fromDatetime = DateTime.ParseExact(FromDate + "T" + FromTime, format, null);
            DateTime toDatetime = DateTime.ParseExact(ToDate + "T" + ToTime, format, null);

            var newManagerAppointment = new ManagerAppointment(managerAppointment.ID, Name, fromDatetime, toDatetime);
            managerAppointmentController.ChangeManagerAppointment(newManagerAppointment);

            managerAppointmentView.RefreshManagerAppointment();
            changeManagerAppointment.Close();
        }

        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        public string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26;

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }
            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

        public string RandomId()
        {
            var passwordBuilder = new StringBuilder();
            passwordBuilder.Append(RandomString(4, true));
            passwordBuilder.Append(RandomNumber(10, 99));

            return passwordBuilder.ToString();
        }
    }
}
