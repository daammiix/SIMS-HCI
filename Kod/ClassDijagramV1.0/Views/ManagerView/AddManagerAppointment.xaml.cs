using ClassDijagramV1._0.ViewModel;
using System.Windows;

namespace ClassDijagramV1._0.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for AddManagerAppointment.xaml
    /// </summary>
    public partial class AddManagerAppointment : Window
    {
        private AddManagerAppointmentViewModel _addManagerAppointmentViewModel;
        public AddManagerAppointment()
        {
            InitializeComponent();

            _addManagerAppointmentViewModel = new AddManagerAppointmentViewModel(this);
            this.DataContext = _addManagerAppointmentViewModel;
        }
    }
}
