using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.Views.ManagerView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel
{
    public class AddManagerAppointmentViewModel
    {
        ManagerAppointmentController managerAppointmentController;
        public BindingList<ManagerAppointment> ManagerAppointments
        {
            get;
            set;
        }

        private readonly Random _random = new Random();

        readonly private String format = "dd/MM/yyyyTHH:mm";

        AddManagerAppointment addManagerAppointment;

        public String Name { get; set; }
        public String FromDate { get; set; }
        public String FromTime { get; set; }
        public String ToDate { get; set; }
        public String ToTime { get; set; }

        private RelayCommand _saveAddedManagerAppointment;
        private RelayCommand _cancelManagerAppointment;

        public AddManagerAppointmentViewModel(AddManagerAppointment addManagerAppointment)
        {
            var app = Application.Current as App;

            FromDate = DateTime.Now.ToString("dd/MM/yyyy");
            FromTime = DateTime.Now.ToString("HH:mm");
            ToDate = DateTime.Now.ToString("dd/MM/yyyy");
            ToTime = DateTime.Now.ToString("HH:mm");

            this.addManagerAppointment = addManagerAppointment;

            managerAppointmentController = app.managerAppointmentController;
            ManagerAppointments = managerAppointmentController.GetAllManagerAppointments();
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

        public RelayCommand CancelManagerAppointment
        {
            get
            {
                _cancelManagerAppointment = new RelayCommand(o =>
                {
                    addManagerAppointment.Close();
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
