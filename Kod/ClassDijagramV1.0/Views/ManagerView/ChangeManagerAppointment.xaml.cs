using ClassDijagramV1._0.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using ClassDijagramV1._0.ViewModel;
using System.Windows;

namespace ClassDijagramV1._0.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for ChangeManagerAppointment.xaml
    /// </summary>
    public partial class ChangeManagerAppointment : Window
    {
        private CalendarViewModel _calendarViewModel;
        public ChangeManagerAppointment()
        {
            InitializeComponent();

            _calendarViewModel = new CalendarViewModel(this);
            this.DataContext = _calendarViewModel;
        }
    }
}
