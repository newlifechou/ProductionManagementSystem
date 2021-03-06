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

namespace PMSClient.ViewModel
{
    public class MaterialNeedVM : BaseViewModelPage
    {
        public MaterialNeedVM()
        {
            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
        }

        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }

        private void InitializeProperties()
        {
            SearchCompositoinStandard = "";
            SearchPMINumber = "";
            MainMaterialNeeds = new ObservableCollection<DcMaterialNeed>();
        }
        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);

            Add = new RelayCommand(ActionAdd, CanAdd);
            Edit = new RelayCommand<DcMaterialNeed>(ActionEdit, CanEdit);
            Duplicate = new RelayCommand<MainService.DcMaterialNeed>(ActionDuplicate, CanDuplicate);


        }

        private bool CanDuplicate(DcMaterialNeed arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditMaterialNeed);
        }

        private void ActionDuplicate(DcMaterialNeed model)
        {
            if (!PMSDialogService.ShowYesNo("请问", "请用确定要复用这条记录吗？"))
            {
                return;
            }
            if (model != null)
            {
                PMSHelper.ViewModels.MaterialNeedEdit.SetEdit(model);
                NavigationService.GoTo(PMSViews.MaterialNeedEdit);
            }
        }

        private bool CanEdit(DcMaterialNeed arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditMaterialNeed);
        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditMaterialNeed);
        }

        private void ActionEdit(DcMaterialNeed model)
        {
            if (model != null)
            {
                PMSHelper.ViewModels.MaterialNeedEdit.SetEdit(model);
                NavigationService.GoTo(PMSViews.MaterialNeedEdit);
            }
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.MaterialNeedEdit.SetNew();
            NavigationService.GoTo(PMSViews.MaterialNeedEdit);
        }

        private bool CanSearch()
        {
            return !(string.IsNullOrEmpty(SearchCompositoinStandard)&&string.IsNullOrEmpty(SearchPMINumber));
        }

        private void ActionAll()
        {
            SearchCompositoinStandard = SearchPMINumber = "";
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
            using (var service = new MaterialNeedServiceClient())
            {
                RecordCount = service.GetMaterialNeedCountBySearch(SearchCompositoinStandard,SearchPMINumber);
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
            using (var service = new MaterialNeedServiceClient())
            {
                var result = service.GetMaterialNeedBySearchInPage(skip, take, SearchCompositoinStandard, SearchPMINumber);
                MainMaterialNeeds.Clear();
                result.ToList().ForEach(o => MainMaterialNeeds.Add(o));
            }

        }

        #region Proeperties
        private string searchCompositionStandard;
        public string SearchCompositoinStandard
        {
            get { return searchCompositionStandard; }
            set
            {
                if (searchCompositionStandard == value)
                    return;
                searchCompositionStandard = value;
                RaisePropertyChanged(() => SearchCompositoinStandard);
            }
        }

        private string searchPMINumber;
        public string SearchPMINumber
        {
            get { return searchPMINumber; }
            set
            {
                if (searchPMINumber == value)
                    return;
                searchPMINumber = value;
                RaisePropertyChanged(nameof(SearchPMINumber));
            }
        }



        private ObservableCollection<DcMaterialNeed> mainMaterialNeeds;
        public ObservableCollection<DcMaterialNeed> MainMaterialNeeds
        {
            get { return mainMaterialNeeds; }
            set { mainMaterialNeeds = value; RaisePropertyChanged(nameof(MainMaterialNeeds)); }
        }

        #endregion

        #region Commands
        public RelayCommand Add { get; private set; }
        public RelayCommand<DcMaterialNeed> Edit { get; private set; }
        public RelayCommand<DcMaterialNeed> Duplicate { get; private set; }
        #endregion



    }
}
