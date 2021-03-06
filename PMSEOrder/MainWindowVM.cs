﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSEOrder.Model;
using GalaSoft.MvvmLight.Messaging;
using XSHelper;
using Newtonsoft.Json;
using PMSEOrder.Service;
using System.IO;
using PMSEOrder.Service.Excel;
using PMSEOrder.PMS;
using PMSEOrder.Dilaog;

namespace PMSEOrder
{
    public class MainWindowVM : ViewModelBase
    {
        public MainWindowVM()
        {
            Initialize();
            LoadData();
        }

        public void Initialize()
        {
            Orders = new ObservableCollection<Order>();
            searchCustomer = searchPO = searchComposition = "";
            hideDeleted = true;

            New = new RelayCommand(ActionNew);
            Edit = new RelayCommand<Order>(ActionEdit);
            Duplicate = new RelayCommand<Order>(ActionDuplicate);
            EOrder = new RelayCommand<Order>(ActionEOrder);
            Txt = new RelayCommand<Order>(ActionTxt);
            AllEOrder = new RelayCommand(ActionAllEOrder);
            AllTxt = new RelayCommand(ActionAllTxt);
            Send = new RelayCommand<Order>(ActionSend);
            Search = new RelayCommand(ActionSearch);
            Backup = new RelayCommand(ActionBackup);
            Excel = new RelayCommand(ActionExcel);
            PMSRefresh = new RelayCommand(ActionPMSRefresh);
            SelectionChanged = new RelayCommand<Order>(ActionSelectionChanged);
            Setting = new RelayCommand(ActionSetting);
            Import = new RelayCommand(ActionImport);
            CheckOnline = new RelayCommand(ActionCheckOnline);
            DBFolder = new RelayCommand(ActionDBFolder);

            Messenger.Default.Register<NotificationMessage>(this, "MSG", ActionDo);

        }

        private void ActionDBFolder()
        {
            try
            {
                string dbpath = @"DB\";
                System.Diagnostics.Process.Start(dbpath);
            }
            catch (Exception)
            {

            }
        }

        private void ActionCheckOnline()
        {

            if (XS.MessageBox.ShowYesNo("Going to Check the order state from pms database?\r\nonline"))
            {
                try
                {
                    using (var s = new EOrderServiceClient())
                    {
                        IEnumerable<Order> filterOrder = new List<Order>();
                        if (XS.MessageBox.ShowYesNo("Check UnSend Orders or Check All Orders?\r\n Yes=UnSend,No=All"))
                        {
                            filterOrder = Orders.Where(i => i.OrderState == OrderState.UnSend.ToString());
                        }
                        else
                        {
                            filterOrder = Orders;
                        }

                        List<OrderCheckState> orderCheckStates = new List<OrderCheckState>();
                        foreach (var item in filterOrder)
                        {
                            var checkResult = new OrderCheckState();
                            checkResult.CurrentOrder = item;
                            checkResult.ExistInPMS = s.CheckEOrderGuid(item.GUIDID.ToString());
                            orderCheckStates.Add(checkResult);
                        }
                        //这里显示一个窗口
                        var win = new OrderCheckStateWindow();
                        win.SetDataSource(orderCheckStates);
                        win.Show();




                    }
                }
                catch (Exception ex)
                {
                    XS.MessageBox.ShowError(ex.Message);
                }
            }

        }

        private void ActionImport()
        {
            try
            {
                var dialog = XS.Dialog.ShowOpenDialog("please select the order json file", "json file(*.json)|*.json");
                if (dialog.HasSelected)
                {
                    string json_str = XS.File.ReadText(dialog.SelectPath);
                    if (ImportService.SaveToDB(json_str))
                    {
                        LoadData();
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void ActionSetting()
        {
            var setting = new SettingWindow();
            setting.ShowDialog();
        }

        private void ActionSelectionChanged(Order obj)
        {
            if (obj == null) return;
            CurrentOrder = obj;

        }

        private void ActionAllTxt()
        {
            if (!XS.MessageBox.ShowYesNo("Will get all [UnSend] order text? Y=continue,N=cancel"))
            {
                return;
            }
            var filterOrder = Orders.Where(i => i.OrderState == OrderState.UnSend.ToString());
            StringBuilder sb = new StringBuilder();
            foreach (var item in filterOrder)
            {
                sb.AppendLine("-----------------------------------");
                sb.AppendLine(TextService.GetOrderText(item));

            }
            var win = new TextWindow();
            win.MainText.Text = sb.ToString();
            win.Show();
        }

        private void ActionTxt(Order obj)
        {
            if (obj == null) return;
            if (XS.MessageBox.ShowYesNo("Will get the order text? Y=continue,N=cancel"))
            {
                var win = new TextWindow();
                win.MainText.Text = TextService.GetOrderText(obj);
                win.Show();
            }
        }

        private void ActionDo(NotificationMessage obj)
        {
            if (obj.Notification == "RefreshMainWindow")
            {
                LoadData();
            }
        }

        private void ActionPMSRefresh()
        {
            //从PMS获取数据来设置状态
            throw new NotImplementedException();
        }

        private void ActionSend(Order obj)
        {
            if (obj == null) return;
            //快速设置为已发送
            if (XS.MessageBox.ShowYesNo("Do you want to set its [OrderState] to Sent?"))
            {
                obj.OrderState = "Sent";
                var s = new DataService();
                s.UpdateOrder(obj);
                LoadData();
            }
        }

        private void ActionExcel()
        {
            //利用NPIO导出所有数据的Excel表格
            if (XS.MessageBox.ShowYesNo("Do you want to output all data to excel file?"))
            {
                try
                {
                    var excel = new ExcelOutputOrder();
                    excel.Intialize("Raw Order", "Data");
                    excel.Output();
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void ActionBackup()
        {
            //复制数据库到别的地方
            if (XS.MessageBox.ShowYesNo("Do you want to backup the local data?"))
            {
                try
                {
                    BackupService.BackUp();
                    XS.MessageBox.ShowInfo("Backup to AutoBackup Folder");
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private void ActionSearch()
        {
            LoadData();
        }

        private void ActionAllEOrder()
        {
            if (!XS.MessageBox.ShowYesNo("Will get all [UnSend] E-Order files? Y=continue,N=cancel"))
            {
                return;
            }
            //批量生成未完成项目的json文件，并弹出文本窗口供复制到Email
            var dialog = XS.Dialog.ShowFolderBrowserDialog("Please select a folder");
            if (dialog.HasSelected)
            {
                var folderPath = dialog.SelectPath;

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var filterOrder = Orders.Where(i => i.OrderState == OrderState.UnSend.ToString());
                foreach (var item in filterOrder)
                {
                    var filedefaultname = JsonService.GetJsonFileName(item);
                    var filepath = Path.Combine(folderPath, filedefaultname + ".json");
                    JsonService.SaveJsonToFile(item, filepath);
                }
                XS.MessageBox.ShowInfo($"All processed count= {filterOrder.Count()}");
            }




        }

        private void ActionEOrder(Order obj)
        {
            if (obj == null) return;
            var filedefaultname = JsonService.GetJsonFileName(obj);
            var result = XS.Dialog.ShowSaveDialog(XS.File.GetDesktopPath(), "json file|*.json", filedefaultname);
            if (result.HasSelected)
            {
                var filepath = result.SelectPath;
                JsonService.SaveJsonToFile(obj, filepath);
                XS.MessageBox.ShowInfo("Process Done");
            }
        }

        private void ActionDuplicate(Order obj)
        {
            if (obj == null) return;
            var edit = new OrderEditView();
            ((OrderEditVM)edit.DataContext).SetDuplicate(obj);
            edit.ShowDialog();
        }

        private void ActionEdit(Order obj)
        {
            if (obj == null) return;

            if (XS.MessageBox.ShowYesNo("Do you want to edit this order?"))
            {
                var edit = new OrderEditView();
                ((OrderEditVM)edit.DataContext).SetEdit(obj);
                edit.ShowDialog();
            }
        }

        private void ActionNew()
        {
            var edit = new OrderEditView();
            ((OrderEditVM)edit.DataContext).SetNew();
            edit.ShowDialog();
        }

        private void LoadData()
        {
            var s = new Service.DataService();
            var orders = s.GetAllOrder();
            var filter_orders = orders.Where(i =>
                                            i.CustomerName.ToLower().Contains(SearchCustomer.ToLower())
                                            && i.Composition.ToLower().Contains(SearchComposition.ToLower())
                                            && i.PO.ToLower().Contains(SearchPO.ToLower()))
                                      .OrderByDescending(i => i.CreateTime);
            Orders.Clear();
            if (HideDeleted)
            {
                filter_orders.Where(i => i.OrderState != "Deleted").ToList().ForEach(i => Orders.Add(i));
            }
            else
            {
                filter_orders.ToList().ForEach(i => Orders.Add(i));
            }
            CurrentOrder = orders.FirstOrDefault();
        }

        private string searchCustomer;
        public string SearchCustomer
        {
            get
            {
                return searchCustomer;
            }
            set
            {
                searchCustomer = value;
                RaisePropertyChanged(nameof(SearchCustomer));
            }
        }

        private string searchComposition;
        public string SearchComposition
        {
            get
            {
                return searchComposition;
            }
            set
            {
                searchComposition = value;
                RaisePropertyChanged(nameof(SearchComposition));
            }
        }

        private string searchPO;
        public string SearchPO
        {
            get
            {
                return searchPO;
            }
            set
            {
                searchPO = value;
                RaisePropertyChanged(nameof(SearchPO));
            }
        }

        private Order currentOrder;
        public Order CurrentOrder
        {
            get
            {
                return currentOrder;
            }
            set
            {
                currentOrder = value;
                RaisePropertyChanged(nameof(currentOrder));
            }
        }

        private bool hideDeleted;
        public bool HideDeleted
        {
            get
            {
                return hideDeleted;
            }
            set
            {
                hideDeleted = value;
                RaisePropertyChanged(nameof(HideDeleted));
            }
        }
        public ObservableCollection<Order> Orders { get; set; }
        public RelayCommand New { get; set; }
        public RelayCommand<Order> Edit { get; set; }
        public RelayCommand<Order> Duplicate { get; set; }
        public RelayCommand<Order> EOrder { get; set; }
        public RelayCommand<Order> Txt { get; set; }
        public RelayCommand<Order> Send { get; set; }

        public RelayCommand<Order> SelectionChanged { get; set; }



        public RelayCommand CheckOnline { get; set; }
        public RelayCommand AllEOrder { get; set; }
        public RelayCommand AllTxt { get; set; }
        public RelayCommand Search { get; set; }
        public RelayCommand Backup { get; set; }
        public RelayCommand Excel { get; set; }
        public RelayCommand PMSRefresh { get; set; }
        public RelayCommand Setting { get; set; }
        public RelayCommand Import { get; set; }
        public RelayCommand DBFolder { get; set; }
    }
}
