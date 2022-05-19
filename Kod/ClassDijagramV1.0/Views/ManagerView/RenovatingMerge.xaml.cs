using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Views.ManagerView;
using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClassDijagramV1._0.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for RenovatingMerge.xaml
    /// </summary>
    public partial class RenovatingMerge : Window
    {
        readonly private String format = "dd/MM/yyyyTHH:mm";
        readonly private String fullFormat = "dd/MM/yyyy HH:mm";

        public String FromDate { get; set; } = DateTime.Now.ToString("dd/MM/yyyy");
        public String FromTime { get; set; } = DateTime.Now.ToString("HH:mm");
        public String ToDate { get; set; } = DateTime.Now.ToString("dd/MM/yyyy");
        public String ToTime { get; set; } = DateTime.Now.ToString("HH:mm");

        public RoomController roomController;
        public EquipmentAppointmentController equipmentAppointmentController;
        public RoomAppointmentController roomAppointmentController;
        public AppointmentController appointmentController;

        public BindingList<Room> Rooms { get; set; }
        public BindingList<String> MergingToRoomAvailable { get; set; }
        public BindingList<String> MergingRoomAvailable { get; set; }
        private BindingList<Availability> availabilities { get; set; }

        private readonly Random _random = new Random();

        public Room selectedRoom { get; set; }
        public Room mergingRoom;
        public RenovatingMerge(Room selectedRoom)
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;
            roomController = app.roomController;
            equipmentAppointmentController = app.equipmentAppointmentController;
            roomAppointmentController = app.roomAppointmentController;
            appointmentController = app.AppointmentController;
            this.selectedRoom = selectedRoom;
            Rooms = new BindingList<Room>();
            MergingToRoomAvailable = new BindingList<String>();
            MergingRoomAvailable = new BindingList<String>();
            availabilities = new BindingList<Availability>();

            UpdateAvailableRooms();
        }

        private void SaveRenovatingMerge_Click(object sender, RoutedEventArgs e)
        {
            Room? mergingRoom = (Room?)MergingRoom.SelectedItem;
            DateTime fromDatetime = DateTime.ParseExact(FromDateField.Text + "T" + FromTimeField.Text, format, null);
            DateTime toDatetime = DateTime.ParseExact(ToDateField.Text + "T" + ToTimeField.Text, format, null);

            if (!checkDateTimeAvailable(fromDatetime, toDatetime))
            {
                WarningDateTime warningDateTime = new WarningDateTime();
                warningDateTime.Show();
                return;
            }

            var appointmentId = RandomId();

            var roomAppointment = new RoomAppointment(appointmentId, selectedRoom.RoomID, fromDatetime, toDatetime - fromDatetime, mergingRoom.RoomID);
            roomAppointmentController.AddRoomAppointment(roomAppointment);

            this.Close();
        }

        private void QuitRenovatingMerge_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool checkTimeSpansOverlap(DateTime fromDatetimeA, DateTime toDatetimeA, DateTime fromDatetimeB, DateTime toDatetimeB)
        {
            return fromDatetimeA <= toDatetimeB && fromDatetimeB <= toDatetimeA;
        }

        private bool checkDateTimeAvailable(DateTime fromDatetime, DateTime toDatetime)
        {
            foreach (var avaible in availabilities)
            {
                if (checkTimeSpansOverlap(fromDatetime, toDatetime, avaible.From, avaible.To))
                {
                    return false;
                }
            }
            return true;
        }

        private void DateField_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ListsHandler();
        }

        private void MergingRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListsHandler();
        }

        public void UpdateAvailableRooms()
        {
            
            BindingList<Room> rooms = roomController.GetAllRooms();
            foreach (Room room in rooms)
            {
                if (room == selectedRoom)
                {
                    continue;
                }
                if (room.Floor == selectedRoom.Floor)
                {
                    Rooms.Add(room);
                }
            }
        }
        private String formatAvailableTime(DateTime start, DateTime end)
        {
            availabilities.Add(new Availability(start, end));
            return start.ToString(fullFormat) + " - " + end.ToString(fullFormat);
        }

        public void ListsHandler()
        {
            MergingToRoomAvailable.Clear();
            MergingRoomAvailable.Clear();

            var roomAppointments = roomAppointmentController.GetAllRoomAppointments();
            var appointments = appointmentController.GetListOfAppointments();
            var equipmentAppointments = equipmentAppointmentController.GetAllEquipmentAppointment();

            Room? mergingRoom = (Room?)MergingRoom.SelectedItem;
            if (mergingRoom == null)
            {
                return;
            }

            DateTime selectedFrom, selectedTo;
            try
            {
                selectedFrom = DateTime.ParseExact(FromDateField.Text, "dd/MM/yyyy", null);
                selectedTo = DateTime.ParseExact(ToDateField.Text, "dd/MM/yyyy", null);
            }
            catch (FormatException)
            {
                return;
            }

            foreach (var equipmentAppointment in equipmentAppointments)
            {
                var aptFrom = equipmentAppointment.FromDateTime.Date;
                var aptTo = equipmentAppointment.ToDateTime.Date;
                if (checkTimeSpansOverlap(aptFrom, aptTo, selectedFrom, selectedTo))
                {
                    if (equipmentAppointment.RoomFrom == mergingRoom.RoomID || equipmentAppointment.RoomTo == mergingRoom.RoomID)
                    {
                        MergingRoomAvailable.Add(formatAvailableTime(equipmentAppointment.FromDateTime, equipmentAppointment.ToDateTime));
                    }
                    if (selectedRoom != null && (equipmentAppointment.RoomFrom == selectedRoom.RoomID || equipmentAppointment.RoomTo == selectedRoom.RoomID))
                    {
                        MergingToRoomAvailable.Add(formatAvailableTime(equipmentAppointment.FromDateTime, equipmentAppointment.ToDateTime));
                    }
                }
            }
            foreach (var roomAppointment in roomAppointments)
            {
                var aptFrom = roomAppointment.startDate.Date;
                var aptTo = (roomAppointment.startDate + roomAppointment.duration).Date;
                if (checkTimeSpansOverlap(aptFrom, aptTo, selectedFrom, selectedTo))
                {
                    if (roomAppointment.roomId == mergingRoom.RoomID)
                    {
                        MergingRoomAvailable.Add(formatAvailableTime(roomAppointment.startDate, roomAppointment.startDate + roomAppointment.duration));
                    }
                    if (roomAppointment.roomId == selectedRoom.RoomID)
                    {
                        MergingToRoomAvailable.Add(formatAvailableTime(roomAppointment.startDate, roomAppointment.startDate + roomAppointment.duration));
                    }
                }
            }
            foreach (var appointment in appointments)
            {
                var aptFrom = appointment.AppointmentDate.Date;
                var aptTo = (appointment.AppointmentDate + appointment.Duration).Date;
                if (checkTimeSpansOverlap(aptFrom, aptTo, selectedFrom, selectedTo))
                {
                    if (appointment.RoomId == mergingRoom.RoomID)
                    {
                        MergingRoomAvailable.Add(formatAvailableTime(appointment.AppointmentDate, appointment.AppointmentDate + appointment.Duration));
                    }
                    if (appointment.RoomId == selectedRoom.RoomID)
                    {
                        MergingToRoomAvailable.Add(formatAvailableTime(appointment.AppointmentDate, appointment.AppointmentDate + appointment.Duration));
                    }
                }
            }
        }

        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        public string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);


            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length = 26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

        public string RandomId()
        {
            var passwordBuilder = new StringBuilder();

            // 4-Letters lower case   
            passwordBuilder.Append(RandomString(4, true));

            // 4-Digits between 10 and 99
            passwordBuilder.Append(RandomNumber(10, 99));

            return passwordBuilder.ToString();
        }
    }
}
