using System.Windows;

namespace ClassDijagramV1._0.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for Warning.xaml
    /// </summary>
    public partial class Warning : Window
    {
        public Warning()
        {
            InitializeComponent();
        }

        private void WarningOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
