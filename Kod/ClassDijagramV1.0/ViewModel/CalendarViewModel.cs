using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.Views.ManagerView;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel
{
    public class CalendarViewModel : ObservableObject
    {
        ManagerAppointmentController managerAppointmentController;
        public BindingList<ManagerAppointment> ManagerAppointments {
            get;
            set;
        }
        public DateTime selectedDate { get; set; } = DateTime.MinValue;

        private RelayCommand _addManagerAppointment;
        private RelayCommand _changeManagerAppointment;

        public CalendarViewModel()
        {
            var app = Application.Current as App;

            this.selectedDate = selectedDate;

            managerAppointmentController = app.managerAppointmentController;
            ManagerAppointments = managerAppointmentController.GetAllManagerAppointments();
        }

        public RelayCommand AddManagerAppointment
        {
            get
            {

                _addManagerAppointment = new RelayCommand(o =>
                {
                    if(selectedDate == DateTime.MinValue)
                    {
                        AddManagerAppointment addManagerAppointment = new AddManagerAppointment();
                        addManagerAppointment.Show();
                    }
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
                    if (selectedDate != DateTime.MinValue)
                    {
                        ChangeManagerAppointment changeManagerAppointment = new ChangeManagerAppointment(selectedDate);
                        changeManagerAppointment.Show();
                    }
                });

                return _changeManagerAppointment;
            }
        }
    }
}
