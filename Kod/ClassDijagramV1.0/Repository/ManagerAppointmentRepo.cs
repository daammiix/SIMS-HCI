using ClassDijagramV1._0.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Repository
{
    public class ManagerAppointmentRepo
    {
        private String path = "..\\..\\..\\Data\\managerAppointments.json";
        private BindingList<ManagerAppointment> managerAppointments = new BindingList<ManagerAppointment>();
        public ManagerAppointmentRepo()
        {
            string jsonData = System.IO.File.ReadAllText(path);
            BindingList<ManagerAppointment>? jsonManagerAppointment = JsonSerializer.Deserialize<BindingList<ManagerAppointment>>(jsonData);
            if (jsonManagerAppointment != null)
            {
                this.managerAppointments = jsonManagerAppointment;
            }
        }
        public ManagerAppointment AddManagerAppointment(ManagerAppointment managerAppointment)
        {
            managerAppointments.Add(managerAppointment);
            writeManagerAppointment();
            return managerAppointment;
        }

        private int findManagerAppointment(String managerAppointmentID)
        {
            int i = 0;
            foreach (ManagerAppointment managerAppointment in managerAppointments)
            {
                if (managerAppointment.ID == managerAppointmentID)
                {
                    return i;
                }
                i++;
            }
            return -1;
        }

        public ManagerAppointment? ChangeManagerAppointment(ManagerAppointment managerAppointment)
        {
            int index = findManagerAppointment(managerAppointment.ID);
            if (index == -1)
            {
                return null;
            }
            managerAppointments.RemoveAt(index);
            managerAppointments.Insert(index, managerAppointment);
            writeManagerAppointment();
            return managerAppointment;
        }

        public ManagerAppointment? GetManagerAppointment(String managerAppointmentID)
        {
            foreach (var managerAppointment in managerAppointments)
            {
                if (managerAppointment.ID == managerAppointmentID)
                {
                    return managerAppointment;
                }
            }
            return null;
        }

        public BindingList<ManagerAppointment> GetAllManagerAppointments()
        {
            return managerAppointments;
        }

        private void writeManagerAppointment()
        {
            String jsonString = JsonSerializer.Serialize(managerAppointments);
            System.IO.File.WriteAllText(path, jsonString);
        }
    }
}
