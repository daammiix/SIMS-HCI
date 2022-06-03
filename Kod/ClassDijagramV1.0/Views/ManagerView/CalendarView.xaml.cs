using System.Windows.Controls;
using Syncfusion.UI.Xaml.Scheduler;
using System.Windows.Input;

namespace ClassDijagramV1._0.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for CalendarView.xaml
    /// </summary>
    public partial class CalendarView : UserControl
    {
        public CalendarView()
        {
            InitializeComponent();
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void Schedule_CellTapped(object sender, CellTappedEventArgs e)
        {
            AddManagerAppointment addManagerAppointment = new AddManagerAppointment();
            addManagerAppointment.Show();
        }

        private void Schedule_AppointmentEditorOpening(object sender, AppointmentEditorOpeningEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
