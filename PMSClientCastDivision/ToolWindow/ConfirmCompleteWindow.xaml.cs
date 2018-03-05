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
using System.Windows.Shapes;

namespace PMSClient.ToolWindow
{
    /// <summary>
    /// ConfirmCompleteWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ConfirmCompleteWindow : Window
    {
        public ConfirmCompleteWindow()
        {
            InitializeComponent();
        }

        public ConfirmModel Model
        {
            get
            {
                ConfirmModel model = new ConfirmModel()
                {
                    MaterialItemLot = txtMaterialLot.Text,
                    Composition = txtComposition.Text,
                    PMINumber = txtPMINumber.Text,
                    Weight = double.Parse(txtWeight.Text),
                    ActualWeight = double.Parse(txtActualWeight.Text),
                    MeltingPoint = txtMeltingPoint.Text,
                    Remark=txtRemark.Text
                };
                return model;
            }
            set
            {
                txtMaterialLot.Text = value.MaterialItemLot;
                txtComposition.Text = value.Composition;
                txtPMINumber.Text = value.PMINumber;
                txtWeight.Text = value.Weight.ToString();
                txtActualWeight.Text = value.ActualWeight.ToString();
                txtMeltingPoint.Text = value.MeltingPoint;
                txtRemark.Text = value.Remark;
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnSure_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
