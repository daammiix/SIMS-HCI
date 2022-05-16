using ClassDijagramV1._0.Controller;
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
    /// Interaction logic for RatingHospital.xaml
    /// </summary>
    public partial class RatingHospital : Page
    {
        private PatientMainWindow parent { get; set; }
        private RatingController _ratingController;
        public ObservableCollection<HospitalRating> HospitalRatings { get; set; }
        public RatingHospital(PatientMainWindow patientMain)
        {
            InitializeComponent();
            parent = patientMain;
            this.DataContext = this;

            App app = Application.Current as App;
            _ratingController = app.RatingController;
            HospitalRatings = _ratingController.GetAllHospitalRatings();
        }

        private void SendRatingHospitalResult(object sender, RoutedEventArgs e)
        {
            List<int> ocjene = new List<int>() { pitanje1.Value, pitanje2.Value, pitanje3.Value, pitanje4.Value };
            string comment = komentar.Text;

            HospitalRating hr = new HospitalRating(ocjene, ocjene.Average(), comment);

            _ratingController.AddRatingHospital(hr);

        }

        private void EverythingRated(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            if(pitanje1.Value != 0 && pitanje2.Value != 0 && pitanje2.Value != 0 && pitanje4.Value != 0)
            {
                ratingBtn.IsEnabled = true;
            }
        }
    }
}
