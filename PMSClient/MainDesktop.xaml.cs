﻿using GalaSoft.MvvmLight.Messaging;
using System;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PMSClient.ViewModel;
using PMSClient.ViewForDesktop;
using PMSClient.MainService;
using PMSClient.Tool;
using System.Timers;
using fm = System.Windows.Forms;
using System.Windows.Threading;

namespace PMSClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainDesktop : Window
    {
        private DesktopViewLocator _views;
        private ToolViewLocator _toolviews;
        private DispatcherTimer _timerHeartBeat;
        private DispatcherTimer _timerNotice;
        private fm.NotifyIcon _notifyIcon;
        public MainDesktop()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region 标题处理
            try
            {
                var titleName = (App.Current as App).FindResource("AppNameDesktop").ToString();
                var versonName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                Title = $"{titleName}-{versonName}";
            }
            catch (Exception)
            {

            }
            #endregion

            #region 初始化变量并注册mvvmlight消息
            _views = PMSHelper.DesktopViews;
            _toolviews = PMSHelper.ToolViews;

            Messenger.Default.Register<PMSViews>(this, MainNavigationToken.Navigate, ActionNavigation);
            Messenger.Default.Register<string>(this, MainNavigationToken.StatusMessage, ActionStatusMessage);
            #endregion


            //导航到首页
            NavigateTo(_views.LogIn);

            #region 设置主定时器
            //设置主定时器
            _timerHeartBeat = new DispatcherTimer();
            _timerHeartBeat.Interval = new TimeSpan(0, 0, 5);
            _timerHeartBeat.Tick += _timer_Tick; ;
            _timerHeartBeat.Start();

            _timerNotice = new DispatcherTimer();
            _timerNotice.Interval = new TimeSpan(0, 0, 10);
            _timerNotice.Tick += _timerNotice_Tick;
            _timerNotice.Start();
            #endregion

            #region 托盘部分
            InitializeTray();
            #endregion
        }

        private void _timerNotice_Tick(object sender, EventArgs e)
        {
            //PMSNotice.HasNewDelivery();
        }

        #region 托盘通知
        private void InitializeTray()
        {
            _notifyIcon = new fm.NotifyIcon();
            _notifyIcon.Text = "PMS生产管理系统";
            _notifyIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath);
            _notifyIcon.Visible = true;

            fm.ContextMenu menu = new fm.ContextMenu();
            fm.MenuItem item = new fm.MenuItem();
            item.Text = "关闭";
            item.Click += (s, e) =>
            {
                this.Close();
            };
            menu.MenuItems.Add(item);
            _notifyIcon.ContextMenu = menu;
            _notifyIcon.Visible = true;
            //SetNotifyIcon("欢迎", "欢迎使用PMS生产管理系统\r\n请在左上方登录使用", 2000);
        }
        public void SetNotifyIcon(string title, string message, int showtime)
        {
            if (_notifyIcon != null)
            {
                _notifyIcon.Visible = true;
                _notifyIcon.BalloonTipTitle = title;
                _notifyIcon.BalloonTipText = message;
                _notifyIcon.ShowBalloonTip(showtime);
            }
        }
        private void RemoveNofiyIcon()
        {
            if (_notifyIcon != null)
            {
                _notifyIcon.Visible = false;
                _notifyIcon.Dispose();
                _notifyIcon = null;
            }
        }
        #endregion

        /// <summary>
        /// 主定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _timer_Tick(object sender, EventArgs e)
        {
            try
            {
                using (var heartbeat = new PMSClient.HeartBeatService.HeartBeatSeriveClient())
                {
                    if (heartbeat.Beat() == "ok")
                    {
                        //this.Dispatcher.Invoke(() =>
                        //{
                        //    txtHeartBeat.Text = "服务器通信正常";
                        //});
                        txtHeartBeat.Text = "服务器通信正常";
                    }
                }

            }
            catch (Exception ex)
            {
                this.Dispatcher.Invoke(() =>
                {
                    txtHeartBeat.Text = ex.Message;
                });
                PMSHelper.CurrentLog.Error(ex);
            }
        }
        /// <summary>
        /// 导航区域
        /// </summary>
        private void ActionNavigation(PMSViews token)
        {
            PMSHelper.CurrentLog.Log(token.ToString());
            switch (token)
            {
                case PMSViews.LogIn:
                    RefreshLogInformation();
                    NavigateTo(_views.LogIn);
                    break;
                case PMSViews.Navigation:
                    RefreshLogInformation();
                    NavigateTo(_views.Navigation);
                    break;
                case PMSViews.Order:
                    NavigateTo(_views.Order);
                    break;
                case PMSViews.OrderEdit:
                    NavigateTo(_views.OrderEdit);
                    break;
                case PMSViews.MissonSelect:
                    NavigateTo(_views.MissonSelect);
                    break;
                case PMSViews.OrderCheck:
                    NavigateTo(_views.OrderCheck);
                    break;
                case PMSViews.OrderCheckEdit:
                    NavigateTo(_views.OrderCheckEdit);
                    break;
                case PMSViews.MaterialNeed:
                    NavigateTo(_views.MaterialNeed);
                    break;
                case PMSViews.MaterialNeedEdit:
                    NavigateTo(_views.MaterialNeedEdit);
                    break;
                case PMSViews.MaterialNeedSelect:
                    NavigateTo(_views.MaterialNeedSelect);
                    break;
                case PMSViews.MaterialOrder:
                    NavigateTo(_views.MaterialOrder);
                    break;
                case PMSViews.MaterialOrderEdit:
                    NavigateTo(_views.MaterialOrderEdit);
                    break;
                case PMSViews.MaterialOrderItemEdit:
                    NavigateTo(_views.MaterialOrderItemEdit);
                    break;
                case PMSViews.MaterialOrderItemSelect:
                    NavigateTo(_views.MaterialOrderItemSelect);
                    break;
                case PMSViews.MaterialInventoryIn:
                    NavigateTo(_views.MaterialInventoryIn);
                    break;
                case PMSViews.MaterialInventoryInEdit:
                    NavigateTo(_views.MaterialInventoryInEdit);
                    break;
                case PMSViews.MaterialInventoryInSelect:
                    NavigateTo(_views.MaterialInventoryInSelect);
                    break;
                case PMSViews.MaterialInventoryOut:
                    NavigateTo(_views.MaterialInventoryOut);
                    break;
                case PMSViews.MaterialInventoryOutEdit:
                    NavigateTo(_views.MaterialInventoryOutEdit);
                    break;
                case PMSViews.Misson:
                    NavigateTo(_views.Misson);
                    break;
                case PMSViews.Plan:
                    NavigateTo(_views.Plan);
                    break;
                case PMSViews.PlanEdit:
                    NavigateTo(_views.PlanEdit);
                    break;
                case PMSViews.PlanSelect:
                    NavigateTo(_views.PlanSelect);
                    break;
                case PMSViews.PlanSearch:
                    NavigateTo(_views.PlanSearch);
                    break;
                case PMSViews.RecordMilling:
                    NavigateTo(_views.RecordMilling);
                    break;
                case PMSViews.RecordMillingEdit:
                    NavigateTo(_views.RecordMillingEdit);
                    break;
                case PMSViews.RecordVHP:
                    NavigateTo(_views.RecordVHP);
                    break;
                case PMSViews.RecordVHPQuickEdit:
                    NavigateTo(_views.RecordVHPQuickEdit);
                    break;
                case PMSViews.RecordDeMold:
                    NavigateTo(_views.RecordDeMold);
                    break;
                case PMSViews.RecordDeMoldEdit:
                    NavigateTo(_views.RecordDeMoldEdit);
                    break;
                case PMSViews.RecordMachine:
                    NavigateTo(_views.RecordMachine);
                    break;
                case PMSViews.RecordMachineEdit:
                    NavigateTo(_views.RecordMachineEdit);
                    break;
                case PMSViews.RecordMachineSelect:
                    NavigateTo(_views.RecordMachineSelect);
                    break;
                case PMSViews.RecordTest:
                    NavigateTo(_views.RecordTest);
                    break;
                case PMSViews.RecordTestEdit:
                    NavigateTo(_views.RecordTestEdit);
                    break;
                case PMSViews.RecordTestSelect:
                    NavigateTo(_views.RecordTestSelect);
                    break;
                case PMSViews.RecordTestDoc:
                    NavigateTo(_views.RecordTestDoc);
                    break;
                case PMSViews.RecordBonding:
                    NavigateTo(_views.RecordBonding);
                    break;
                case PMSViews.RecordBondingEdit:
                    NavigateTo(_views.RecordBondingEdit);
                    break;
                case PMSViews.Delivery:
                    NavigateTo(_views.Delivery);
                    break;
                case PMSViews.DeliveryEdit:
                    NavigateTo(_views.DeliveryEdit);
                    break;
                case PMSViews.DeliveryItemEdit:
                    NavigateTo(_views.DeliveryItemEdit);
                    break;
                case PMSViews.DeliveryItemList:
                    NavigateTo(_views.DeliveryItemList);
                    break;
                case PMSViews.Product:
                    NavigateTo(_views.Product);
                    break;
                case PMSViews.ProductEdit:
                    NavigateTo(_views.ProductEdit);
                    break;
                case PMSViews.ProductSelect:
                    NavigateTo(_views.ProductSelect);
                    break;
                case PMSViews.MaterialNeedCalcuationTool:
                    NavigateTo(_toolviews.MaterialNeedCalculation);
                    break;
                case PMSViews.LabelOutPut:
                    NavigateTo(_toolviews.LabelOutPut);
                    break;
                default:
                    break;
            }
        }

        #region 主窗口操作和事件处理
        private void NavigateTo(UserControl view)
        {
            if (view != null)
            {
                mainArea.Content = view;
            }
        }

        private void ActionStatusMessage(string obj)
        {
            if (!string.IsNullOrEmpty(obj))
            {
                txtStateMessage.Text = obj;
            }
            else
            {
                txtStateMessage.Text = "状态栏";
            }
        }
        /// <summary>
        /// 必须放在Closed事件中，否则退出过程中取消就出问题了
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            RemoveNofiyIcon();
            Messenger.Default.Unregister(this);
            base.OnClosed(e);
        }
        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            var result = PMSDialogService.ShowYesNo("请问", "确定要退出吗？");
            if (!result)
            {
                //this.WindowState = WindowState.Minimized;
                e.Cancel = true;
                return;
            }
        }
        #endregion
        #region 公共方法

        public void LogOut()
        {
            if (PMSHelper.CurrentSession.CurrentUser != null)
            {
                PMSHelper.CurrentSession.LogOut();
                _views.LogIn.ClearLog();
                SetNotifyIcon("注销成功", "您的账户已经注销成功", 3000);
                NavigationService.GoTo(PMSViews.LogIn);
            }
        }
        public void RefreshLogInformation()
        {
            var _logInformation = PMSHelper.CurrentSession;
            if (_logInformation.CurrentUser != null)
            {
                txtCurrentUserName.Text = $"当前用户:[{ _logInformation.CurrentUser.RealName}] 角色:[{_logInformation.CurrentUserRole.GroupName}]";
            }
            else
            {
                txtCurrentUserName.Text = "未登录";
            }
        }
        #endregion
    }
}
