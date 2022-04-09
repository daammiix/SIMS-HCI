using ClassDijagramV1._0.Dialog;
using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClassDijagramV1._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public AppointmentController _appointmentController;

        public ObservableCollection<Appointment> Appointments
        {
            get;
            set;
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AutoColumns_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
