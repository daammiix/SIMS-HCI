using ClassDijagramV1._0.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for MedicineComponentsWindow.xaml
    /// </summary>
    public partial class MedicineComponentsWindow : Window
    {
        public BindingList<String> MedicineComponents { get; set; }
        public MedicineComponentsWindow(QuantifiedMedicine? quantifiedMedicine)
        {
            InitializeComponent();
            this.MedicineComponents = quantifiedMedicine.Medicines.MedicineComponents;
            Components.Clear();

            foreach (var component in MedicineComponents)
            {
                Components.AppendText(component + Environment.NewLine);
            }

        }

        private void CloseMedicineComponents_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
