using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Service
{
    public class ManagerAppointmentService
    {
        private ManagerAppointmentRepo managerAppointmentRepo;

        public ManagerAppointmentService(ManagerAppointmentRepo managerAppointmentRepo)
        {
            this.managerAppointmentRepo = managerAppointmentRepo;
        }

        public void AddManagerAppointment(ManagerAppointment managerAppointment)
        {
            managerAppointmentRepo.AddManagerAppointment(managerAppointment);
        }

        public void ChangeManagerAppointment(ManagerAppointment managerAppointment)
        {
            managerAppointmentRepo.ChangeManagerAppointment(managerAppointment);
        }

        public ManagerAppointment? GetManagerAppointment(String managerAppointmentID)
        {
            return managerAppointmentRepo.GetManagerAppointment(managerAppointmentID);
        }

        public BindingList<ManagerAppointment> GetAllManagerAppointments()
        {
            return managerAppointmentRepo.GetAllManagerAppointments();
        }
    }
}
