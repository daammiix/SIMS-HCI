using ClassDijagramV1._0.ViewModel.SecretaryViewModels.EquipmentViewModels;
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

namespace ClassDijagramV1._0.Views.SecretaryView.EquipmentView
{
    /// <summary>
    /// Interaction logic for MakeOrderDailog.xaml
    /// </summary>
    public partial class MakeOrderDialog : Window
    {
        private Point dragStartPoint = new Point();

        public MakeOrderDialog()
        {
            InitializeComponent();
        }

        #region Application State Event Handlers

        /// <summary>
        /// Minimizes application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Maximizes application or change it back to normal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WindowStateButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        /// <summary>
        /// Closes applications main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this?.Close();
        }


        private void Header_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        #endregion

        #region Event Handlers

        private void ListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            dragStartPoint = e.GetPosition(null);
        }

        private void ListView_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = mousePos - dragStartPoint;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                // Uzmemo dragovan listView item
                ListView listView = sender as ListView;
                ListViewItem listViewItem = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

                if (listViewItem == null) return;

                // Uzmemo objekat iza list viewa
                EquipmentViewModel equipment = (EquipmentViewModel)listView
                    .ItemContainerGenerator.ItemFromContainer(listViewItem);

                DataObject dragData = new DataObject("myFormat", equipment);
                DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Copy);
            }
        }

        private void ListView_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat"))
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void ListView_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                // uzmemo equipmentViewModel to nam je DataObject
                EquipmentViewModel equipmentViewModel = e.Data.GetData("myFormat") as EquipmentViewModel;
                // Stavimo quantity na 0 jer cemo da pokazemo prozor u kom cemo da dopunimo quantity
                PurchaseOrderEquipmentViewModel purchaseOrderEquipmentViewModel =
                    new PurchaseOrderEquipmentViewModel(new Model.PurchaseOrderEquipment(equipmentViewModel.Id, 0));
                // Prikazemo dialog za unos kolicine
                InsertQuantityDialog insertQuantityDialog = new InsertQuantityDialog();
                // Stavimo mu data context na odgovarajuci viewModel kom prosledimo equipment da bi ga dopunili
                insertQuantityDialog.DataContext = new InsertQuantityDialogViewModel(purchaseOrderEquipmentViewModel);
                insertQuantityDialog.Owner = this;
                insertQuantityDialog.ShowDialog();

                // Ako vec imamo ovaj equipment u orderu samo povecamo quantity
                MakeOrderDialogViewModel viewModel = this.DataContext as MakeOrderDialogViewModel;
                PurchaseOrderEquipmentViewModel? equip = viewModel.EquipmentInOrder
                    .Where(equipment => equipment.Id.Equals(purchaseOrderEquipmentViewModel.Id)).ElementAtOrDefault(0);
                if (equip != null)
                {
                    // Povecamo quantity jer vec postoji
                    equip.Quantity += purchaseOrderEquipmentViewModel.Quantity;
                }
                else
                {
                    // Dodamo equipment u order posto ne postoji
                    viewModel.EquipmentInOrder.Add(purchaseOrderEquipmentViewModel);
                }
            }
        }

        #endregion

        #region Helpers

        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            } while (current != null);
            return null;
        }

        #endregion

    }
}
