using ClassDijagramV1._0;
using ClassDijagramV1._0.Model;
using Model;
using Repository;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using ClassDijagramV1._0.Model.DTO;
using ClassDijagramV1._0.Service;

namespace Service
{
    public class AppointmentService
    {
        #region Fields

        private AppointmentRepo _appointmentRepo;

        private DoctorService _doctorService;

        private RoomAppointmentService _roomAppointmentService;

        private RoomService _roomService;

        #endregion

        #region Constructor
        public AppointmentService(AppointmentRepo repo, DoctorService doctorService,
            RoomAppointmentService roomAppointmentService, RoomService roomService)
        {
            _appointmentRepo = repo;
            _doctorService = doctorService;
            _roomAppointmentService = roomAppointmentService;
            _roomService = roomService;
        }

        #endregion

        #region Methods

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
            //_appointmentRepo.AddNewAppointment(updatedAppointment);
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
            Appointment requiredAppointment = null;
            foreach (Appointment a in _appointmentRepo.GetAppointments())
            {
                if (a.Id == id)
                {
                    requiredAppointment = a;
                    break;
                }
            }

            return requiredAppointment;

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
                if (TerminsOverlaps(start, duration, a))
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

        /// <summary>
        /// Pronalazi najblizi slobodan termin za bilo kog doktora sa prosledjenom specijalizacijom
        /// Ako nema slobodnih termina u narednih sat vremena pomerice najblizi termin
        /// </summary>
        /// <param name="doctorType">Specijalizacija</param>
        /// <param name="duration">Trajanje</param>
        /// <param name="from">Od kog trenutka trazimo</param>
        /// <returns> Najblizi slobodan termin </returns>
        public DoctorWithTerminAndRoomDTO? FindClosestFreeTerminForSpecialization(DoctorType doctorType, DateTime from, int duration)
        {
            // From nam je samo date zapravo kad ga biramo iz kalendara stavimo mu minute i sekunde na iste kao ovaj trenutak
            // ne mozemo da staivmo minute i sekunde jer su readonly pa napravimo samo novi DateTime
            from = new DateTime(from.Year, from.Month, from.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            // Ret val
            DoctorWithTerminAndRoomDTO? closestFreeTermin = null;

            // Da li mozemo da zakazemo termin za 5 min i kad mu dodamo duration da se nijedan drugi termin ne preklopi
            bool isFree = true;


            // Dodamo 5 minuta na from jer hocemo da nam krene slucaj 5 minuta od sad
            from = from.AddMinutes(5);

            // Prodjemo kroz sve doktore prosledjene specijalizacije
            foreach (Doctor doctor in _doctorService.GetDoctorsForSpecialization(doctorType))
            {
                // Proverimo da li doktor trenutno ima appointment
                Appointment? currentAppointment = getDoctorsCurrentAppointment(doctor.Id, from);

                // Ako je doktor zauzet from stavimo na kraj appointmenta
                if (currentAppointment != null)
                {
                    // Ako taj appointment nece da se zavrsi u narednih pola sata idemo na sledeceg doktora
                    if (currentAppointment.AppointmentDate + currentAppointment.Duration > from.AddMinutes(30))
                    {
                        continue;
                    }
                    else
                    {
                        // Ako hoce stavimo da nas appointment pocinje 5 minuta posle nego sto se trenutni zavrsi
                        from = currentAppointment.AppointmentDate + currentAppointment.Duration;
                        from = from.AddMinutes(5);
                    }

                }

                // Pomerimo appointmente koji se preklapaju(sama funkcija handluje slucaj kada ih je 0)
                MoveOverlapingAppointments(doctor.Id, from.AddMinutes(duration), from);

                // Uzmemo prvu slobodnu sobu
                Room? freeRoom = FindFreeRoom(from, from.AddMinutes(duration));

                closestFreeTermin = new DoctorWithTerminAndRoomDTO(doctor, from, freeRoom);

                // breakujemo jer smo nasli najblizi slobodan termin
                break;
            }

            // ako je ovde closestFreeTermin == null treba da stavimo da appointment pocinje 5 minuta posle nego sto se najskoriji zavrsi
            // bice null ako smo breakovali gore uvek, tj svaki doktor je zauzet u narednih pola sata
            if (closestFreeTermin == null)
            {
                // Uzmemo najblizi appointment za specijalizaciju
                Appointment? closestAppointment = FindClosestAppoitmentForSpecialization(from, doctorType);

                // Provera da li nije null, ne bi trebalo da ikad bude null ako smo dosli do ovde
                // jer ako je null onda nema appointmenta za tu specijalizaciju i mogli smo da zakazemo za 5 min
                if (closestAppointment != null)
                {
                    // napravimo dto sa ovim doktorom i sobom iz appointmenta i pocetkom termina 5 min posle kraja ovog
                    closestFreeTermin = createDoctorWithTerminAndRoomDTO(closestAppointment);

                    // Pomerimo doktorove appointmente koji se preklapaju
                    MoveOverlapingAppointments(closestAppointment.DoctorId,
                        closestAppointment.AppointmentDate + closestAppointment.Duration, closestAppointment.AppointmentDate);
                }
            }

            return closestFreeTermin;
        }

        #endregion


        #region Private Helpers

        /// <summary>
        /// Vraca trenutni appointment doktora ili null ako doktor trenutno nema appointment
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="from"></param>
        /// <returns></returns>
        private Appointment? getDoctorsCurrentAppointment(int doctorId, DateTime from)
        {
            // Uzmemo listu appointmenta doktora
            List<Appointment> doctorsAppointments = GetDoctorAppointments(doctorId);

            // Proverimo da li doktor trenutno ima appointment
            Appointment? currentAppointment = IsDoctorBusy(doctorsAppointments, from);

            return currentAppointment;
        }

        private DoctorWithTerminAndRoomDTO createDoctorWithTerminAndRoomDTO(Appointment closestAppointment)
        {
            // Uzmemo doktora i sobu vezanu za appointment
            Doctor doctor = _doctorService.GetDoctorById(closestAppointment.DoctorId);
            Room room = _roomService.GetARoom(closestAppointment.RoomId);
            // start je kraj ovog najblize i jos 5 minuta
            DateTime start = closestAppointment.AppointmentDate + closestAppointment.Duration;
            start = start.AddMinutes(5);

            // napravimo dto sa ovim doktorom i sobom iz appointmenta i pocetkom termina 5 min posle kraja ovog
            return new DoctorWithTerminAndRoomDTO(doctor, start, room);
        }

        /// <summary>
        /// Vraca listu appointmenta vezanu za doktora
        /// </summary>
        /// <param name="doctorId">Id doktora cije appointmente trazimo</param>
        /// <returns>List appointmenta vezana za doktora</returns>
        private List<Appointment> GetDoctorAppointments(int doctorId)
        {
            // Ret val
            List<Appointment> ret = new List<Appointment>();

            foreach (Appointment appointment in _appointmentRepo.Appointments)
            {
                if (appointment.DoctorId == doctorId)
                {
                    ret.Add(appointment);
                }
            }

            return ret;
        }

        /// <summary>
        /// Nalazi najskoriji termin za doktore iz prosledjene specijalizacije
        /// </summary>
        /// <param name="specialization"></param>
        /// <param name="from">Trenutak od kog trazimo</param>
        /// <returns></returns>
        private Appointment? FindClosestAppoitmentForSpecialization(DateTime from, DoctorType specialization)
        {
            // Ret val
            Appointment? ret = null;

            // Uzmemo sve appointmente za specijalizaciju
            List<Appointment> appointmentsForSpecialization = FindAppointmentsForSpecialization(specialization);

            // Ako ima uopste appointmenta za datu specijalizaciju
            if (appointmentsForSpecialization.Count > 0)
            {

                // Sortiramo ih po vremenu
                appointmentsForSpecialization = appointmentsForSpecialization.OrderBy(appointment => appointment.AppointmentDate).ToList();

                // Uzmemo prvi koji se zavrsava posle from
                foreach (Appointment appointment in appointmentsForSpecialization)
                {
                    if (appointment.AppointmentDate + appointment.Duration >= from)
                    {
                        ret = appointment;
                        break;
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// Pronalazi sve appointmente za prosledjenu specijalizaciju
        /// </summary>
        /// <param name="specialization"></param>
        /// <returns></returns>
        private List<Appointment> FindAppointmentsForSpecialization(DoctorType specialization)
        {
            // Ret val
            List<Appointment> appointmentsForSpecialization = new List<Appointment>();

            foreach (Appointment appointment in _appointmentRepo.Appointments)
            {
                // Uzmemo doktora koji je vezan za appointment
                Doctor appointmentDoctor = _doctorService.GetDoctorById(appointment.DoctorId);

                // Ako je doktor odgovarajuceg tipa dodamo appointment u listu
                if (appointmentDoctor.Type == specialization)
                {
                    appointmentsForSpecialization.Add(appointment);
                }
            }

            return appointmentsForSpecialization;
        }

        /// <summary>
        /// Vraca sortiranu listu appointmenta koji se preklapaju, tj. pocinju izmedju from i to ne uzimamo u obzir one koji pocinju
        /// pre pa kad im se doda duration upadaju
        /// </summary>
        /// <param name="appointments"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>Lista appointmenta koji se preklapaju</returns>
        private List<Appointment> GetAppointmentsThatOverlaps(List<Appointment> appointments, DateTime from, DateTime to)
        {
            // Ret val
            List<Appointment> ret = new List<Appointment>();

            // Ubacimo sve koji su izmedju u ret, i sortiramo tako da je prvi onaj najblizi
            ret = appointments
                .Where(appointment => (appointment.AppointmentDate > from) && (appointment.AppointmentDate < to))
                .OrderBy(appointment => appointment.AppointmentDate)
                .ToList();

            return ret;
        }

        /// <summary>
        /// Proverimo da li ima appointmenta koji je poceo pre from a i dalje traje
        /// </summary>
        /// <param name="doctorsAppointments"></param>
        /// <param name="from"></param>
        /// <returns>Appointment koji i dalje traje ako ga ima u suprotnom null</returns>
        private Appointment? IsDoctorBusy(List<Appointment> doctorsAppointments, DateTime from)
        {
            // Ret val
            Appointment? ret = null;

            // Prodjemo kroz sve appointmente
            foreach (Appointment appointment in doctorsAppointments)
            {
                DateTime appointmentStart = appointment.AppointmentDate;
                DateTime appointmentEnd = appointment.AppointmentDate + appointment.Duration;

                // Ako je izmedju nasli smo ga i njega vratimo
                if (appointmentStart < from && appointmentEnd > from)
                {
                    ret = appointment;
                    break;
                }
            }

            return ret;
        }

        /// <summary>
        /// Pomera listu appointmenta iza to, tako da ce prvi da pomeri 15 min iza to a ostale 15 min iza kraja svakog sledeceg 
        /// </summary>
        public void MoveOverlapingAppointments(int doctorId, DateTime to, DateTime from)
        {

            // Uzmemo listu appointmenta doktora
            List<Appointment> doctorsAppointments = GetDoctorAppointments(doctorId);

            // Dodamo 5 minuta od from da bi poceo za 5 min
            from = from.AddMinutes(5);

            // uzmemo sortirane appointment
            List<Appointment> sortedOverlapingAppointments = GetAppointmentsThatOverlaps(doctorsAppointments, from, to);

            // Prvo proverimo da li uopste ima nekog appointmenta koji se preklapa
            if (sortedOverlapingAppointments.Count > 0)
            {
                // pomerimo prvi 15 min iza to
                sortedOverlapingAppointments[0].AppointmentDate = to.AddMinutes(15);

                // ako uopste ima vise od 1
                if (sortedOverlapingAppointments.Count > 1)
                {
                    // svaki sledeci pomerimo 15 min posle kraja prethodnog
                    for (int i = 1; i < sortedOverlapingAppointments.Count; i++)
                    {
                        Appointment previous = sortedOverlapingAppointments[i - 1];
                        Appointment current = sortedOverlapingAppointments[i];

                        // stavimo na kraj prethodnog
                        DateTime moveTo = previous.AppointmentDate + previous.Duration;
                        // dodamo jos 15 min
                        moveTo = moveTo.AddMinutes(15);

                        // updatujemo vreme
                        sortedOverlapingAppointments[i].AppointmentDate = moveTo;
                    }
                }
            }
        }

        /// <summary>
        /// Pronalazi prvu slobodnu sobu u datom terminu
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>Slobodnu sobu</returns>
        private Room? FindFreeRoom(DateTime from, DateTime to)
        {
            // Ret val
            Room? ret = null;

            foreach (Room room in _roomService.GetAllRooms())
            {
                if (IsRoomFree(from, to, room))
                {
                    ret = room;
                }
            }

            return ret;
        }

        /// <summary>
        /// Proverava da li ima appointment-a u prosledjenoj sobi za prosledjeno vreme
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        private bool IsRoomFree(DateTime from, DateTime to, Room room)
        {
            // Ret val
            bool isFree = true;

            foreach (Appointment appointment in GetAppointments())
            {
                if (appointment.RoomId.Equals(room.RoomID))
                {
                    if (TerminsOverlaps(from, to - from, appointment) || !_roomAppointmentService.CheckIsTerminFree(
                        from, to - from, room))
                    {
                        isFree = false;
                        break;
                    }
                }
            }

            return isFree;
        }

        /// <summary>
        /// Proverava da li termin upada u termin appointmenta
        /// </summary>
        /// <param name="from"></param>
        /// <param name="Duration"></param>
        /// <param name="appointment"></param>
        /// <returns></returns>
        private bool TerminsOverlaps(DateTime from, TimeSpan duration, Appointment appointment)
        {
            // Prvo proverimo da li se vremena preklapaju
            // 1. Moze from da bude pre appointmenta ali kad se doda durasis da udje u termin
            // 2. Moze from da pocne posle pocetka a pre kraja appointment-a
            if ((from < appointment.AppointmentDate && from + duration > appointment.AppointmentDate) ||
                 (from >= appointment.AppointmentDate && from <= appointment.AppointmentDate + appointment.Duration))
            {
                return true;
            }

            return false;
        }

        #endregion

    }
}