using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ClassDijagramV1._0.Views.PatientView
{
    /// <summary>
    /// Interaction logic for PatientMainPage.xaml
    /// </summary>
    public partial class PatientMainPage : Page
    {
        #region Fields
        private PatientMainWindow parent { get; set; }

        private ObservableCollection<AppointmentViewModel> _appointmentViewModels;
        private ObservableCollection<AppointmentViewModel> _oldAppointmentViewModels;

        public MedicineController _medicineController;
        private BanningPatientController _banningPatientController;

        #endregion

        public PatientMainPage(PatientMainWindow patientMain)
        {
            InitializeComponent();
            parent = patientMain;

            App app = Application.Current as App;
            _medicineController = app.medicinesController;
            _banningPatientController = app.BanningPatientController;

            _appointmentViewModels = new ObservableCollection<AppointmentViewModel>();
            _oldAppointmentViewModels = new ObservableCollection<AppointmentViewModel>();

            SplitAppointments();
        }

        private void AppointmentsViewOpen(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new AppointmentsViewPage(parent,_appointmentViewModels);
        }

        private void AddAppointmetClick(object sender, RoutedEventArgs e)
        {
            if (_banningPatientController.CheckStatusOfPatient(parent.Patient.Id, parent.Account) == true)
            {
                zakazivanjeRed.Background = new SolidColorBrush(Colors.Red);
                iks.Kind = MaterialDesignThemes.Wpf.PackIconKind.Close;
                zakazivenjeTekst.Text = "Banovani ste!";
                zakazivenjeTekst.Foreground = new SolidColorBrush(Colors.Red);
                zakazivanjeOpis.Text = "Previse puta ste promjenili svoje preglede";
                zakazivanjeOpis.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                parent.startWindow.Content = new AppointmentAddPage(parent, _appointmentViewModels);
            }
            
            
            
        }

        private void RatingOpen(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new RatingPage(parent, _oldAppointmentViewModels);
        }

        private void uvidUTerapijuClick(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new TerapyPage(parent, _oldAppointmentViewModels);
        }

        private void zdravstveniKartonClick(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new MedicalRecordPage(parent, _oldAppointmentViewModels);
        }

        private async void demoClick(object sender, RoutedEventArgs e)
        {
            await Task.Delay(1000);
            parent.startWindow.Content = new AppointmentAddPage(parent, _appointmentViewModels);
            await Task.Delay(3000);
            parent.startWindow.Content = new RatingPage(parent, _appointmentViewModels);
            await Task.Delay(1000);
            parent.startWindow.Content = new MedicalRecordPage(parent, _oldAppointmentViewModels);
        }

        /// <summary>
        /// Podjela pregleda na stare i nove
        /// </summary>
        /// <returns></returns>
        private void SplitAppointments()
        {
            foreach (Appointment a in parent.Patient.Appointments)
            {
                if (a.AppointmentDate < DateTime.Now)
                {
                    _oldAppointmentViewModels.Add(new AppointmentViewModel(a));
                    addMedicalReportToAllAppointmnts(a);
                }
                else
                {
                    _appointmentViewModels.Add(new AppointmentViewModel(a));
                }
            }
        }

        /// <summary>
        /// Fja dodaje doktorov izvjestaj na pregled, posto nemamo doktora
        /// </summary>
        /// <returns></returns>
        private void addMedicalReportToAllAppointmnts(Appointment appointment)
        {
            Random rnd = new Random();
            var medicines = _medicineController.GetAllMedicines();
            List<String> dvaLijeka = new List<String>() { medicines[rnd.Next(0, medicines.Count - 1)].ID ,
                                                          medicines[rnd.Next(0, medicines.Count - 1)].ID };
            String loremIpsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris sit amet purus a ligula tempus porttitor. Ut urna orci, fermentum eget nibh quis, commodo convallis eros. Maecenas ut efficitur nisi, ac hendrerit dolor. Nullam non pretium lectus. Nulla facilisi. Praesent euismod mi nunc, ut commodo felis efficitur a. Cras quis arcu tortor. Nam congue ultrices metus eget eleifend. In hac habitasse platea dictumst. Nunc bibendum ante nec iaculis aliquet. In vel odio." +
                                "Donec placerat pretium velit ac eleifend.Suspendisse vel vehicula lacus, et mollis orci.Vestibulum dictum dolor a cursus laoreet.Nunc vel ex at leo egestas malesuada sed accumsan eros.Vestibulum auctor, massa ut viverra vulputate, dui lectus accumsan urna, non pharetra risus nunc eu felis.Nulla eu accumsan metus.Vestibulum tempor convallis quam, at ornare felis molestie vitae.Fusce vel ante sed felis pharetra pharetra at quis neque.Ut eleifend pellentesque mauris," +
                                " commodo convallis eros. Maecenas ut efficitur nisi, ac hendrerit dolor. Nullam non pretium lectus. Nulla facilisi. Praesent euismod mi nunc, ut commodo felis efficitur a. Cras quis arcu tortor. Nam congue ultrices metus eget eleifend. In hac habitasse platea dictumst. Nunc bibendum ante nec iaculis aliquet. In vel odio auctor, molestie risus at, faucibus orci. Etiam feugiat neque mauris, ut ultrices lectus porttitor eu. Nullam in sodales elit, sit amet auctor odio.";
            MedicineDrug medicine1 = new MedicineDrug(dvaLijeka[0],DateTime.Now,DateTime.Now.AddDays(21),8);
            MedicineDrug medicine2 = new MedicineDrug(dvaLijeka[0], DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), 16);
            List<MedicineDrug> medicineTherapy = new List<MedicineDrug>() { medicine1, medicine2};
            if (appointment.MedicalReport == null)
            {
                appointment.MedicalReport = new MedicalReport(loremIpsum, medicineTherapy);
            }
        }
    }
}
