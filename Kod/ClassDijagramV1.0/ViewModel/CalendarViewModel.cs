using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Helpers;
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
    public class CalendarViewModel : ObservableObject, IRefreshableManagerAppointmentView
    {
        ManagerAppointmentController managerAppointmentController;
        public BindingList<ManagerAppointment> ManagerAppointments {
            get;
            set;
        }

        public ManagerAppointment selectedManagerAppointment { get; set; }

        private BindingList<ManagerAppointment> _managerAppointmentsOfDay;
        public DateTime selectedDate { get; set; }

        private RelayCommand _addManagerAppointment;
        private RelayCommand _changeManagerAppointment;

        public CalendarViewModel()
        {
            var app = Application.Current as App;

            this.selectedDate = selectedDate;
            ManagerAppointmentsOfDay = new BindingList<ManagerAppointment>();

            managerAppointmentController = app.managerAppointmentController;
            ManagerAppointments = managerAppointmentController.GetAllManagerAppointments();

        }
        public BindingList<ManagerAppointment> ManagerAppointmentsOfDay
        {
            get
            {
                return _managerAppointmentsOfDay;
            }
            set
            {
                if (_managerAppointmentsOfDay == value)
                {
                    return;
                }
                _managerAppointmentsOfDay = value;
                OnPropertyChanged("ManagerAppointmentsOfDay");
            }
        }

        public RelayCommand AddManagerAppointment
        {
            get
            {

                _addManagerAppointment = new RelayCommand(o =>
                {
                    if(selectedDate.Day >= DateTime.Now.Day)
                    {
                        AddManagerAppointment addManagerAppointment = new AddManagerAppointment(this, selectedDate);
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
                    if (selectedDate.Day >= DateTime.Now.Day && selectedManagerAppointment != null)
                    {
                        ChangeManagerAppointment changeManagerAppointment = new ChangeManagerAppointment(selectedManagerAppointment, this);
                        changeManagerAppointment.Show();
                    }
                });

                return _changeManagerAppointment;
            }
        }

        public void RefreshManagerAppointment()
        {
            ManagerAppointmentsOfDay.Clear();
            foreach (var managerAppointment in managerAppointmentController.GetAllManagerAppointments())
            {
                if (managerAppointment.Start.Day == selectedDate.Day)
                {
                    ManagerAppointmentsOfDay.Add(managerAppointment);
                }
            }
        }
    }
}
