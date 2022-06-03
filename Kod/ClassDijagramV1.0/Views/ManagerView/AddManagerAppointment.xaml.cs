using ClassDijagramV1._0.ViewModel;
using System.Windows;

namespace ClassDijagramV1._0.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for AddManagerAppointment.xaml
    /// </summary>
    public partial class AddManagerAppointment : Window
    {
        private CalendarViewModel _calendarViewModel;
        public AddManagerAppointment()
        {
            InitializeComponent();

            _calendarViewModel = new CalendarViewModel(this);
            this.DataContext = _calendarViewModel;
        }
    }
}
