using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
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

        public ObservableCollection<AppointmentViewModel> _appointmentViewModels;
        public ObservableCollection<AppointmentViewModel> _oldAppointmentViewModels;

        private MedicineController _medicineController;
        private BanningPatientController _banningPatientController;
        private DoctorController _doctorController;
        private RoomController _roomController;

        #endregion

        public PatientMainPage(PatientMainWindow patientMain)
        {
            InitializeComponent();
            parent = patientMain;

            App app = Application.Current as App;
            _medicineController = app.medicinesController;
            _banningPatientController = app.BanningPatientController;
            _doctorController = app.DoctorController;
            _roomController = app.roomController;

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
            await Task.Delay(500);
            AppointmentAddPage adp = new AppointmentAddPage(parent, _appointmentViewModels);
            parent.startWindow.Content = adp;
            adp.doctorRB.IsChecked = true;
            PriorityDoctor prioritetDoktor = new PriorityDoctor(parent, _appointmentViewModels);
            adp.prioritetFrame.Content = prioritetDoktor;

            await Task.Delay(2000);
            prioritetDoktor.dodavanjPregledaDoktor.SelectedItem = _doctorController.GetAllDoctors()[0];
            await Task.Delay(1000);
            prioritetDoktor.kalendar.SelectedDate = new DateTime(2222, 10, 10, 10, 0, 0);
            await Task.Delay(1000);
            prioritetDoktor.timeCB.SelectedItem = "10:00";
            await Task.Delay(2000);
            prioritetDoktor.addAppBtn.Background = new SolidColorBrush(Colors.Orange);


            await Task.Delay(100);
            DateTime deset = new DateTime(2222,10,10,10,0,0);
            TimeSpan interval = new TimeSpan(0, 0, 30, 0);
            Appointment simulacijaPregled = new Appointment(parent.Patient.Id,
                                            _doctorController.GetAllDoctors()[0].Id,_roomController.GetAllRooms()[0].RoomID,
                                            deset,interval,AppointmentType.generalPractitionerCheckup);
            AppointmentViewModel avm = new AppointmentViewModel(simulacijaPregled);
            _appointmentViewModels.Add(avm);

            AppointmentsViewPage avp = new AppointmentsViewPage(parent, _appointmentViewModels);
            parent.startWindow.Content = avp;
            await Task.Delay(2000);
            avp.tabelaPregledi.SelectedItem = avm;
            avp.izmijeni.Background = new SolidColorBrush(Colors.Orange);
            await Task.Delay(1000);

            AppointmentUpdatePage aup = new AppointmentUpdatePage(parent, _appointmentViewModels);
            parent.startWindow.Content = aup;
            aup.doctorRB.IsChecked = true;
            UpdatePriorityDoctor prioritetUpdateDoktor = new UpdatePriorityDoctor(parent, _appointmentViewModels);
            aup.prioritetFrame.Content = prioritetUpdateDoktor;

            prioritetUpdateDoktor.izmjenaPregledaDoktor.SelectedItem = _doctorController.GetAllDoctors()[0];
            prioritetUpdateDoktor.promjenaKalendar.SelectedDate = new DateTime(2222, 10, 10, 10, 0, 0);

            await Task.Delay(4000);
            prioritetUpdateDoktor.promjenaKalendar.SelectedDate = new DateTime(2222, 10, 11, 11, 0, 0);
            await Task.Delay(2000);
            prioritetUpdateDoktor.timeCB.SelectedItem = "11:00";
            await Task.Delay(2000);
            prioritetUpdateDoktor.addAppBtn.BorderThickness = new Thickness(5);

            
            DateTime jedanaest = new DateTime(2222, 11, 11, 11, 0, 0);
            Appointment simulacijaUpdatePregled = new Appointment(parent.Patient.Id,
                                            _doctorController.GetAllDoctors()[0].Id, _roomController.GetAllRooms()[0].RoomID,
                                            jedanaest, interval, AppointmentType.generalPractitionerCheckup);
            AppointmentViewModel auvm = new AppointmentViewModel(simulacijaUpdatePregled);
            _appointmentViewModels.Remove(avm);
            _appointmentViewModels.Add(auvm);

            parent.startWindow.Content = avp;
            await Task.Delay(4000);
            avp.tabelaPregledi.SelectedItem = auvm;
            await Task.Delay(4000);
            avp.otkazi.Background = new SolidColorBrush(Colors.Orange);
            _appointmentViewModels.Remove(auvm);


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
            List<String> dvaLijeka = new List<String>() { medicines[rnd.Next(0, medicines.Count - 1)].Name ,
                                                          medicines[rnd.Next(0, medicines.Count - 1)].Name };
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
