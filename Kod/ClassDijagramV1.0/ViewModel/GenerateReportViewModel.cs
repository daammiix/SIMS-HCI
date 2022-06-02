using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Helpers;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel
{
    public class GenerateReportViewModel : ObservableObject
    {
        readonly private String format = "dd/MM/yyyyTHH:mm";
        readonly private String fullFormat = "dd/MM/yyyy HH:mm";

        public String FromDate;
        public String FromTime;
        public String ToDate;
        public String ToTime;

        public RoomController roomController;
        public EquipmentAppointmentController equipmentAppointmentController;
        public RoomAppointmentController roomAppointmentController;
        public AppointmentController appointmentController;

        private MainRoomsViewModel mainRoomsViewModel;

        private BindingList<RoomToGenerate> _rooms = new BindingList<RoomToGenerate>();

        private RelayCommand _generateReport;
        private RelayCommand _cancelReport;

        public GenerateReportViewModel(MainRoomsViewModel mainRoomsViewModel)
        {
            var app = Application.Current as App;

            roomController = app.roomController;
            equipmentAppointmentController = app.equipmentAppointmentController;
            roomAppointmentController = app.roomAppointmentController;
            appointmentController = app.AppointmentController;

            this.mainRoomsViewModel = mainRoomsViewModel;

            FromDate = DateTime.Now.ToString("dd/MM/yyyy");
            FromTime = DateTime.Now.ToString("HH:mm");
            ToDate = DateTime.Now.ToString("dd/MM/yyyy");
            ToTime = DateTime.Now.ToString("HH:mm");
        }

        public RelayCommand GenerateReport
        {
            get
            {
                _generateReport = new RelayCommand(o =>
                {
                    GenerateReportAction();
                });

                return _generateReport;
            }
        }

        public RelayCommand CancelReport
        {
            get
            {

                _cancelReport = new RelayCommand(o =>
                {
                    this.mainRoomsViewModel.ResetView();
                });

                return _cancelReport;
            }
        }

        public String selectedFromDate
        {
            get
            {
                return FromDate;
            }
            set
            {
                if (FromDate == value)
                    return;
                FromDate = value;
                ListsHandler();
            }
        }

        public String selectedFromTime
        {
            get
            {
                return FromTime;
            }
            set
            {
                if (FromTime == value)
                    return;
                FromTime = value;
                ListsHandler();
            }
        }

        public String selectedToDate
        {
            get
            {
                return ToDate;
            }
            set
            {
                if (ToDate == value)
                    return;
                ToDate = value;
                ListsHandler();
            }
        }

        public String selectedToTime
        {
            get
            {
                return ToTime;
            }
            set
            {
                if (ToTime == value)
                    return;
                ToTime = value;
                ListsHandler();
            }
        }

        public BindingList<RoomToGenerate> RoomsAvailable
        {
            get
            {
                return _rooms;
            }
            set
            {
                if (_rooms == value)
                {
                    return;
                }
                _rooms = value;
            }
        }

        private void GenerateReportAction()
        {
            PDFGenerator pdfGenerator = new PDFGenerator(RoomsAvailable);
            this.mainRoomsViewModel.ResetView();
        }

        private bool checkDateSpansOverlap(DateTime fromDatetimeA, DateTime toDatetimeA, DateTime fromDatetimeB, DateTime toDatetimeB)
        {
            return fromDatetimeA <= toDatetimeB && fromDatetimeB <= toDatetimeA;
        }

        public void ListsHandler()
        {
            RoomsAvailable.Clear();

            var roomAppointments = roomAppointmentController.GetAllRoomAppointments();
            var appointments = appointmentController.GetListOfAppointments();
            var equipmentAppointments = equipmentAppointmentController.GetAllEquipmentAppointment();

            DateTime selectedFrom, selectedTo;
            TimeSpan selectedTimeFrom, selectedTimeTo;
            try
            {
                selectedFrom = DateTime.ParseExact(FromDate + "T" + FromTime, "dd/MM/yyyyTHH:mm", null);
                selectedTo = DateTime.ParseExact(ToDate + "T" + ToTime, "dd/MM/yyyyTHH:mm", null);
            }
            catch (FormatException)
            {
                return;
            }

            foreach (var equipmentAppointment in equipmentAppointments)
            {
                var aptFrom = equipmentAppointment.FromDateTime;
                var aptTo = equipmentAppointment.ToDateTime;
                if (checkDateSpansOverlap(aptFrom, aptTo, selectedFrom, selectedTo))
                {
                    var roomFrom = roomController.GetRoom(equipmentAppointment.RoomFrom);
                    var roomTo = roomController.GetRoom(equipmentAppointment.RoomTo);
                    RoomsAvailable.Add(new RoomToGenerate(equipmentAppointment.RoomFrom, roomFrom.RoomName, (equipmentAppointment.FromDateTime).ToString(fullFormat), equipmentAppointment.ToDateTime.ToString(fullFormat)));
                    RoomsAvailable.Add(new RoomToGenerate(equipmentAppointment.RoomTo, roomTo.RoomName, (equipmentAppointment.FromDateTime).ToString(fullFormat), equipmentAppointment.ToDateTime.ToString(fullFormat)));
                }
            }
            foreach (var roomAppointment in roomAppointments)
            {
                var aptFrom = roomAppointment.startDate;
                var aptTo = roomAppointment.startDate + roomAppointment.duration;
                if (checkDateSpansOverlap(aptFrom, aptTo, selectedFrom, selectedTo))
                {
                    var room = roomController.GetRoom(roomAppointment.roomId);
                    RoomsAvailable.Add(new RoomToGenerate(roomAppointment.roomId, room.RoomName, roomAppointment.startDate.ToString(fullFormat), (roomAppointment.startDate + roomAppointment.duration).ToString(fullFormat)));
                }
            }
            foreach (var appointment in appointments)
            {
                var aptFrom = appointment.AppointmentDate;
                var aptTo = appointment.AppointmentDate + appointment.Duration;
                if (checkDateSpansOverlap(aptFrom, aptTo, selectedFrom, selectedTo))
                {
                    var room = roomController.GetRoom(appointment.RoomId);
                    RoomsAvailable.Add(new RoomToGenerate(appointment.RoomId, room.RoomName, appointment.AppointmentDate.ToString(fullFormat), (appointment.AppointmentDate + appointment.Duration).ToString(fullFormat)));
                }
            }
        }
    }
}
