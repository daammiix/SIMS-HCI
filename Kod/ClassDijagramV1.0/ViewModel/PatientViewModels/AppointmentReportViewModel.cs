using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
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
    public class AppointmentReportViewModel
    {
        public string Doctor { get; set; }
        public string Date { get; set; }
        public string DoctorReport { get; set; }
        public string Zabiljeska { get; set; }
        public List<MedicineDrug> Lijekovi { get; set; }

        private Appointment Pregled { get; set; }

        public AppointmentReportViewModel(Appointment patientMain)
        {
            Pregled = patientMain;
            FillFields();
        }

        private void FillFields()
        {
            App app = Application.Current as App;
            MedicalRecordController _medicalRecordController = app.MedicalRecordController;
            PatientController patientController = app.PatientController;
            DoctorController doctorController = app.DoctorController;
            RoomController roomController = app.roomController;

            Patient patient = patientController.GetPatientById(Pregled.PatientId);
            MedicalRecord medicalRecord = _medicalRecordController.GetPatientsMedicalRecord(patient.Id);
            Doctor doctor = doctorController.GetDoctorById(Pregled.DoctorId);
            Room room = roomController.GetRoom(Pregled.RoomId);


            Doctor = doctor.Name + " " + doctor.Surname;
            Date = Pregled.AppointmentDate.ToString("dd.MM.yyyy HH:mm");

            DoctorReport = Pregled.MedicalReport.Description;
            Zabiljeska = Pregled.MedicalReport.Note;
            Lijekovi = Pregled.MedicalReport.Medicine;

        }
    }
}
