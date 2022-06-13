using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.Views.PatientView;
using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel.PatientViewModels
{
    public class UpdateAppointmentViewModel : BindableBase
    {
        public string Doctor { get; set; }
        public string Date { get; set; }
        public string Room { get; set; }
        private Appointment Pregled { get; set; }

        public UpdateAppointmentViewModel(Appointment patientMain)
        {
            Pregled = patientMain;
            FillFields();
        }

        private void FillFields()
        {
            App app = Application.Current as App;
            PatientController patientController = app.PatientController;
            DoctorController doctorController = app.DoctorController;
            RoomController roomController = app.roomController;

            Doctor doctor = doctorController.GetDoctorById(Pregled.DoctorId);
            Room room = roomController.GetRoom(Pregled.RoomId);

            Doctor = doctor.Name + " " + doctor.Surname;
            Date = Pregled.AppointmentDate.ToString("dd.MM.yyyy HH:mm");
            Room = room.RoomName + " " + room.RoomNumber;

        }
    }
}
