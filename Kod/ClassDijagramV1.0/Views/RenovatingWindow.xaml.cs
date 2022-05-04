using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClassDijagramV1._0.Views
{
    /// <summary>
    /// Interaction logic for RenovatingAndChangingPurpose.xaml
    /// </summary>
    public partial class RenovaitingWindow : Window
    {
        readonly private String format = "dd/MM/yyyyTHH:mm";
        readonly private String timeFormat = "HH:mm";
        readonly private String dateFormat = "dd/MM/yyyy";

        public RoomController roomController;

        public EquipmentAppointmentController equipmentAppointmentController;
        public RoomAppointmentController roomAppointmentController;
        public AppointmentController appointmentController;

        public Room selectedRoom { get; set; }

        public BindingList<String> RoomsAvailable { get; set; }
        private BindingList<Availability> availabilities { get; set; }

        public String FromDate { get; set; }
        public String FromTime { get; set; }
        public String ToDate { get; set; }
        public String ToTime { get; set; }

        public DateTime selectedDate { get; set; }

        private readonly Random _random = new Random();
        public RenovaitingWindow(Room selectedRoom)
        {
            InitializeComponent();
            DataContext = this;

            var app = Application.Current as App;
            roomController = app.roomController;
            equipmentAppointmentController = app.equipmentAppointmentController;
            roomAppointmentController = app.roomAppointmentController;
            appointmentController = app.appointmentController;

            this.selectedRoom = selectedRoom;

            FromDate = DateTime.Now.ToString("dd/MM/yyyy");
            ToDate = DateTime.Now.ToString("dd/MM/yyyy");
            FromTime = DateTime.Now.ToString(timeFormat);
            ToTime = DateTime.Now.ToString(timeFormat);

            RoomsAvailable = new BindingList<String>();
            availabilities = new BindingList<Availability>();

            selectedDate = DateTime.Now.Date;

        }

        private void SaveRenovating_Click(object sender, RoutedEventArgs e)
        {
            DateTime fromDatetime, toDatetime;
            DateTime.TryParseExact(FromDate + "T" + FromTime, format, null, System.Globalization.DateTimeStyles.None, out fromDatetime);
            DateTime.TryParseExact(ToDate + "T" + ToTime, format, null, System.Globalization.DateTimeStyles.None, out toDatetime);

            if (!checkDateTimeAvailable(fromDatetime, toDatetime))
            {
                WarningDateTime warningDateTime = new WarningDateTime();
                warningDateTime.Show();
                return;
            }

            var appointmentId = RandomPassword();

            var roomAppointment = new RoomAppointment(appointmentId, selectedRoom.RoomID, fromDatetime, (fromDatetime-toDatetime));
            roomAppointmentController.AddRoomAppointment(roomAppointment);

            this.Close();
        }

        private void QuitRenovating_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void PickerDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ListsHandler();
        }

        private bool checkDateTimeAvailable(DateTime fromDatetime, DateTime toDatetime)
        {
            foreach (var avaible in availabilities)
            {
                if (fromDatetime < avaible.To && avaible.From < toDatetime)
                {
                    return false;
                }
            }
            return true;
        }

        private String formatAvailableTime(DateTime start, DateTime end)
        {
            availabilities.Add(new Availability(start, end));

            return start.ToString(timeFormat) + " - " + end.ToString(timeFormat);
        }

        public void ListsHandler()
        {
            RoomsAvailable.Clear();

            var roomAppointments = roomAppointmentController.GetAllRoomAppointments();
            var appointments = appointmentController.GetListOfAppointments();
            var equipmentAppointments = equipmentAppointmentController.GetAllEquipmentAppointment();

            foreach (var equipmentAppointment in equipmentAppointments)
            {
                if (equipmentAppointment.FromDateTime.Date == selectedDate)
                {
                    if (selectedRoom != null && (equipmentAppointment.RoomFrom == selectedRoom.RoomID || equipmentAppointment.RoomTo == selectedRoom.RoomID))
                    {
                        RoomsAvailable.Add(formatAvailableTime(equipmentAppointment.FromDateTime, equipmentAppointment.ToDateTime));
                    }
                }
            }
            foreach (var roomAppointment in roomAppointments)
            {
                if (roomAppointment.startDate.Date == selectedDate)
                {
                    if (roomAppointment.roomId == selectedRoom.RoomID)
                    {
                        RoomsAvailable.Add(formatAvailableTime(roomAppointment.startDate, roomAppointment.startDate + roomAppointment.duration));
                    }
                }
            }
            foreach (var appointment in appointments)
            {
                if (appointment.AppointmentDate.Date == selectedDate)
                {
                    if (appointment.Room.RoomID == selectedRoom.RoomID)
                    {
                        RoomsAvailable.Add(formatAvailableTime(appointment.AppointmentDate, appointment.AppointmentDate + appointment.Duration));
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

        public string RandomPassword()
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
