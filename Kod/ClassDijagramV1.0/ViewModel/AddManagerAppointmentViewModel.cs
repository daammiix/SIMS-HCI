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
    public class AddManagerAppointmentViewModel : ObservableObject
    {
        ManagerAppointmentController managerAppointmentController;
        public BindingList<ManagerAppointment> ManagerAppointments
        {
            get;
            set;
        }

        private readonly Random _random = new Random();

        private DateTime selectedDate;

        readonly private String format = "dd/MM/yyyyTHH:mm";

        AddManagerAppointment addManagerAppointment;

        private IRefreshableManagerAppointmentView managerAppointmentView;

        public String ErrorMessage { get; set; }

        private String Name;
        private String FromDate;
        private String FromTime;
        private String ToDate;
        private String ToTime;

        private RelayCommand _saveAddedManagerAppointment;
        private RelayCommand _cancelManagerAppointment;

        public AddManagerAppointmentViewModel(AddManagerAppointment addManagerAppointment, IRefreshableManagerAppointmentView managerAppointmentView, DateTime selectedDate)
        {
            var app = Application.Current as App;

            this.addManagerAppointment = addManagerAppointment;
            this.managerAppointmentView = managerAppointmentView;
            this.selectedDate = selectedDate;

            managerAppointmentController = app.managerAppointmentController;
            ManagerAppointments = managerAppointmentController.GetAllManagerAppointments();

            resetFields();
        }

        private void resetFields()
        {
            this.Name = null;

            FromDate = selectedDate.Date.ToString("dd/MM/yyyy");
            FromTime = DateTime.Now.ToString("HH:mm");
            ToDate = selectedDate.Date.ToString("dd/MM/yyyy");
            ToTime = DateTime.Now.ToString("HH:mm");
        }

        public String selectedName
        {
            get { return Name; }
            set
            {
                if (Name == value) { return; }
                Name = value;
                if (value.Length < 1)
                {
                    ErrorMessage = "Polje naziv ne sme da bude prazno";
                    OnPropertyChanged("ErrorMessage");
                }
                else
                {
                    ErrorMessage = "";
                    OnPropertyChanged("ErrorMessage");
                }
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
                DateTime date;
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
                DateTime time;
                bool format = DateTime.TryParseExact(value, "HH:mm:TT", System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out time);
                if (!format)
                {
                    ErrorMessage = "Uneti format je pogrešan";
                    OnPropertyChanged("ErrorMessage");
                }
                else
                {
                    ErrorMessage = "";
                    OnPropertyChanged("ErrorMessage");
                }
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
                DateTime time;
                bool format = DateTime.TryParseExact(value, "HH:mm", System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out time);
                if (!format)
                {
                    ErrorMessage = "Uneti format je pogrešan";
                    OnPropertyChanged("ErrorMessage");
                }
                else
                {
                    ErrorMessage = "";
                    OnPropertyChanged("ErrorMessage");
                }
            }
        }

        public RelayCommand SaveAddedManagerAppointment
        {
            get
            {
                _saveAddedManagerAppointment = new RelayCommand(o =>
                {
                    if(selectedName == null || selectedFromTime == "" || selectedToTime == "" || selectedName == "")
                    {
                        ErrorMessage = "Polje naziv ne sme da bude prazno";
                        OnPropertyChanged("ErrorMessage");
                        return;
                    }
                    AddManagerAppointmentAction();
                });

                return _saveAddedManagerAppointment;
            }
        }

        public RelayCommand CancelManagerAppointment
        {
            get
            {
                _cancelManagerAppointment = new RelayCommand(o =>
                {
                    resetFields();
                    addManagerAppointment.Close();
                });

                return _cancelManagerAppointment;
            }
        }

        public void AddManagerAppointmentAction()
        {
            DateTime fromDatetime = DateTime.ParseExact(FromDate + "T" + FromTime, "dd/MM/yyyyTHH:mm", null);
            DateTime toDatetime = DateTime.ParseExact(ToDate + "T" + ToTime, this.format, null);

            var appointmentId = RandomId();
            var newManagerAppointment = new ManagerAppointment(appointmentId, Name, fromDatetime, toDatetime);
            managerAppointmentController.AddManagerAppointment(newManagerAppointment);

            managerAppointmentView.RefreshManagerAppointment();
            resetFields();
            addManagerAppointment.Close();
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
