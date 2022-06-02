using ClassDijagramV1._0.FileHandlers;
using ClassDijagramV1._0.Model;
using Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Repository
{
    public class AppointmentRepo
    {

        private String Path;

        private FileHandler<Notification> _notificationFileHandler;
        private FileHandler<Appointment> _appointmentFileHandler;

        public ObservableCollection<Appointment> Appointments;
        public ObservableCollection<Appointment> AppointmentsByPatient;
        public ObservableCollection<Doctor> Doctors
        {
            get;
            set;
        }

        public AppointmentRepo(FileHandler<Appointment> fileHandler)
        {
            _appointmentFileHandler = fileHandler;
            Appointments = new ObservableCollection<Appointment>(_appointmentFileHandler.GetItems());
            Doctors = new ObservableCollection<Doctor>();
            /*Room r1 = new Room();
            DateTime date1 = new DateTime(2008, 5, 1, 8, 30, 52);
            DateTime date2 = new DateTime(2010, 8, 18, 13, 30, 30);
            TimeSpan interval = date2 - date1;
            Doctor d1 = new Doctor("doktor", "doktor", "123", "musko", "3875432", "the292200", date1);
            Doctor d2 = new Doctor("lekar", "lekar", "123", "musko", "3875432", "the292200", date1);
            Doctors.Add(d1);

            Patient p1 = new Patient("djordje", "lipovcic", "123", "musko", "3875432", "the292200", date1, "1234");
            Patient p2 = new Patient("sandra", "brkic", "123", "musko", "3875432", "the292200", date1, "1234");

            Appointment a1 = new Appointment(p1, r1, d1, "1", date1, interval, AppointmentType.generalPractitionerCheckup);
            Appointment a2 = new Appointment(p2, r1, d2, "2", date1, interval, AppointmentType.generalPractitionerCheckup);
            Appointments.Add(a1);
            Appointments.Add(a2);*/
        }

        public void UpdateAppointment(int oldAppointmentID, Appointment updatedAppointment)
        {
            Appointment oldAppointment = FindAppointmentById(oldAppointmentID);
            oldAppointment = updatedAppointment;
        }

        public ObservableCollection<Appointment> GetAppointments()
        {
            return Appointments;
        }

        public void SaveAppointments()
        {
            _appointmentFileHandler.SaveItems(Appointments.ToList());
        }

        /// <summary>
        /// Dodaje novi appointment ako ne postoji appointment sa istim id-em, u suprotnom ce da pregazi stari
        /// </summary>
        /// <param name="newAppointment"></param>
        /// <returns></returns>
        public void AddNewAppointment(Appointment newAppointment)
        {
            bool exists = false;
            Appointment toUpdate = null;
            foreach (Appointment appointment in Appointments)
            {
                if (appointment.Id == newAppointment.Id)
                {
                    exists = true;
                    toUpdate = appointment;
                    break;
                }
            }

            if (!exists)
            {
                Appointments.Add(newAppointment);
            }
            else
            {
                toUpdate = newAppointment;
            }
        }

        public void RemoveAppointment(int appointmentID)
        {
            Appointments.Remove(FindAppointmentById(appointmentID));
        }


        public Appointment FindAppointmentById(int appointmentID)
        {
            foreach (Appointment a in Appointments)
            {
                if (a.Id == appointmentID)
                {
                    return a;
                }
            }
            return null;
        }

        public ObservableCollection<Appointment> GetListOfAppointments()
        {
            return Appointments;
        }
    }
}