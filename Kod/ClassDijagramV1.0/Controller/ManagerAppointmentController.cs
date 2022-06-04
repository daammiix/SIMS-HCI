using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Controller
{
    public class ManagerAppointmentController
    {
        private ManagerAppointmentService managerAppointmentService;

        public ManagerAppointmentController(ManagerAppointmentService managerAppointmentService)
        {
            this.managerAppointmentService = managerAppointmentService;
        }

        public void AddManagerAppointment(ManagerAppointment managerAppointment)
        {
            managerAppointmentService.AddManagerAppointment(managerAppointment);
        }

        public void ChangeManagerAppointment(ManagerAppointment managerAppointment)
        {
            managerAppointmentService.ChangeManagerAppointment(managerAppointment);
        }

        public ManagerAppointment? GetManagerAppointment(String managerAppointmentID)
        {
            return managerAppointmentService.GetManagerAppointment(managerAppointmentID);
        }

        public BindingList<ManagerAppointment> GetAllManagerAppointments()
        {
            return managerAppointmentService.GetAllManagerAppointments();
        }
    }
}
