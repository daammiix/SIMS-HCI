using ClassDijagramV1._0.ViewModel.SecretaryViewModels.AppointmentsViewModels;
using Controller;
using Model;
using Syncfusion.UI.Xaml.Scheduler;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClassDijagramV1._0.Views.SecretaryView.AppointmentsView
{
    /// <summary>
    ///
    /// </summary>
    public partial class AppointmentsMainView : UserControl
    {
        #region Fields

        private AppointmentController _appointmentController;

        private AppointmentsMainViewModel _viewModel;

        #endregion

        public AppointmentsMainView()
        {
            App app = Application.Current as App;

            _appointmentController = app.AppointmentController;

            InitializeComponent();

            _viewModel = this.DataContext as AppointmentsMainViewModel;

        }

        private void Schedule_AppointmentEditorOpening(object sender, AppointmentEditorOpeningEventArgs e)
        {
            // Da se ne prikazuje default editor window
            e.Cancel = true;

            if (e.Appointment != null)
            {
                // Prikazemo custom window za editovanje appointmenta
                // Editovanje iskljuceno jer se prikazuju kao indicator
                MessageBox.Show("editovanje");

            }
            else
            {
                // Uzmemo selktovani dan
                DateTime selectedDate = e.DateTime;
                // Prikazemo custom window za dodavanje appointmenta i prosledimo mu listu
                // AppointmentViewModela da bi tu dodali novi Appointment kako bi se prikazao u scheduleru
                // MessageBox.Show("dodavanje");
                AddAppointmentDialog addAppointmentDialog = new AddAppointmentDialog(_viewModel.ScheduleViewModel.Appointments,
                    selectedDate);
                addAppointmentDialog.Owner = Application.Current.MainWindow;
                addAppointmentDialog.ShowDialog();
            }
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Castujemo object u ListViewItem posto smo na to kliknuli da bi proizveli event
            ListViewItem listViewItem = sender as ListViewItem;
            if (listViewItem != null)
            {
                // Data context za listViewItem je ScheduleAppointment
                ScheduleAppointment scheduleAppointment = (ScheduleAppointment)listViewItem.DataContext;
                // Iskoristimo Scedule Appointment subject sto nam je Id da uzmemo appointment
                // koji menjamo iz kontroler
                Appointment appointmentToChange = _appointmentController.GetAppointmentById(int.Parse(scheduleAppointment.Subject));
                int appointmentToChangeId = int.Parse(scheduleAppointment.Subject);
                // Proverimo da li slucajno nije null sto ce da se desi samo ako on postoji u View-u a u bazi ne postoji
                if (appointmentToChange != null)
                {
                    // Prikazemo window za menjanje 
                    ChangeAppointmentDialog changeAppointmentDialog = new ChangeAppointmentDialog();
                    // Stavimo data context i prosledimo mu id appointmenta koji menjamo i listu appointmentViewModela
                    changeAppointmentDialog.DataContext = new ChangeAppointmentDialogViewModel(appointmentToChangeId,
                        _viewModel.ScheduleViewModel.Appointments);
                    changeAppointmentDialog.Owner = Application.Current.MainWindow;
                    changeAppointmentDialog.ShowDialog();
                }
            }
        }
    }
}
