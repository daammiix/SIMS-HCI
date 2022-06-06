using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.ViewModel.SecretaryViewModels.AccountViewModels;
using ClassDijagramV1._0.ViewModel.SecretaryViewModels.MeetingsViewModels;
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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace ClassDijagramV1._0.Views.SecretaryView.MeetingsView
{
    /// <summary>
    /// Interaction logic for EditMeetingDialog.xaml
    /// </summary>
    public partial class EditMeetingDialog : Window
    {
        #region Fields

        private Point dragStartPoint;

        #endregion

        public EditMeetingDialog(MeetingViewModel meetingVM)
        {
            InitializeComponent();
            this.DataContext = new EditMeetingDialogViewModel(meetingVM);
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
                AccountViewModel account = (AccountViewModel)listView
                    .ItemContainerGenerator.ItemFromContainer(listViewItem);

                DataObject dragData = new DataObject("myFormat", account);
                DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Move);
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
                // uzmemo accountViewModel to nam je DataObject
                ClassDijagramV1._0.ViewModel.SecretaryViewModels.AccountViewModels.AccountViewModel accountViewModel = e.Data.GetData("myFormat") as AccountViewModel;
                // Napravimo meeting account view model jer nam je bitno da li je osoba obavezna stavimo required
                // na false na pocetku, svakako ga updatujemo preko dijaloga
                MeetingAccountViewModel meetingAccountViewModel = new MeetingAccountViewModel(accountViewModel, false);
                // Prikazemo dialog za odabir da li je osoba obavezna
                IsRequiredDialog isRequiredDialog = new IsRequiredDialog();
                // Stavimo mu data context na odgovarajuci viewModel kom prosledimo meetingAccount da bi ga dopunili
                isRequiredDialog.DataContext = new IsRequiredDialogViewModel(meetingAccountViewModel);
                isRequiredDialog.Owner = this;
                isRequiredDialog.ShowDialog();

                EditMeetingDialogViewModel editMeetingDialogViewModel = this.DataContext as EditMeetingDialogViewModel;

                // Dodamo account u listu accounta u meetingu
                editMeetingDialogViewModel.AccountsInMeeting.Add(meetingAccountViewModel);
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
                if (current is Visual || current is Visual3D)
                {
                    current = VisualTreeHelper.GetParent(current);
                }
                else
                {
                    // Ako upadnemo u Logical Land moramo da idemo uz LogicalTree
                    current = LogicalTreeHelper.GetParent(current);
                }

            } while (current != null);
            return null;
        }

        #endregion

    }
}
