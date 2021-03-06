﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSCommon;
using PMSClient.MainService;
using System.Collections.ObjectModel;
using AutoMapper;
using System.Windows;
using PMSClient;
using Novacode;
using PMSClient.ReportsHelper;

namespace PMSClient.ViewModel
{
    public class MaterialOrderVM : BaseViewModelPage
    {
        public MaterialOrderVM()
        {
            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
        }
        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }
        public void RefreshDataItem()
        {
            ActionSelectionChanged(CurrentSelectItem);
        }

        private void InitializeProperties()
        {
            searchOrderPO = "";
            searchSupplier = "";
            totalCost = totalMaterialCost = totalProcessCost = 0;
            MaterialOrders = new ObservableCollection<DcMaterialOrder>();
            MaterialOrderItems = new ObservableCollection<DcMaterialOrderItem>();
        }
        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);
            Doc = new RelayCommand<DcMaterialOrder>(ActionGenerateDoc);
            Excel = new RelayCommand<DcMaterialOrder>(ActionGenerateExcel);

            Add = new RelayCommand(ActionAdd, CanAdd);
            Edit = new RelayCommand<DcMaterialOrder>(ActionEdit, CanEdit);

            AddItem = new RelayCommand<DcMaterialOrder>(ActionAddItem, CanAddItem);
            EditItem = new RelayCommand<DcMaterialOrderItem>(ActionEditItem, CanEditItem);

            SelectionChanged = new RelayCommand<DcMaterialOrder>(ActionSelectionChanged);

            GoToMaterialOrderItemList = new RelayCommand(ActionGoToMaterialOrderItemList);
            GoToMaterialOrderItemListUnCompleted = new RelayCommand(ActionGoToMaterialOrderItemListFlag);

            ShowRawMaterial = new RelayCommand<DcMaterialOrderItem>(ActionShowRawMaterial);
        }

        private void ActionShowRawMaterial(DcMaterialOrderItem obj)
        {
            if (obj != null)
            {
                var dialog = new Components.MaterialOrder.SimpleMaterialResultReadOnly();
                dialog.KeyStrings = obj.ProvideRawMaterial;
                dialog.ShowDialog();
            }
        }

        private void ActionGenerateExcel(DcMaterialOrder obj)
        {
            if (obj != null)
            {
                if (!XSHelper.XS.MessageBox.ShowYesNo("请问要创建该订单的excel文档吗？"))
                {
                    return;
                }
                var excel = new ExcelOutputHelper.ExcelOutputMaterialOrder(obj.ID);
                excel.Intialize($"Material Order PO_{obj.OrderPO}", "Data");
                excel.Output();
            }
        }

        private void ActionGoToMaterialOrderItemListFlag()
        {
            NavigationService.GoTo(PMSViews.MaterialOrderItemListUnCompleted);
        }

        public void SetSearch(string orderPO, string supplier)
        {
            SearchOrderPO = orderPO;
            SearchSupplier = supplier;
            SetPageParametersWhenConditionChange();
        }

        private void ActionGoToMaterialOrderItemList()
        {
            NavigationService.GoTo(PMSViews.MaterialOrderItemList);
        }

        private bool CanEditItem(DcMaterialOrderItem arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditMaterialOrder);
        }

        private bool CanAddItem(DcMaterialOrder arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditMaterialOrder);
        }


        private bool CanEdit(DcMaterialOrder arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditMaterialOrder);
        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditMaterialOrder);
        }

        private void ActionSelectionChanged(DcMaterialOrder model)
        {
            if (model != null)
            {
                using (var service = new MaterialOrderServiceClient())
                {
                    var result = service.GetMaterialOrderItembyMaterialID(model.ID);
                    MaterialOrderItems.Clear();
                    result.ToList().ForEach(i => MaterialOrderItems.Add(i));
                    CurrentSelectItem = model;

                    CalculateTotalCost();
                }
            }
        }

        private void CalculateTotalCost()
        {
            double total = 0, total_material = 0, total_process = 0;
            foreach (var item in MaterialOrderItems)
            {
                total += item.UnitPrice * item.Weight + item.MaterialPrice;
                total_material += item.MaterialPrice;
                total_process += item.UnitPrice * item.Weight;
            }

            TotalCost = total;
            TotalMaterialCost = total_material;
            TotalProcessCost = total_process;
        }

        private void ActionEditItem(DcMaterialOrderItem item)
        {
            if (CurrentSelectItem.State == PMSCommon.MaterialOrderState.已核验.ToString())
            {
                //修改提示
                if (!PMSDialogService.ShowYesNo("请问", "此订单已是[已核验状态]，需要慎重修改，依然确定要【修改】这个订单项吗？"))
                    return;
            }


            if (item != null)
            {
                PMSHelper.ViewModels.MaterialOrderItemEdit.SetEdit(item);
                NavigationService.GoTo(PMSViews.MaterialOrderItemEdit);
            }
        }

        private void ActionAddItem(DcMaterialOrder order)
        {
            if (order != null)
            {
                PMSHelper.ViewModels.MaterialOrderItemEdit.SetNew(order);
                NavigationService.GoTo(PMSViews.MaterialOrderItemEdit);
            }
        }

        private void ActionEdit(DcMaterialOrder order)
        {
            if (order != null)
            {
                PMSHelper.ViewModels.MaterialOrderEdit.SetEdit(order);
                NavigationService.GoTo(PMSViews.MaterialOrderEdit);
            }
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.MaterialOrderEdit.SetNew();
            NavigationService.GoTo(PMSViews.MaterialOrderEdit);
        }

        /// <summary>
        /// 生成报告部分
        /// </summary>
        /// <param name="order"></param>
        private void ActionGenerateDoc(DcMaterialOrder order)
        {
            if (!XSHelper.XS.MessageBox.ShowYesNo("请问要创建该订单的word文档吗？"))
            {
                return;
            }

            try
            {
                if (order != null)
                {
                    NavigationService.Status("开始创建报告……");
                    WordMaterialOrderHorizontal report = new WordMaterialOrderHorizontal();
                    report.SetModel(order);
                    report.Output();
                    //PMSDialogService.ShowYes("原材料报告创建成功，请在桌面查看");
                    NavigationService.Status("原材料订单创建完毕！");
                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
                NavigationService.Status(ex.Message);
            }
        }

        private bool CanSearch()
        {
            return !(string.IsNullOrEmpty(SearchOrderPO) && string.IsNullOrEmpty(SearchSupplier));
        }

        private void ActionAll()
        {
            SearchOrderPO = "";
            SearchSupplier = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 30;
            var service = new MaterialOrderServiceClient();
            RecordCount = service.GetMaterialOrderCountBySearch(SearchOrderPO, SearchSupplier);
            service.Close();
            ActionPaging();
        }
        /// <summary>
        /// 分页动作的时候读入数据
        /// </summary>
        private void ActionPaging()
        {

            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var service = new MaterialOrderServiceClient();
            var orders = service.GetMaterialOrderBySearch(skip, take, SearchOrderPO, SearchSupplier);
            service.Close();
            MaterialOrders.Clear();
            orders.ToList().ForEach(o => MaterialOrders.Add(o));

            CurrentSelectIndex = 0;
            CurrentSelectItem = MaterialOrders.FirstOrDefault();
            ActionSelectionChanged(CurrentSelectItem);
        }


        #region Proeperties
        private string searchOrderPO;
        public string SearchOrderPO
        {
            get { return searchOrderPO; }
            set
            {
                if (searchOrderPO == value)
                    return;
                searchOrderPO = value;
                RaisePropertyChanged(() => SearchOrderPO);
            }
        }
        private string searchSupplier;
        public string SearchSupplier
        {
            get { return searchSupplier; }
            set
            {
                if (searchSupplier == value)
                    return;
                searchSupplier = value;
                RaisePropertyChanged(() => SearchSupplier);
            }
        }
        private double totalCost;

        public double TotalCost
        {
            get { return totalCost; }
            set { totalCost = value; RaisePropertyChanged(nameof(TotalCost)); }
        }

        private double totalProcessCost;
        public double TotalProcessCost
        {
            get { return totalProcessCost; }
            set { totalProcessCost = value; RaisePropertyChanged(nameof(TotalProcessCost)); }
        }

        private double totalMaterialCost;
        public double TotalMaterialCost
        {
            get { return totalMaterialCost; }
            set { totalMaterialCost = value; RaisePropertyChanged(nameof(TotalMaterialCost)); }
        }

        public ObservableCollection<DcMaterialOrder> MaterialOrders { get; set; }
        public ObservableCollection<DcMaterialOrderItem> MaterialOrderItems { get; set; }
        private int currrentSelectIndex;
        public int CurrentSelectIndex
        {
            get { return currrentSelectIndex; }
            set { currrentSelectIndex = value; RaisePropertyChanged(nameof(CurrentSelectIndex)); }
        }

        private DcMaterialOrder currentSelectItem;
        public DcMaterialOrder CurrentSelectItem
        {
            get { return currentSelectItem; }
            set { currentSelectItem = value; RaisePropertyChanged(nameof(CurrentSelectItem)); }
        }


        #endregion

        #region Commands
        public RelayCommand Add { get; private set; }
        public RelayCommand<DcMaterialOrder> Edit { get; set; }
        public RelayCommand<DcMaterialOrder> Doc { get; private set; }
        public RelayCommand<DcMaterialOrder> Excel { get; private set; }
        public RelayCommand<DcMaterialOrder> Refresh { get; set; }
        public RelayCommand<DcMaterialOrder> SelectionChanged { get; set; }
        public RelayCommand GoToMaterialOrderItemList { get; set; }
        public RelayCommand GoToMaterialOrderItemListUnCompleted { get; set; }
        public RelayCommand<DcMaterialOrder> AddItem { get; private set; }
        public RelayCommand<DcMaterialOrderItem> EditItem { get; private set; }

        public RelayCommand<DcMaterialOrderItem> ShowRawMaterial { get; set; }
        #endregion
    }
}
