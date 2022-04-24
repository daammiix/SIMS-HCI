using ClassDijagramV1._0.Model;
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
    /// Interaction logic for NotificationPage.xaml
    /// </summary>
    public partial class NotificationPage : Page
    {
        private List<MedicineDrug> drugs = new List<MedicineDrug>();

        //ObservableCollection<MedicineDrug> Drugs;

        public ObservableCollection<MedicineDrug> Drugs
        {
            get;
            set;
        }
        


        ObservableCollection<Notification> Notification
        {
            get;
            set;
        }
        
        //ObservableCollection<MedicineDrug> Drugs = new ObservableCollection<MedicineDrug>();

        private PatientMainWindow parent { get; set; }
        public NotificationPage(PatientMainWindow patientMain)
        {
            InitializeComponent(); 
            this.DataContext = this;
            Drugs = new ObservableCollection<MedicineDrug>();
            parent = patientMain;
           
            
            Random gen = new Random();
            DateTime start = new DateTime(2022, 5, gen.Next(1, 15), 8, 0, 0);
            DateTime end = new DateTime(2022, 5, gen.Next(15, 30), 20, 0, 0);
            int interval = 6;

            MedicineDrug drug1 = new MedicineDrug("Lekadol" + gen.Next(100, 200), start, end, interval);
            MedicineDrug drug2 = new MedicineDrug("Lekadol" + gen.Next(100, 200), DateTime.Now, DateTime.Now.AddDays(7), 1);
            Drugs.Add(drug1);
            Drugs.Add(drug2);



        }

        private void addDrug(object sender, RoutedEventArgs e)
        {
            Random gen = new Random();
            DateTime start = new DateTime(2022, 5, gen.Next(1, 15), 8, 0, 0);
            DateTime end = new DateTime(2022, 5, gen.Next(15, 30), 20, 0, 0);
            int interval = 6;

            MedicineDrug drug1 = new MedicineDrug("Lekadol" + gen.Next(100, 200), start, end, interval);
            MedicineDrug drug2 = new MedicineDrug("Lekadol" + gen.Next(100, 200), DateTime.Now, DateTime.Now.AddDays(7), 1);
            Drugs.Add(drug1);
            Drugs.Add(drug2);
            //Notification = 
        }
    }
}
