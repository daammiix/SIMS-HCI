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
using System.Windows.Shapes;

namespace ClassDijagramV1._0.Views
{
    /// <summary>
    /// Interaction logic for RenovatingAndChangingPurpose.xaml
    /// </summary>
    public partial class RenovatingAndChangingPurpose : Window
    {
        public RenovatingAndChangingPurpose()
        {
            InitializeComponent();
        }

        private void SaveRenovating_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void QuitRenovating_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Calendar_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
        {
            Calendar calObj = sender as Calendar;
            calObj.DisplayMode = CalendarMode.Month;
        }
    }
}
