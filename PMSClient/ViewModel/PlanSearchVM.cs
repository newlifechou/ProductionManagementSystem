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
    public class PlanSearchVM : BaseViewModelPage
    {
        public PlanSearchVM()
        {
            IntitializeCommands();
            IntitializeProperties();
            SetPageParametersWhenConditionChange();
        }

        private void IntitializeProperties()
        {
            MissonWithPlans = new ObservableCollection<DcMissonWithPlan>();
            SearchPlanDate1 = DateTime.Now.AddDays(-90).Date;
            SearchPlanDate2 = DateTime.Now.AddDays(1).Date;
        }

        private void IntitializeCommands()
        {
            GoToMisson = new RelayCommand(() => NavigationService.GoTo(PMSViews.Misson));
            Refresh = new RelayCommand(ActionRefresh);
            Search = new RelayCommand(ActionSearch);
            PageChanged = new RelayCommand(ActionPaging);
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void ActionRefresh()
        {
            SetPageParametersWhenConditionChange();
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 20;

            using (var service = new MissonServiceClient())
            {
                RecordCount = service.GetMissonWithPlanCheckedCountByDateRange(SearchPlanDate1, SearchPlanDate2);
            }
            //只显示Checked过的计划

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
        public RelayCommand GoToMisson { get; set; }
        public RelayCommand Refresh { get; set; }
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
