﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PMSEOrder.Model;
using PMSEOrder.Service;

namespace PMSEOrder
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowVM();
        }

        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            Order model =(Order)e.Row.DataContext;
            if (model != null)
            {
                switch (model.OrderState)
                {
                    case "Deleted":
                        e.Row.Background = this.FindResource("CancelledBrush") as SolidColorBrush;
                        break;
                    case "UnFinished":
                        e.Row.Background = this.FindResource("UnCheckedBrush") as SolidColorBrush;
                        break;
                    case "UnSend":
                        e.Row.Background = this.FindResource("UnCompletedBrush") as SolidColorBrush;
                        break;
                    case "Sent":
                        e.Row.Background = this.FindResource("CheckedBrush") as SolidColorBrush;
                        break;
                    default:
                        break;
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(!XSHelper.XS.MessageBox.ShowYesNo("Do you want to quit this program?"))
            {
                e.Cancel = true;
                return;
            }
            BackupService.BackUp();
        }

        private void BtnSpecialRequirement_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn != null)
            {
                var win = new WPFControls.KeyValueTestResultReadOnlyE();
                win.Width = 600;
                win.KeyStrings = btn.Content.ToString();
                win.ShowDialog();
            }
        }
    }
}
