using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.FileHandlers;
using ClassDijagramV1._0.Repository;
using ClassDijagramV1._0.Service;
using Controller;
using Model;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDijagramV1._0
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Fields

        private string _accountsFilePath = "../../../Data/accounts.json";
        private string _patientsFilePath = "../../../Data/patients.json";
        private string _appointmentsFilePath = "../../../Data/appointments.json";
        private string _doctorsFilePath = "../../../Data/doctors.json";
        private string _roomsFilePath = "../../../Data/rooms.json";
        private string _equipmentFilePath = "../../../Data/equipment.json";

        #endregion

        #region Properties
        public AppointmentController appointmentController { get; set; }

        public AccountController AccountController { get; set; }

        public PatientController PatientController { get; set; }

        public SurgeryController surgeryController { get; set; }
        public DoctorController doctorController { get; set; }

        public RoomController roomController { get; set; }

        public EquipmentController equipmentController { get; set; }

        public EquipmentAppointmentController equipmentAppointmentController { get; set; }

        #endregion

        public App()
        {
            var appointmentRepository = new AppointmentRepo(new AppointmentFileHandler(_appointmentsFilePath));
            var appointmentService = new AppointmentService(appointmentRepository);
            appointmentController = new AppointmentController(appointmentService);

            // Patients
            var patientRepo = new PatientRepo(new PatientFileHandler(_patientsFilePath));
            var patientService = new PatientService(patientRepo);
            PatientController = new PatientController(patientService);

            // Accounts
            var accountRepo = new AccountRepo(new AccountFileHandler(_accountsFilePath));
            var accountService = new AccountService(accountRepo);
            AccountController = new AccountController(accountService);

            //
            var surgeryRepository = new SurgeryRepo();
            var surgeryService = new SurgeryService(surgeryRepository);
            surgeryController = new SurgeryController(surgeryService);

            //
            var doctorRepository = new DoctorRepo(new DoctorFileHandler(_doctorsFilePath));
            var doctorService = new DoctorService(doctorRepository);
            doctorController = new DoctorController(doctorService);

            // Rooms
            var roomRepo = new RoomRepoJson(new FileHandler<BindingList<Room>>(_roomsFilePath));
            var roomService = new RoomService(roomRepo);
            roomController = new RoomController(roomService);

            // Equipment
            var equipmentRepo = new EquipmentRepo();
            var equipmentService = new EquipmentService(equipmentRepo);
            equipmentController = new EquipmentController(equipmentService);

            // EquipmentAppointment
            var equipmentAppointmentRepo = new EquipmentAppointmentRepo();
            var equipmentAppointmentService = new EquipmentAppointmentService(equipmentAppointmentRepo);
            equipmentAppointmentController = new EquipmentAppointmentController(equipmentAppointmentService);
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            AccountController.SaveAccounts();
            PatientController.SavePatients();
            appointmentController.SaveAppointments();
            doctorController.SaveDoctors();
            roomController.SaveRooms();
        }
    }
}
