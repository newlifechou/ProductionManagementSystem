﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSShipment.TCB;
using PMSShipment.VMHelper;

namespace PMSShipment
{
    public class MainWindowVM : BaseViewModelPage
    {
        public MainWindowVM()
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
            searchDeliveryName = "";
            Deliveries = new ObservableCollection<DcDelivery>();
            DeliveryItems = new ObservableCollection<DcDeliveryItem>();
        }
        private void InitializeCommands()
        {
            Search = new RelayCommand(ActionSearch);
            All = new RelayCommand(ActionAll);
            PageChanged = new RelayCommand(ActionPaging);
            Edit = new RelayCommand<DcDelivery>(ActionEdit);

            EditItem = new RelayCommand<DcDeliveryItem>(ActionEditItem);

            SelectionChanged = new RelayCommand<DcDelivery>(ActionSelectionChanged);

            QuickSave = new RelayCommand<DcDeliveryItem>(ActionQuickSave);
            ExpressTrack = new RelayCommand(ActionExpressTrack);

        }

        private void ActionExpressTrack()
        {
            //if (!PMSDialogService.ShowYesNo("请问", "确定追踪【未完成】的发货物流情况吗？"))
            //    return;
            ////追踪物流情况
            //new Express.Operation().TraceUnCompleted();
        }

        private void ActionQuickSave(DcDeliveryItem obj)
        {
            try
            {

            }
            catch (Exception ex)
            {
            }
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void ActionAll()
        {
            SearchDeliveryName = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSelectionChanged(DcDelivery model)
        {
            try
            {
                if (model != null)
                {
                    using (var service = new TCBServiceClient())
                    {
                        var result = service.GetDeliveryItemTCBByDeliveryID(model.ID);
                        DeliveryItems.Clear();
                        result.OrderBy(i => i.OrderNumber)
                            .ThenBy(i => i.PackNumber)
                            .ThenBy(i => i.ProductID)
                            .ToList().ForEach(i => DeliveryItems.Add(i));

                        RaisePropertyChanged(nameof(TotalItems));
                        CurrentSelectItem = model;
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 发货单标签打印区域
        /// </summary>
        //private bt.Application btApp;
        //private bt.Format btnFormat;
        private void ActionLabel(DcDelivery model)
        {

        }

        private void ActionEditItem(DcDeliveryItem model)
        {

        }

        private void ActionEdit(DcDelivery model)
        {

        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 20;
            var service = new TCBServiceClient();
            RecordCount = service.GetDeliveryCount(SearchDeliveryName);
            service.Close();
            ActionPaging();
        }
        private void ActionPaging()
        {

            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var service = new TCBServiceClient();
            var models = service.GetDelivery(skip, take, SearchDeliveryName);
            service.Close();
            Deliveries.Clear();
            models.ToList().ForEach(o => Deliveries.Add(o));

            CurrentSelectIndex = 0;
            CurrentSelectItem = Deliveries.FirstOrDefault();
            ActionSelectionChanged(CurrentSelectItem);
        }
        #region Properties
        public ObservableCollection<DcDelivery> Deliveries { get; set; }
        public ObservableCollection<DcDeliveryItem> DeliveryItems { get; set; }

        public int TotalItems
        {
            get
            {
                return DeliveryItems.Count;
            }
        }


        private int currentSelectIndex;

        public int CurrentSelectIndex
        {
            get { return currentSelectIndex; }
            set { currentSelectIndex = value; RaisePropertyChanged(nameof(CurrentSelectIndex)); }
        }
        private DcDelivery currentSelectItem;

        public DcDelivery CurrentSelectItem
        {
            get { return currentSelectItem; }
            set { currentSelectItem = value; RaisePropertyChanged(nameof(CurrentSelectItem)); }
        }

        private string searchDeliveryName;
        public string SearchDeliveryName
        {
            get { return searchDeliveryName; }
            set { searchDeliveryName = value; RaisePropertyChanged(nameof(SearchDeliveryName)); }
        }

        #endregion

        public RelayCommand<DcDelivery> Edit { get; set; }
        public RelayCommand<DcDeliveryItem> EditItem { get; set; }
        public RelayCommand<DcDeliveryItem> SearchRecordTest { get; set; }
        public RelayCommand<DcDelivery> SelectionChanged { get; set; }

        public RelayCommand<DcDeliveryItem> QuickSave { get; set; }
        public RelayCommand ExpressTrack { get; set; }


    }
}
