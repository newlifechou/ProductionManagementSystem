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
using GalaSoft.MvvmLight.Messaging;
using PMSEOrder.Helper;

namespace PMSEOrder
{
    /// <summary>
    /// OrderEditView.xaml 的交互逻辑
    /// </summary>
    public partial class OrderEditView : Window
    {
        public OrderEditView()
        {
            InitializeComponent();
            this.Height = 800;
            var vm = new OrderEditVM();
            this.DataContext = vm;
            Messenger.Default.Register<NotificationMessage>(this, "MSG", ActionDo);
        }

        private void ActionDo(NotificationMessage obj)
        {
            if (obj.Notification == "CloseEditWindow")
            {
                this.Close();
            }

        }

        private void BtnShipToTCB_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBox(TxtShipTo, "TCB");
        }

        private void BtnShipToCustomer_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBox(TxtShipTo, "Customer");

        }

        private void BtnShipToLeon_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBox(TxtShipTo, "Leon");

        }

        private void BtnSampleForAnlysisNone_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBox(TxtSampleForAnlysis, "");

        }

        private void BtnSampleForAnlysisDefault_Click(object sender, RoutedEventArgs e)
        {
            var sample = new SampleWindow();
            if (sample.ShowDialog() == true)
            {
                string sample_str = sample.SampleResult;
                PMSMethods.SetTextBoxAppend(TxtSampleForAnlysis, $"{sample_str};");
            }
        }

        private void BtnSampleNeedDefault_Click(object sender, RoutedEventArgs e)
        {
            var sample = new SampleWindow();
            if (sample.ShowDialog() == true)
            {
                string sample_str = sample.SampleResult;
                PMSMethods.SetTextBoxAppend(TxtSampleNeed, $"{sample_str};");
            }
        }

        private void BtnSampleNeedNone_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBox(TxtSampleNeed, "");

        }

        private void BtnAcceptDefects_Click(object sender, RoutedEventArgs e)
        {
            string s = @"ρ>3g/cm3";
            PMSMethods.SetTextBox(TxtAcceptDefects, s);
        }

        private void BtnBasicRequirement1_Click(object sender, RoutedEventArgs e)
        {
            string s = @"TD±0.1 TH±0.1 Ra<1.6 FR=2";
            PMSMethods.SetTextBox(TxtDimensionDetails, s);
        }

        private void BtnBasicRequirement2_Click(object sender, RoutedEventArgs e)
        {
            string s = @"TD-0+0.1 TH-0+0.1 Ra<1.6 FR=2";
            PMSMethods.SetTextBox(TxtDimensionDetails, s);
        }

        private void BtnBasicRequirement3_Click(object sender, RoutedEventArgs e)
        {
            string s = @"TD-0.1+0 TH-0.1+0 Ra<1.6 FR=2";
            PMSMethods.SetTextBox(TxtDimensionDetails, s);
        }
        private void BtnPurity1_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBox(TxtPurity, "99.990%");
        }

        private void BtnPurity2_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBox(TxtPurity, "99.995%");

        }

        private void BtnPurity3_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBox(TxtPurity, "99.999%");

        }

        private void BtnSpecialRequirement_Click(object sender, RoutedEventArgs e)
        {
            SetKeyValue(TxtSpecialRequirement);
        }

        private void SetKeyValue(TextBox textBox)
        {
            if (textBox == null) return;
            var dialog = new WPFControls.KeyValueTestResultE();
            dialog.KeyStrings = textBox.Text.Trim();
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                PMSMethods.SetTextBox(textBox, dialog.KeyStrings);
            }
        }

        private void BtnDimension_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBox(TxtDimension, "mm OD x mm thickness");
        }

        private void BtnBondingRequirement_Click(object sender, RoutedEventArgs e)
        {
            var bonding = new BondingWindow();
            if (bonding.ShowDialog() == true)
            {
                string bonding_str = bonding.BondingResult;
                PMSMethods.SetTextBox(TxtBondingRequirement, $"{bonding_str}");
            }
        }

        private void BtnBondingRequirementNone_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBox(TxtBondingRequirement, "");

        }

        private void SPSpecialRequirement_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)e.OriginalSource;
            switch (btn.Name)
            {
                case "BtnSpecial1":
                    PMSMethods.SetTextBoxAppend(TxtSpecialRequirement, "further polishing at Opticraft=yes;");
                    break;
                case "BtnSpecial2":
                    PMSMethods.SetTextBoxAppend(TxtSpecialRequirement, "final thickness to be polished at Opticraft=0mm;");
                    break;
                case "BtnSpecial3":
                    PMSMethods.SetTextBoxAppend(TxtSpecialRequirement, "Laser Mark=both side;");
                    break;
                case "BtnSpecial4":
                    PMSMethods.SetTextBoxAppend(TxtSpecialRequirement, "Part Number=value;");
                    break;
                default:
                    PMSMethods.SetTextBoxAppend(TxtSpecialRequirement, "key=value;");
                    break;
            }
        }
    }
}
