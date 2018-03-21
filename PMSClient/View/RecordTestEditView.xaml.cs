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
using System.IO;
using Microsoft.Win32;
using PMSClient.ToolWindow;

namespace PMSClient.View
{
    /// <summary>
    /// ProductEditView.xaml 的交互逻辑
    /// </summary>
    public partial class RecordTestEditView : UserControl
    {
        public RecordTestEditView()
        {
            InitializeComponent();
        }

        private void BtnCsv_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "CSV|*.csv";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            if (dialog.ShowDialog() == true)
            {
                string filename = dialog.FileName;
                if (File.Exists(filename))
                {
                    string result = File.ReadAllText(filename);
                    PMSMethods.SetTextBox(txtCompositionXRF, result.TrimEnd());
                }
            }
        }

        private void BtnResistance_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBox(txtResistance, "OutOfRange");
        }

        private void BtnAddPlate_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBox(txtRemark, "附有背板");
        }

        private void BtnSimulator_Click(object sender, RoutedEventArgs e)
        {
            CompositionSimulator simulator = new CompositionSimulator();
            simulator.FillIn += (s, args) =>
            {
                txtCompositionXRF.Text = args;
            };
            simulator.Show();
        }

        private void BtnCalculator_Click(object sender, RoutedEventArgs e)
        {
            DensityCalculation calculator = new DensityCalculation();
            calculator.TargetWeight = TargetWeight.Text;
            calculator.TargetDimension = TargetDimension.Text;
            calculator.Show();
        }
    }
}
