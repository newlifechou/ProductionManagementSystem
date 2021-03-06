﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSClient.MainService;
using System.Collections.ObjectModel;

namespace PMSClient.ViewModel
{
    public class OrderCheckEditVM : BaseViewModelEdit
    {
        public OrderCheckEditVM()
        {
            InitializeCommands();
            InitializeProperties();
        }

        public void SetEdit(DcOrder order)
        {
            if (order != null)
            {
                IsNew = false;
                CurrentOrder = order;
            }
        }



        private void InitializeCommands()
        {
            Save = new RelayCommand(ActionSave, CanSave);
            GiveUp = new RelayCommand(GoBack);
        }

        public void InitializeProperties()
        {
            OrderStates = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.OrderState>(OrderStates);

            OrderPriorities = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.OrderPriority>(OrderPriorities);

            PolicyTypes = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.OrderPolicyType>(PolicyTypes);
        }

        private bool CanSave()
        {
            return true;
        }

        private void ActionSave()
        {
            //订单完成警告
            if (CurrentOrder.State == PMSCommon.OrderState.完成.ToString())
            {
                PMSDialogService.ShowWarning("将该订单状态设定为【完成】后，生产经理将无法安排新的热压计划到该订单任务下\r\n请确定所有靶材和样品都完成了再设置【完成】");
            }

            if (!PMSDialogService.ShowYesNo("请问", "确定保存这条记录？"))
            {
                return;
            }
            if (CurrentOrder.State == "作废")
            {
                if (!PMSDialogService.ShowYesNo("请问", "确定作废这条记录？"))
                {
                    return;
                }
            }
            try
            {
                CurrentOrder.Reviewer = PMSHelper.CurrentSession.CurrentUser.UserName;
                CurrentOrder.ReviewTime = DateTime.Now;
                string uid = PMSHelper.CurrentSession.CurrentUser.UserName;
                using (var service = new OrderServiceClient())
                {
                    if (IsNew)
                    {
                        service.AddOrderByUID(CurrentOrder, uid);
                    }
                    else
                    {
                        if (CurrentOrder.State=="完成")
                        {
                            CurrentOrder.FinishTime = DateTime.Now;
                        }
                        service.UpdateOrderByUID(CurrentOrder, uid);
                    }
                    service.Close();
                    GoBack();
                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private static void GoBack()
        {
            NavigationService.GoTo(PMSViews.Order);
        }

        private DcOrder currentOrder;

        public DcOrder CurrentOrder
        {
            get { return currentOrder; }
            set
            {
                currentOrder = value;
                RaisePropertyChanged(nameof(CurrentOrder));
            }
        }

        public List<string> OrderStates { get; set; }
        public List<string> OrderPriorities { get; set; }

        public List<string> PolicyTypes { get; set; }

    }
}
