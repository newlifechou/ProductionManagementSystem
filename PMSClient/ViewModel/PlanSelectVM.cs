﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;

namespace PMSClient.ViewModel
{
    public class PlanSelectVM : BaseViewModelPage
    {
        public PlanSelectVM()
        {
            IntitializeCommands();
            IntitializeProperties();
            SetPageParametersWhenConditionChange();
        }

        private void ActionSelect(DcMissonWithPlan plan)
        {
            if (plan!=null)
            {
                switch (requestView)
                {
                    case PMSViews.RecordMillingEdit:
                        PMSHelper.ViewModels.RecordMillingEdit.SetBySelect(plan);
                        break;
                    case PMSViews.RecordVHPQuickEdit:
                        break;
                    case PMSViews.RecordDeMoldEdit:
                        PMSHelper.ViewModels.RecordDeMoldEdit.SetBySelect(plan);
                        break;
                    case PMSViews.RecordMachineEdit:
                        PMSHelper.ViewModels.RecordMachineEdit.SetBySelect(plan);
                        break;
                    case PMSViews.RecordTestEdit:
                        PMSHelper.ViewModels.RecordTestEdit.SetBySelect(plan);
                        break;
                    default:
                        break;
                }

                NavigationService.GoTo(requestView);
            }
        }

        private PMSViews requestView;
        /// <summary>
        /// 设置请求视图的token，返回或者选择后返回用
        /// </summary>
        /// <param name="request">请求视图的token</param>
        public void SetRequestView(PMSViews request)
        {
            requestView = request;
        }

        private void IntitializeProperties()
        {
            MissonWithPlans = new ObservableCollection<DcMissonWithPlan>();
            SearchPlanDate1 = DateTime.Now.AddDays(-90).Date;
            SearchPlanDate2 = DateTime.Now.AddDays(10).Date;
        }

        private void IntitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            GiveUp = new RelayCommand(() => NavigationService.GoTo(requestView));
            Select = new RelayCommand<DcMissonWithPlan>(ActionSelect);
            All = new RelayCommand(SetPageParametersWhenConditionChange);
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 20;
            using (var service = new MissonServiceClient())
            {
                RecordCount = service.GetMissonWithPlanCheckedCountByDateRange(SearchPlanDate1, SearchPlanDate2);
            }
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
            //只显示Checked过的计划
            using (var service = new MissonServiceClient())
            {
                var orders = service.GetMissonWithPlanCheckedByDateRange(skip, take, SearchPlanDate1, SearchPlanDate2);
                MissonWithPlans.Clear();
                orders.ToList().ForEach(o => MissonWithPlans.Add(o));
            }
        }





        #region Commands
        public RelayCommand GiveUp { get; set; }
        public RelayCommand<DcMissonWithPlan> Select { get; set; }
        #endregion

        #region Properties
        public ObservableCollection<DcMissonWithPlan> MissonWithPlans { get; set; }

        private DateTime searchPlanDate1;
        public DateTime SearchPlanDate1
        {
            get { return searchPlanDate1; }
            set { searchPlanDate1 = value; RaisePropertyChanged(nameof(SearchPlanDate1)); }
        }

        private DateTime searchPlanDate2;
        public DateTime SearchPlanDate2
        {
            get { return searchPlanDate2; }
            set { searchPlanDate2 = value; RaisePropertyChanged(nameof(SearchPlanDate2)); }
        }
        #endregion

    }
}
