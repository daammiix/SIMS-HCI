using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.FileHandlers;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Model.Enums;
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
        // Project path
        public static string ProjectPath = System.Reflection.Assembly.GetExecutingAssembly().Location
            .Split(new string[] { "bin" }, StringSplitOptions.None)[0];

        #region Fields

        public string _accountsFilePath = "../../../Data/accounts.json";
        public string _patientsFilePath = "../../../Data/patients.json";
        private string _appointmentsFilePath = "../../../Data/appointments.json";
        private string _doctorsFilePath = "../../../Data/doctors.json";
        private string _secretaryFilePath = "../../../Data/secretary.json";
        private string _managerFilePath = "../../../Data/managers.json";
        private string _roomsFilePath = "../../../Data/rooms.json";
        private string _equipmentFilePath = "../../../Data/equipment.json";
        private string _storageFilePath = "../../../Data/storage.json";
        private string _roomAppointmentsFilePath = "../../../Data/roomAppointments.json";

        #endregion

        #region Properties
        public AppointmentController appointmentController { get; set; }

        public AccountController AccountController { get; set; }

        public PatientController PatientController { get; set; }

        public SurgeryController surgeryController { get; set; }

        public DoctorController DoctorController { get; set; }

        public RoomController roomController { get; set; }
        
        public SecretaryController SecretaryController { get; set; }

        public ManagerController ManagerController { get; set; }

        public EquipmentController equipmentController { get; set; }

        public EquipmentAppointmentController equipmentAppointmentController { get; set; }
        public RoomAppointmentController roomAppointmentController { get; set; }


        #endregion

        public App()
        {
            var appointmentRepository = new AppointmentRepo(new AppointmentFileHandler(_appointmentsFilePath));
            var appointmentService = new AppointmentService(appointmentRepository);
            appointmentController = new AppointmentController(appointmentService);

            // Patients
            var patientRepo = new PatientRepo(new FileHandler<Patient>(_patientsFilePath));
            var patientService = new PatientService(patientRepo);
            PatientController = new PatientController(patientService);

            // Accounts
            var accountRepo = new AccountRepo(new FileHandler<Account>(_accountsFilePath));
            var accountService = new AccountService(accountRepo);
            AccountController = new AccountController(accountService);

            //
            var surgeryRepository = new SurgeryRepo();
            var surgeryService = new SurgeryService(surgeryRepository);
            surgeryController = new SurgeryController(surgeryService);

            //
            var doctorRepository = new DoctorRepo(new DoctorFileHandler(_doctorsFilePath));
            var doctorService = new DoctorService(doctorRepository);
            DoctorController = new DoctorController(doctorService);

            // Secretary
            var secretaryRepo = new SecretaryRepo(new FileHandler<Secretary>(_secretaryFilePath));
            var secretaryService = new SecretaryService(secretaryRepo);
            SecretaryController = new SecretaryController(secretaryService);


            // Managers
            var managerRepo = new ManagerRepo(new FileHandler<Manager>(_managerFilePath));
            var managerService = new ManagerService(managerRepo);
            ManagerController = new ManagerController(managerService);

            // Rooms-Storage
            var roomRepo = new RoomRepoJson(new FileHandler<BindingList<Room>>(_roomsFilePath), new FileHandler<BindingList<Storage>>(_storageFilePath));
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

            // RoomAppointment
            var roomAppointmentRepo = new RoomAppointmentRepo(new FileHandler<BindingList<RoomAppointment>>(_roomAppointmentsFilePath));
            var roomAppointmentService = new RoomAppointmentService(roomAppointmentRepo);
            roomAppointmentController = new RoomAppointmentController(roomAppointmentService);

            var dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();

            MakeTestData();
        }

        private void dispatcherTimer_Tick(object? sender, EventArgs e)
        {
            equipmentAppointmentController.ScheduledAppointment();
            roomAppointmentController.ScheduledAppointments();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            AccountController.SaveAccounts();
            PatientController.SavePatients();
            appointmentController.SaveAppointments();
            DoctorController.SaveDoctors();
            SecretaryController.SaveSecretaries();
            ManagerController.SaveManagers();
            roomController.SaveRooms();
            roomAppointmentController.SaveRoomAppointment();
        }

        private void MakeTestData()
        {
            Patient p1 = new Patient("Pera", "Peric", "1595959565626", "M", "0655986598", "perap123@gmail.com",
                new DateTime(1992, 5, 25), "2222");
            p1.Id = 1; // Izbrisati kad se svi podaci budu pravili ovde
            Secretary s1 = new Secretary("Mika", "Lazic", "8921154845568", "M", "0696523145", "mikal123@gmail.com",
                new DateTime(1994, 3, 15));
            s1.Id = 2;
            Manager m1 = new Manager("Svetlana", "Gogalovic", "265959595959", "Z", "65959895956", "svetlanagogo@gmail.com",
                new DateTime(1987, 11, 12));
            m1.Id = 3;
            Doctor d1 = new Doctor("Dragana", "Cvetkovic", "54815181818", "Z", "061235236237", "dcetvkovic49@gmail.com",
                new DateTime(1991, 7, 17), DoctorType.dermatology, null);
            d1.Id = 4;
            Patient p2 = new Patient("Mira", "Mirkovic", "1659599494", "Z", "065594959", "miram@gmail.com",
                new DateTime(1998, 7, 20), "2525");
            p2.Id = 5;

            PatientController.AddPatient(p1);
            PatientController.AddPatient(p2);

            ManagerController.AddManager(m1);

            SecretaryController.AddSecretary(s1);

            DoctorController.AddDoctor(d1);

            Account ac1 = new Account(p1.Id, Role.Patient, "pacijent123", "pacijent123");
            Account ac2 = new Account(s1.Id, Role.Secretary, "sekretar123", "sekretar123");
            Account ac3 = new Account(m1.Id, Role.Manager, "upravnik123", "upravnik123");
            Account ac4 = new Account(d1.Id, Role.Doctor, "doktor123", "doktor123");

            AccountController.AddAccount(ac1);
            AccountController.AddAccount(ac2);
            AccountController.AddAccount(ac3);
            AccountController.AddAccount(ac4);

        }
    }
}
