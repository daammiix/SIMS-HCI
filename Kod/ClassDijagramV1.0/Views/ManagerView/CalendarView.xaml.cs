using System.Windows.Controls;
using Syncfusion.UI.Xaml.Scheduler;
using System.Windows.Input;
using ClassDijagramV1._0.ViewModel;
using System;
using ClassDijagramV1._0.Controller;
using System.Collections.ObjectModel;
using ClassDijagramV1._0.Model;
using System.Windows;
using System.ComponentModel;

namespace ClassDijagramV1._0.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for CalendarView.xaml
    /// </summary>
    public partial class CalendarView : UserControl
    {
        private CalendarViewModel calendarViewModel;
        DateTime selectedDate;
        ManagerAppointmentController managerAppointmentController;
        public BindingList<ManagerAppointment> ManagerAppointments
        {
            get;
            set;
        }
        public CalendarView()
        {
            InitializeComponent();
            calendarViewModel = new CalendarViewModel();
            this.DataContext = calendarViewModel;
            var app = Application.Current as App;

            managerAppointmentController = app.managerAppointmentController;
            ManagerAppointments = managerAppointmentController.GetAllManagerAppointments();
        }

        public void Schedule_CellTapped(object sender, CellTappedEventArgs e)
        {
            this.selectedDate = e.DateTime;
            calendarViewModel.ManagerAppointmentsOfDay.Clear();
            if (selectedDate.Day >= DateTime.Now.Day)
            {
                foreach (var managerAppointment in ManagerAppointments)
                {
                    if (managerAppointment.Start.Day == selectedDate.Day)
                    {
                        calendarViewModel.ManagerAppointmentsOfDay.Add(managerAppointment);

                        calendarViewModel.selectedDate = e.DateTime;
                    }
                    if (managerAppointment.Start.Day != selectedDate.Day)
                    {
                        calendarViewModel.selectedDate = e.DateTime;
                    }
                }
            }
        }

        private void Schedule_AppointmentEditorOpening(object sender, AppointmentEditorOpeningEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
