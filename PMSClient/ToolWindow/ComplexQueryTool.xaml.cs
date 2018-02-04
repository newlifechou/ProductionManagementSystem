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
    /// ComplextQueryTool.xaml 的交互逻辑
    /// </summary>
    public partial class ComplexQueryTool : Window
    {
        public ComplexQueryTool()
        {
            InitializeComponent();
            SetAll();
        }

        private void SetAll()
        {
            this.Left = SystemParameters.WorkArea.Left;
            this.Top = SystemParameters.WorkArea.Top + 100;
            //TODO:设置好权限
        }

        private void BtnNavigation_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoTo(PMSViews.Navigation);
        }

        private void BtnOrder_Click(object sender, RoutedEventArgs e)
        {
            string pminumber = TxtPMINumber.Text.Trim();
            PMSHelper.ViewModels.Order.SetSearch(pminumber);
            NavigationService.GoTo(PMSViews.Order);
        }

        private void BtnMaterialOrderItem_Click(object sender, RoutedEventArgs e)
        {
            string pminumber = TxtPMINumber.Text.Trim();
            PMSHelper.ViewModels.MaterialOrderItemList.SetSearch(pminumber);
            NavigationService.GoTo(PMSViews.MaterialOrderItemList);
        }
    }
}
