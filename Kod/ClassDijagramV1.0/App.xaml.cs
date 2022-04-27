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

        public static string _accountsFilePath = "../../../Data/accounts.json";
        public static string _patientsFilePath = "../../../Data/patients.json";
        private string _appointmentsFilePath = "../../../Data/appointments.json";
        private string _doctorsFilePath = "../../../Data/doctors.json";

        #endregion

        #region Properties
        public AppointmentController appointmentController { get; set; }

        public AccountController AccountController { get; set; }

        public PatientController PatientController { get; set; }

        public SurgeryController surgeryController { get; set; }
        public DoctorController doctorController { get; set; }

        public RoomController roomController { get; set; }

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
            doctorController = new DoctorController(doctorService);

            //
            roomController = new RoomController();


            MakeTestData();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            AccountController.SaveAccounts();
            PatientController.SavePatients();
            appointmentController.SaveAppointments();
            doctorController.SaveDoctors();
        }

        private void MakeTestData()
        {
            Patient p1 = new Patient("Pera", "Peric", "1595959565626", "M", "0655986598", "perap123@gmail.com",
                new DateTime(1992, 5, 25), "2222");
            Secretary s1 = new Secretary("Mika", "Lazic", "8921154845568", "M", "0696523145", "mikal123@gmail.com",
                new DateTime(1994, 3, 15));
            Manager m1 = new Manager("Svetlana", "Gogalovic", "265959595959", "Z", "65959895956", "svetlanagogo@gmail.com",
                new DateTime(1987, 11, 12));
            Doctor d1 = new Doctor("Dragana", "Cvetkovic", "54815181818", "Z", "061235236237", "dcetvkovic49@gmail.com",
                new DateTime(1991, 7, 17), DoctorType.dermatology, null);

            PatientController.AddPatient(p1);

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
