﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NotificationClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        #region Properties
        private double startPosition;
        public double StartPosition
        {
            get { return startPosition; }
            set { startPosition = value; RaisePropertyChanged(nameof(StartPosition)); }
        }

        private double endPosition;
        public double EndPosition
        {
            get { return endPosition; }
            set { endPosition = value; RaisePropertyChanged(nameof(EndPosition)); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            string msg = "会议通知：2016-12-19 13:30 在新会议室举行上一批产品问题解决方案相关的会议 参加人员:习近平 胡锦涛 江泽民 李鹏 邓小平";
            SetAndStart(msg);
        }

        private void SetAndStart(string content)
        {
            txtInformation.Text = content;

            StartPosition = window.ActualWidth;
            EndPosition = txtInformation.ActualWidth*-1;

            var storyboard = (Storyboard)this.FindResource("InformationScrollAnimation");
            storyboard.Begin();
        }

        private void window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            StartPosition = window.ActualWidth;
            EndPosition = txtInformation.ActualWidth * -1;
        }
    }
}
