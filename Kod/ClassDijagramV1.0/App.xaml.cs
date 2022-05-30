using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Converters;
using ClassDijagramV1._0.FileHandlers;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Model.Enums;
using ClassDijagramV1._0.Repository;
using ClassDijagramV1._0.Service;
using ClassDijagramV1._0.ViewModel;
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

        #region Static Fields

        public static string _accountsFilePath = "../../../Data/accounts.json";
        public static string _patientsFilePath = "../../../Data/patients.json";
        public static string _appointmentsFilePath = "../../../Data/appointments.json";
        public static string _doctorsFilePath = "../../../Data/doctors.json";
        public static string _secretaryFilePath = "../../../Data/secretary.json";
        public static string _managerFilePath = "../../../Data/managers.json";
        public static string _roomsFilePath = "../../../Data/rooms.json";
        public static string _equipmentFilePath = "../../../Data/equipment.json";
        public static string _medicalRecordFilePath = "../../../Data/medicalRecords.json";
        public string _storageFilePath = "../../../Data/storage.json";
        public string _roomAppointmentsFilePath = "../../../Data/roomAppointments.json";
        public static string _purchaseOrdersFilePath = "../../../Data/purchaseOrders.json";
        public static string _hospitalRatingsFilePath = "../../../Data/hospitalratings.json";
        public static string _doctorRatingsFilePath = "../../../Data/doctorratings.json";
        public static string _activityFilePath = "../../../Data/activity.json";
        public string _medicinesFilePath = "../../../Data/medicines.json";
        public string _reportsFilePath = "../../../Data/reports.json";
        public static string _notificationsFilePath = "../../../Data/notifications.json";


        #endregion

        #region Properties
        public AppointmentController AppointmentController { get; set; }

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

        public MedicalRecordController MedicalRecordController { get; set; }

        public MedicineController medicinesController { get; set; }

        public ReportsController reportsController { get; set; }

        public QuarterlyReportsController QuarterlyReportsController { get; set; }

        public RatingController RatingController { get; set; }

        public ActivityController ActivityController { get; set; }

        public BanningPatientController BanningPatientController { get; set; }

        public PurchaseOrderController PurchaseOrderController { get; set; }

        public NotificationController NotificationController { get; set; }



        #endregion

        public App()
        {
            // Appointments

            // Rooms-Storage
            var roomRepo = new RoomRepoJson(new FileHandler<BindingList<Room>>(_roomsFilePath), new FileHandler<BindingList<Storage>>(_storageFilePath));
            var roomService = new RoomService(roomRepo);
            roomController = new RoomController(roomService);

            // Room Appointment
            var roomAppointmentRepo = new RoomAppointmentRepo(new FileHandler<BindingList<RoomAppointment>>(_roomAppointmentsFilePath));
            var roomAppointmentService = new RoomAppointmentService(roomAppointmentRepo);
            roomAppointmentController = new RoomAppointmentController(roomAppointmentService);

            // Doctor
            var doctorRepository = new DoctorRepo(new DoctorFileHandler(_doctorsFilePath));
            var doctorService = new DoctorService(doctorRepository);
            DoctorController = new DoctorController(doctorService);

            // Appointment
            var appointmentRepository = new AppointmentRepo(new FileHandler<Appointment>(_appointmentsFilePath));
            var appointmentService = new AppointmentService(appointmentRepository, doctorService,
                roomAppointmentService, roomService);
            AppointmentController = new AppointmentController(appointmentService);

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

            // Secretary
            var secretaryRepo = new SecretaryRepo(new FileHandler<Secretary>(_secretaryFilePath));
            var secretaryService = new SecretaryService(secretaryRepo);
            SecretaryController = new SecretaryController(secretaryService);


            // Managers
            var managerRepo = new ManagerRepo(new FileHandler<Manager>(_managerFilePath));
            var managerService = new ManagerService(managerRepo);
            ManagerController = new ManagerController(managerService);

            // Equipment
            var equipmentRepo = new EquipmentRepo();
            var equipmentService = new EquipmentService(equipmentRepo);
            equipmentController = new EquipmentController(equipmentService);

            // EquipmentAppointment
            var equipmentAppointmentRepo = new EquipmentAppointmentRepo();
            var equipmentAppointmentService = new EquipmentAppointmentService(equipmentAppointmentRepo);
            equipmentAppointmentController = new EquipmentAppointmentController(equipmentAppointmentService);

            // MedicalRecords
            var medicalRecordRepo = new MedicalRecordRepo(new FileHandler<MedicalRecord>(_medicalRecordFilePath));
            var medicalRecordService = new MedicalRecordService(medicalRecordRepo, patientService);
            MedicalRecordController = new MedicalRecordController(medicalRecordService);

            // HospitalRating
            var ratingRepository = new RatingRepo(new FileHandler<HospitalRating>(_hospitalRatingsFilePath), new FileHandler<DoctorRating>(_doctorRatingsFilePath));
            var ratingService = new RatingService(ratingRepository);
            RatingController = new RatingController(ratingService);

            // Activity Log
            var activityRepository = new ActivityRepo(new FileHandler<ActivityLog>(_activityFilePath));
            var activityService = new ActivityService(activityRepository);
            ActivityController = new ActivityController(activityService);

            // Banning Patient
            //var activityRepository = new ActivityRepo(new FileHandler<ActivityLog>(_activityFilePath));
            var banningPatientService = new BanningPatientService(activityService, accountRepo);
            BanningPatientController = new BanningPatientController(banningPatientService);

            //Medicines
            var medicinesRepo = new MedicinesRepo();
            var medicinesService = new MedicinesService(medicinesRepo);
            medicinesController = new MedicineController(medicinesService);

            //Reports
            var reportsRepo = new ReportsRepo();
            var reportsService = new ReportsService(reportsRepo);
            reportsController = new ReportsController(reportsService);

            //QuarterlyReports
            var quarterlyReportsRepo = new QuarterlyReportsRepo();
            var quarterlyReportsService = new QuarterlyReportsService(quarterlyReportsRepo);
            QuarterlyReportsController = new QuarterlyReportsController(quarterlyReportsService);

            var dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();

            // PurchaseOrders
            var purchaseOrderRepo = new PurchaseOrderRepo();
            var purchaseOrderService = new PurchaseOrderService(purchaseOrderRepo, roomService, equipmentService);
            PurchaseOrderController = new PurchaseOrderController(purchaseOrderService);

            // Appointment
            var notificationRepository = new NotificationRepo(new FileHandler<Notification>(_notificationsFilePath));
            var notificationService = new NotificationService(notificationRepository);
            NotificationController = new NotificationController(notificationService);

            MakeTestData();

            // Namestimo brojace 
            SetIdCounters();

            // Sve ovo sto je dodato kroz kod nece da se povezuje iako je sacuvano na kraju i u fajlu
            // jer se povezivanje odvija u konstruktoru repozitorijuma koji se formira pre svih podataka
            // tako da u trenutku formiranja ovi podaci ne postoje trebalo bi ovde ispod povezati sve
            // da bi se i ovi podaci lepo povezali
            ConnectData();

        }

        private void dispatcherTimer_Tick(object? sender, EventArgs e)
        {
            equipmentAppointmentController.ScheduledAppointment();
            roomAppointmentController.ScheduledAppointments();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            SaveAll();

        }

        private void SaveAll()
        {
            AccountController.SaveAccounts();
            PatientController.SavePatients();
            AppointmentController.SaveAppointments();
            DoctorController.SaveDoctors();
            SecretaryController.SaveSecretaries();
            ManagerController.SaveManagers();
            roomController.SaveRooms();
            MedicalRecordController.SaveMedicalRecords();
            roomAppointmentController.SaveRoomAppointment();
            RatingController.SaveHospitalRatings();
            RatingController.SaveDoctorRatings();
            ActivityController.SaveActivity();
            NotificationController.SaveNotifications();
        }

        private void MakeTestData()
        {

            Patient p1 = new Patient("Pera", "Peric", "1595959565626", Gender.Male, "0655986598", "perap123@gmail.com",
                new DateTime(1992, 5, 25), new Address("Srbija", "Novi Sad", "Dimitrija Tucovica", "12"), "2222");

            Secretary s1 = new Secretary("Mika", "Lazic", "8921154845568", Gender.Male, "0696523145", "mikal123@gmail.com",
                new DateTime(1994, 3, 15), new Address("Srbija", "Beograd", "Vojvode Putnika", "100/A"));

            Manager m1 = new Manager("Svetlana", "Gogalovic", "265959595959", Gender.Female, "65959895956", "svetlanagogo@gmail.com",
                new DateTime(1987, 11, 12), new Address("Srbija", "Novi Sad", "Dalmatinska", "123"));

            Doctor d1 = new Doctor("Dragana", "Cvetkovic", "54815181818", Gender.Female, "061235236237", "dcetvkovic49@gmail.com",
                new DateTime(1991, 7, 17), new Address("Srbija", "Beograd", "Karposeva", "56"), DoctorType.Dermatology);

            Doctor d2 = new Doctor("Milica", "Jovanovic", "5959884823", Gender.Male, "3875432", "the292200@gmail.com",
                new DateTime(1989, 5, 20), new Address("Srbija", "Novi Sad", "Bulevar Oslobodjenja", "100"), DoctorType.GeneralSurgery);

            Doctor d3 = new Doctor("Dragisa", "Perovic", "9897112156", Gender.Male, "3875432", "the292200@gmail.com",
                new DateTime(1985, 2, 22), new Address("Srbija", "Novi Sad", "Strazilovska", "33"), DoctorType.General);

            Doctor d4 = new Doctor("Dragan", "Draskovic", "559885556225", Gender.Male, "569856", "drasko@gmail.com",
                new DateTime(1968, 4, 12), new Address("Srbija", "Novi Sad", "Strazilovska", "92"), DoctorType.Dermatology);

            Patient p2 = new Patient("Mira", "Mirkovic", "1659599494", Gender.Female, "065594959", "miram@gmail.com",
                new DateTime(1998, 7, 20), new Address("Srbija", "Subotica", "Bulevar Cara Lazara", "167"), "2525");

            Patient p3 = new Patient("Jovan", "Peric", "959899959885", Gender.Female, "069985658", "jovvaann@gmail.com",
                new DateTime(1993, 3, 17), new Address("Srbija", "Smederevo", "Uzicka", "222/A"), "3993");


            MedicalRecord mr1 = new MedicalRecord(p1.Id, "Dragan", MaritalStatus.Single, "377899", BloodType.O);
            MedicalRecord mr2 = new MedicalRecord(p2.Id, "Slavisa", MaritalStatus.Divorced, "223223", BloodType.AB);
            MedicalRecord mr3 = new MedicalRecord(p3.Id, "Miodrag", MaritalStatus.Widow, "565659", BloodType.A);

            Room r1 = roomController.GetRoom("id");
            Room r2 = roomController.GetRoom("id5");

            Appointment? a1 = null;
            Appointment? a2 = null;
            if (r1 != null && r2 != null)
            {
                DateTime date1 = DateTime.Now.AddDays(1);
                DateTime date2 = DateTime.Now.AddDays(2);
                TimeSpan interval = new TimeSpan(0, 30, 0);
                a1 = new Appointment(p1.Id, d1.Id, r1.RoomID, date1, interval, AppointmentType.generalPractitionerCheckup);
                a2 = new Appointment(p2.Id, d1.Id, r2.RoomID, date2, interval, AppointmentType.generalPractitionerCheckup);
            }

            PatientController.AddPatient(p1);
            PatientController.AddPatient(p2);
            PatientController.AddPatient(p3);

            ManagerController.AddManager(m1);

            SecretaryController.AddSecretary(s1);

            DoctorController.AddDoctor(d1);
            DoctorController.AddDoctor(d2);
            DoctorController.AddDoctor(d3);
            DoctorController.AddDoctor(d4);

            MedicalRecordController.AddMedicalRecord(mr1);
            MedicalRecordController.AddMedicalRecord(mr2);
            MedicalRecordController.AddMedicalRecord(mr3);

            if (a1 != null)
            {
                AppointmentController.AddAppointment(a1);
            }

            if (a2 != null)
            {
                AppointmentController.AddAppointment(a2);
            }

            Account ac1 = new Account(p1.Id, Role.Patient, "pacijent123", "pacijent123");
            Account ac2 = new Account(s1.Id, Role.Secretary, "sekretar123", "sekretar123");
            Account ac3 = new Account(m1.Id, Role.Manager, "upravnik123", "upravnik123");
            Account ac4 = new Account(d1.Id, Role.Doctor, "doktor123", "doktor123");
            Account ac5 = new Account(p3.Id, Role.Patient, "jovan222", "jova123");
            Account ac6 = new Account(d2.Id, Role.Doctor, "milicaaa", "milicaaa");
            Account ac7 = new Account(d3.Id, Role.Doctor, "dragisa76", "ddd");
            Account ac8 = new Account(p2.Id, Role.Patient, "miraa1", "12345678");
            Account ac9 = new Account(d4.Id, Role.Doctor, "draskoo", "drasko68");

            AccountController.AddAccount(ac1);
            AccountController.AddAccount(ac2);
            AccountController.AddAccount(ac3);
            AccountController.AddAccount(ac4);
            AccountController.AddAccount(ac5);
            AccountController.AddAccount(ac6);
            AccountController.AddAccount(ac7);
            AccountController.AddAccount(ac8);
            AccountController.AddAccount(ac9);

        }

        private void ConnectData()
        {
            foreach (Patient patient in PatientController.GetPatients())
            {
                // Vezemo pacijente za njihove kartone
                foreach (MedicalRecord medicalRecord in MedicalRecordController.GetMedicalRecords())
                {
                    if (medicalRecord.PatientId == patient.Id)
                    {
                        patient.MedicalRecordNumber = medicalRecord.Number;
                    }
                }

                // Ako su im appointmenti null sto ce da bude slucaj uvek jer ih ne serijalizujemo
                if (patient.Appointments == null)
                {
                    patient.Appointments = new List<Appointment>();
                }

                // Vezemo pacijente za njihove appointmente
                foreach (Appointment appointment in AppointmentController.GetAppointments())
                {
                    if (appointment.PatientId == patient.Id)
                    {
                        patient.Appointments.Add(appointment);
                    }
                }
            }
        }

        /// <summary>
        /// Postavlja sve id countere na maximalan id, da bi kad se prave nove stvari kroz program imale 
        /// jedinstveni id
        /// </summary>
        private void SetIdCounters()
        {
            int maxId = -1;

            // Za osobe

            foreach (Patient p in PatientController.GetPatients())
            {
                if (p.Id > maxId)
                {
                    maxId = p.Id;
                }
            }
            foreach (Secretary s in SecretaryController.GetSecretaries())
            {
                if (s.Id > maxId)
                {
                    maxId = s.Id;
                }
            }
            foreach (Manager m in ManagerController.GetManagers())
            {
                if (m.Id > maxId)
                {
                    maxId = m.Id;
                }
            }
            foreach (Doctor d in DoctorController.GetAllDoctors())
            {
                if (d.Id > maxId)
                {
                    maxId = d.Id;
                }
            }

            // Updatujemo brojac
            Person.idCounter = maxId;

            maxId = -1;
            // Medicinski kartoni
            foreach (MedicalRecord mr in MedicalRecordController.GetMedicalRecords())
            {
                // Kartoni imaju broj ne id
                if (mr.Number > maxId)
                {
                    maxId = mr.Number;
                }
            }

            // Updatujemo brojac
            MedicalRecord.numCounter = maxId;

            maxId = -1;
            // Appointmneti
            foreach (Appointment appointment in AppointmentController.GetAppointments())
            {
                if (appointment.Id > maxId)
                {
                    maxId = appointment.Id;
                }
            }

            // Updatujemo brojac
            Appointment.idCounter = maxId;

            // Narudzbine
            List<PurchaseOrder> purchaseOrders = PurchaseOrderController.GetPurchaseOrders();
            if (purchaseOrders.Count > 0)
            {
                maxId = purchaseOrders.Max(order => order.Id);
                PurchaseOrder.idCounter = maxId;
            }
        }

        private void Application_Deactivated(object sender, EventArgs e)
        {
            SaveAll();
            Environment.Exit(0);
        }
    }
}
