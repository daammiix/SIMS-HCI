﻿using ClassDijagramV1._0.Dialog;
using Controller;
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
using System.Windows.Shapes;

namespace ClassDijagramV1._0.Views.PatientView
{
    /// <summary>
    /// Interaction logic for PatientMainWindow.xaml
    /// </summary>
    public partial class PatientMainWindow : Window
    {
        
        public PatientMainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            startWindow.Content = new PatientMainPage(this);
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            var a = new MainWindow();
            a.Show();
            Window.GetWindow(this).Close();
            
            
        }
    }
}