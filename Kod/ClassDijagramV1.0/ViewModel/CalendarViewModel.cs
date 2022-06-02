using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using System.ComponentModel;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel
{
    public class CalendarViewModel : ObservableObject
    {
        ManagerAppointmentController managerAppointmentController;
        BindingList<ManagerAppointment> _managerAppointments;
        public CalendarViewModel()
        {
            var app = Application.Current as App;

            managerAppointmentController = app.managerAppointmentController;
            _managerAppointments = managerAppointmentController.GetAllManagerAppointments();
        }

        public BindingList<ManagerAppointment> ManagerAppointments
        {
            get
            {
                return _managerAppointments;
            }
            set
            {
                if (_managerAppointments == value)
                {
                    return;
                }
                _managerAppointments = value;
            }
        }
    }
}
