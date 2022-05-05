using ClassDijagramV1._0;
using ClassDijagramV1._0.Model;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Service
{
    public class AppointmentService
    {
        private AppointmentRepo _appointmentRepo;

        public AppointmentService(AppointmentRepo repo)
        {
            _appointmentRepo = repo;
        }

        public void AddAppointment(Appointment appointment)
        {
            _appointmentRepo.AddNewAppointment(appointment);
        }

        public void RemoveAppointment(int appointmentID)
        {
            _appointmentRepo.RemoveAppointment(appointmentID);
        }

        public void UpdateAppointment(int oldAppointmentID, Appointment updatedAppointment)
        {
            _appointmentRepo.UpdateAppointment(oldAppointmentID, updatedAppointment);
        }

        public void SaveAppointments()
        {
            _appointmentRepo.SaveAppointments();
        }


        /// <summary>
        /// Vraca sve appointmente
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Appointment> GetAppointments()
        {
            return _appointmentRepo.GetAppointments();
        }

        /// <summary>
        /// Vraca appointment sa zadatim id-em, ako ne postoji vraca null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Appointment GetAppointmentById(int id)
        {
            Appointment ret = null;
            foreach (Appointment a in _appointmentRepo.GetAppointments())
            {
                if (a.Id == id)
                {
                    ret = a;
                    break;
                }
            }

            return ret;

        }

        public ObservableCollection<Appointment> GetAllAppointmentsByPatient(int patientID) // obrisi iz bajdinga preglede koji mi ne trebaju, tj nisu od tog pacijenta
        {
            ObservableCollection<Appointment> otherPatients = new ObservableCollection<Appointment>();
            foreach (Appointment a in _appointmentRepo.GetAppointments())
            {
                if (a.PatientId.Equals(patientID))
                {
                    otherPatients.Add(a);
                }
            }
            return otherPatients;
        }

        public ObservableCollection<Appointment> GetAllAppointments()
        {
            return _appointmentRepo.GetAppointments();
        }

        // room prosledjen jer nema u appointment-u 
        internal void AddNotification(Appointment appointment, Room r1, NotificationType notificationType)
        {

            Notification note;



            if (NotificationType.addingAppointment.Equals(notificationType))
            {
                var notificationID = "1";
                var content = "Imate zakazan pregled u " + appointment.AppointmentDate + " u sobi " + r1.RoomName;
                DateTime created = appointment.AppointmentDate;
                Notification n = new Notification(notificationID, content, "djordje", false, created, NotificationType.addingAppointment);
                _appointmentRepo.AddNotification(n);
            }
            else if (NotificationType.deletingAppointment.Equals(notificationType))
            {
                // _appointmentRepo.RemoveNotification()
                // mora se povezati apojntment sa notifikacijom
            }
        }

        internal ObservableCollection<Notification> GetAllNotifications()
        {
            return _appointmentRepo.GetAllNotifications();
        }

        public Appointment GetOneAppointment(int appointmentID)
        {
            return _appointmentRepo.GetOneAppointment(appointmentID);
        }

        /// <summary>
        /// Proverava da li je termin zauzet odnosno da li vec postoji neki appointment u istoj sobi za dato vreme
        /// ili da li neki lekar vec ima appointment u to vreme
        /// </summary>
        /// <param name="start"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public bool CheckIsTerminFree(Appointment newAppointment)
        {
            // Pokupimo sve informacije koje nam trebaju
            DateTime start = newAppointment.AppointmentDate;
            TimeSpan duration = newAppointment.Duration;
            String roomId = newAppointment.RoomId;
            int doctorId = newAppointment.DoctorId;

            // Ret Value
            bool free = true;

            // Prodjemo kroz sve appointmente
            foreach (Appointment a in _appointmentRepo.GetAppointments())
            {
                // Ako se appointmentId-evi poklapaju onda preskocimo jer ovaj appointment ne uzimamo u opticaj
                // treba nam ovo kada menjamo vec postojeci appointment
                if (a.Id == newAppointment.Id)
                {
                    continue;
                }

                // Prvo proverimo da li se vremena preklapaju
                // 1. Moze novi da pocinje pre i da kad se doda duration udje u termin ovog
                // 2. Moze novi da pocne u terminu ovog
                if ((start < a.AppointmentDate && (start + duration) > a.AppointmentDate) ||
                    (start >= a.AppointmentDate && start <= (a.AppointmentDate + a.Duration)))
                {
                    // Sad proverimo da li su ista soba
                    if (roomId.Equals(a.RoomId))
                    {
                        // ako jesu znaci da soba nije slobodan 
                        free = false;
                        break;
                    }

                    // Proverimo da li je isti lekar u pitanju
                    if (doctorId.Equals(a.DoctorId))
                    {
                        // ako jesu znaci da lekar nije slobodan 
                        free = false;
                        break;
                    }
                }
            }
            return free;
        }

        public ObservableCollection<Appointment> GetListOfAppointments()
        {
            return _appointmentRepo.GetListOfAppointments();

        }

    }
}