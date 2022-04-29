using Controller;
using Model;
using System;
using System.Collections.Generic;
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

namespace ClassDijagramV1._0.Views
{
    /// <summary>
    /// Interaction logic for RenovatingAndChangingPurpose.xaml
    /// </summary>
    public partial class RenovatingAndChangingPurpose : Window
    {
        readonly private String format = "dd/MM/yyyyTHH:mm";
        public RoomController roomController;
        public Room selectedRoom;
        public RenovatingAndChangingPurpose()
        {
            InitializeComponent();
            var app = Application.Current as App;
            roomController = app.roomController;
            this.selectedRoom = selectedRoom;
            DataContext = this;
        }

        private void SaveRenovating_Click(object sender, RoutedEventArgs e)
        {
            DateTime fromDatetime, toDatetime;
            String date = FromDate.Text;
            String time = FromTime.Text;
            DateTime.TryParseExact(date + "T" + time, format, null, System.Globalization.DateTimeStyles.None, out fromDatetime);

            date = ToDate.Text;
            time = ToTime.Text;
            DateTime.TryParseExact(date + "T" + time, format, null, System.Globalization.DateTimeStyles.None, out toDatetime);

            //equipmentAppointmentController.ScheduledAppointment(selectedRoom, destinationRoom, selectedEquipment, quantity, fromDatetime, toDatetime);
            this.Close();
        }

        private void QuitRenovating_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
