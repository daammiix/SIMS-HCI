using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
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

namespace ClassDijagramV1._0.Views.PatientView
{
    /// <summary>
    /// Interaction logic for PatientMainPage.xaml
    /// </summary>
    public partial class PatientMainPage : Page
    {
        #region Fields

        private Patient _logedPatient;
        private Account _account;

        private ObservableCollection<AppointmentViewModel> _appointmentViewModels;
        private ObservableCollection<AppointmentViewModel> _oldAppointmentViewModels;

        public MedicineController _medicineController;

        #endregion

        private PatientMainWindow parent { get; set; }
        public PatientMainPage(PatientMainWindow patientMain, Patient logedPatient, Account account)
        {
            InitializeComponent();
            parent = patientMain;
            _logedPatient = logedPatient;
            _account = account;

            App app = Application.Current as App;
            _medicineController = app.medicinesController;

            // napravimo listu appointmentViewModela od svakog appointmenta pacijenta
            _appointmentViewModels = new ObservableCollection<AppointmentViewModel>();
            _oldAppointmentViewModels = new ObservableCollection<AppointmentViewModel>();
            foreach (Appointment a in _logedPatient.Appointments)
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

        private void AppointmentsViewOpen(object sender, RoutedEventArgs e)
        {
            // Proslledimo appointmentViewModels da bi mogli da prikazemo appointmente i ulogovanog pacijenta
            parent.startWindow.Content = new AppointmentsViewPage(_appointmentViewModels, parent, parent.Patient, _account);
        }

        private void AddAppointmetClick(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new AppointmentAddPage(parent, _appointmentViewModels, _logedPatient);
        }

        private void RatingOpen(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new RatingPage(parent, _appointmentViewModels, _logedPatient);
        }

        private void uvidUTerapijuClick(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new TerapyPage(parent, _logedPatient);
        }

        private void zdravstveniKartonClick(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new MedicalRecordPage(parent, _logedPatient, _oldAppointmentViewModels);
        }

        private void addMedicalReportToAllAppointmnts(Appointment appointment)
        {
            Random rnd = new Random();     
            var medicines = _medicineController.GetAllMedicines();
            List<String> dvaLijeka = new List<String>() { medicines[rnd.Next(0, medicines.Count - 1)].ID ,
                                                          medicines[rnd.Next(0, medicines.Count - 1)].ID
                                                        };
            String loremIpsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris sit amet purus a ligula tempus porttitor. Ut urna orci, fermentum eget nibh quis, commodo convallis eros. Maecenas ut efficitur nisi, ac hendrerit dolor. Nullam non pretium lectus. Nulla facilisi. Praesent euismod mi nunc, ut commodo felis efficitur a. Cras quis arcu tortor. Nam congue ultrices metus eget eleifend. In hac habitasse platea dictumst. Nunc bibendum ante nec iaculis aliquet. In vel odio auctor, molestie risus at, faucibus orci. Etiam feugiat neque mauris, ut ultrices lectus porttitor eu. Nullam in sodales elit, sit amet auctor odio." +
                                "Donec placerat pretium velit ac eleifend.Suspendisse vel vehicula lacus, et mollis orci.Vestibulum dictum dolor a cursus laoreet.Nunc vel ex at leo egestas malesuada sed accumsan eros.Vestibulum auctor, massa ut viverra vulputate, dui lectus accumsan urna, non pharetra risus nunc eu felis.Nulla eu accumsan metus.Vestibulum tempor convallis quam, at ornare felis molestie vitae.Fusce vel ante sed felis pharetra pharetra at quis neque.Ut eleifend pellentesque mauris quis feugiat.Phasellus et orci urna" + "ur adipiscing elit. Mauris sit amet purus a ligula tempus porttitor. Ut urna orci, fermentum eget nibh quis, commodo convallis eros. Maecenas ut efficitur nisi, ac hendrerit dolor. Nullam non pretium lectus. Nulla facilisi. Praesent euismod mi nunc, ut commodo felis efficitur a. Cras quis arcu tortor. Nam congue ultrices metus eget eleifend. In hac habitasse platea dictumst. Nunc bibendum ante nec iaculis aliquet. In vel odio auctor, molestie risus at, faucibus orci. Etiam feugiat neque mauris, ut ultrices lectus porttitor eu. Nullam in sodales elit, sit amet auctor odio." +
                                "Donec placerat pretium velit ac eleifend.Suspendisse vel vehicula lacus, et mollis orci.Vestibulum dictum dolor a cursus laoreet.Nunc vel ex at leo egestas malesuada sed accumsan eros.Vestibulum auctor, massa ut viverra vulputate, dui lectus accumsan urna, non pharetra risus nunc eu felis.Nulla eu accumsan metus.Vestibulum tempor convallis quam, at ornare felis molestie vitae.Fusce vel ante sed felis pharetra pharetra at quis neque.Ut eleifend pellentesque mauris quis feugiat.Phasellus et orci urna";

            appointment.MedicalReport.Description = loremIpsum;
            appointment.MedicalReport.Medicine = dvaLijeka;
        }
    }
}
