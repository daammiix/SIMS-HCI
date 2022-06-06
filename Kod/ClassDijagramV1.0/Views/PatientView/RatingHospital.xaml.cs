using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ClassDijagramV1._0.Views.PatientView
{
    /// <summary>
    /// Interaction logic for RatingHospital.xaml
    /// </summary>
    public partial class RatingHospital : Page
    {
        #region Fields
        private PatientMainWindow parent { get; set; }
        private RatingController _ratingController;
        #endregion

        public RatingHospital(PatientMainWindow patientMain)
        {
            InitializeComponent();
            parent = patientMain;
            this.DataContext = this;

            App app = Application.Current as App;
            _ratingController = app.RatingController;
        }

        private void SendRatingHospitalResult(object sender, RoutedEventArgs e)
        {
            List<int> ocjene = new List<int>() { pitanje1.Value, pitanje2.Value, pitanje3.Value, pitanje4.Value };
            string comment = komentar.Text;
            HospitalRating hr = new HospitalRating(ocjene, ocjene.Average(), comment);
            _ratingController.AddRatingHospital(hr);

            parent.startWindow.Content = new PatientMainPage(parent);

        }

        private void EverythingRated(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            if (pitanje1.Value != 0 && pitanje2.Value != 0 && pitanje2.Value != 0 && pitanje4.Value != 0)
            {
                ratingBtn.IsEnabled = true;
            }
        }
    }
}
