using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.Views.ManagerView;
using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel
{
    public class CalendarViewModel : ObservableObject
    {
        ManagerAppointmentController managerAppointmentController;
        public ObservableCollection<ManagerAppointment> ManagerAppointments {
            get;
            set;
        }

        private readonly Random _random = new Random();

        Window window;

        readonly private String format = "dd/MM/yyyyTHH:mm";
        readonly private String fullFormat = "dd/MM/yyyy HH:mm";

        public String Name { get; set; }
        public String FromDate { get; set; }
        public String FromTime { get; set; }
        public String ToDate { get; set; }
        public String ToTime { get; set; }

        private RelayCommand _addManagerAppointment;
        private RelayCommand _changeManagerAppointment;
        private RelayCommand _saveAddedManagerAppointment;
        private RelayCommand _saveChandegManagerAppointment;
        private RelayCommand _cancelManagerAppointment;

        public CalendarViewModel(Window? window)
        {
            var app = Application.Current as App;

            this.window = window;

            FromDate = DateTime.Now.ToString("dd/MM/yyyy");
            FromTime = DateTime.Now.ToString("HH:mm");
            ToDate = DateTime.Now.ToString("dd/MM/yyyy");
            ToTime = DateTime.Now.ToString("HH:mm");

            managerAppointmentController = app.managerAppointmentController;
            ManagerAppointments = new ObservableCollection<ManagerAppointment>();
            var apts = managerAppointmentController.GetAllManagerAppointments();
            foreach (var apt in apts)
            {
                ManagerAppointments.Add(apt);
            }

        }

        public RelayCommand AddManagerAppointment
        {
            get
            {

                _addManagerAppointment = new RelayCommand(o =>
                {
                    AddManagerAppointment addManagerAppointment = new AddManagerAppointment();
                    addManagerAppointment.Show();
                });

                return _addManagerAppointment;
            }
        }

        public RelayCommand ChangeManagerAppointment
        {
            get
            {
                _changeManagerAppointment = new RelayCommand(o =>
                {
                    ChangeManagerAppointment changeManagerAppointment = new ChangeManagerAppointment();
                    changeManagerAppointment.Show();
                });

                return _changeManagerAppointment;
            }
        }

        public RelayCommand SaveAddedManagerAppointment
        {
            get
            {
                _saveAddedManagerAppointment = new RelayCommand(o =>
                {
                    AddManagerAppointmentAction();
                });

                return _saveAddedManagerAppointment;
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
                    if(window != null)
                    {
                        window.Close();
                    }
                });

                return _cancelManagerAppointment;
            }
        }

        public void AddManagerAppointmentAction()
        {
            DateTime fromDatetime = DateTime.ParseExact(FromDate + "T" + FromTime, format, null);
            DateTime toDatetime = DateTime.ParseExact(ToDate + "T" + ToTime, format, null);

            var appointmentId = RandomId();
            var newManagerAppointment = new ManagerAppointment(appointmentId, Name, fromDatetime, toDatetime);
            managerAppointmentController.AddManagerAppointment(newManagerAppointment);

            window.Close();
        }

        public void ChangeManagerAppointmentAction()
        {
            DateTime fromDatetime = DateTime.ParseExact(FromDate + "T" + FromTime, format, null);
            DateTime toDatetime = DateTime.ParseExact(ToDate + "T" + ToTime, format, null);

            var appointmentId = RandomId();
            var newManagerAppointment = new ManagerAppointment(appointmentId, Name, fromDatetime, toDatetime);
            managerAppointmentController.ChangeManagerAppointment(newManagerAppointment);

            window.Close();
        }

        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        public string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length = 26  

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
