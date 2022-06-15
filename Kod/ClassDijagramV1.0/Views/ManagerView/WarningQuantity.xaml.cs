using System.Windows;

namespace ClassDijagramV1._0.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for WarningQuantity.xaml
    /// </summary>
    public partial class WarningQuantity : Window
    {
        public WarningQuantity()
        {
            InitializeComponent();
        }

        private void WarningOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
