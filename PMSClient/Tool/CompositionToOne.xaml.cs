﻿using PMSClient.Tool.Models;
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

namespace PMSClient.Tool
{
    /// <summary>
    /// CompositionToOne.xaml 的交互逻辑
    /// </summary>
    public partial class CompositionToOne : Window
    {
        public CompositionToOne()
        {
            InitializeComponent();
            Elements = new ObservableCollection<Element>();
            for (int i = 0; i < 10; i++)
            {
                Element temp = new Element();
                temp.Number = i + 1;
                temp.Name = "无";
                temp.At = 0;
                Elements.Add(temp);
            }


            this.DataContext = this;
        }

        public ObservableCollection<Element> Elements { get; set; }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            double sumAt = Elements.Sum(i => i.At);
            StringBuilder sb = new StringBuilder();
            foreach (var item in Elements)
            {
                if (!item.Name.Contains("无") || string.IsNullOrEmpty(item.Name))
                {
                    sb.Append(item.Name);
                    double tmp = item.At / sumAt * 100;
                    sb.Append(tmp.ToString("F2"));
                }
            }
            txtResult.Text = sb.ToString();
        }
    }
}
