using ClassDijagramV1._0.Model;
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
using System.Windows.Shapes;

namespace ClassDijagramV1._0.Views.PatientView
{
    /// <summary>
    /// Interaction logic for PatientMainWindow.xaml
    /// </summary>
    public partial class PatientMainWindow : Window
    {

        #region Properties

        // Pacijent koji je ulogovan
        public Patient Patient { get; set; }
        public Account Account { get; set; }

        #endregion

        public PatientMainWindow(Patient p, Account account)
        {
            InitializeComponent();
            Patient = p;
            Account = account;
            this.DataContext = this;
            // prosledimo i pacijenta koji je ulogovan
            startWindow.Content = new PatientMainPage(this, Patient, Account);
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            var a = new MainWindow();
            a.Show();
            Window.GetWindow(this).Close();
        }

        private void openNotificationClick(object sender, RoutedEventArgs e)
        {
            startWindow.Content = new NotificationPage(this, Patient);
        }

        private void pocetnaClick(object sender, RoutedEventArgs e)
        {
            startWindow.Content = new PatientMainPage(this, Patient, Account);
        }
    }
}
