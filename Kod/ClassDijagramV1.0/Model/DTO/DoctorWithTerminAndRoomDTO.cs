using Model;
using System;

namespace ClassDijagramV1._0.Model.DTO
{
    // Koristi se kod predloga termina za hitan pregled da bi se prosledio i termin i doktor za kog je vezan termin
    public class DoctorWithTerminAndRoomDTO
    {
        #region Properties

        public Doctor Doctor { get; set; }

        // Termin u kom je doktor slobodan
        public DateTime Termin { get; set; }

        // Soba za koju je vezan termin
        public Room Room { get; set; }

        #endregion


        #region Constructor

        public DoctorWithTerminAndRoomDTO(Doctor doctor, DateTime termin, Room room)
        {
            Doctor = doctor;
            Termin = termin;
            Room = room;
        }

        #endregion
    }
}
