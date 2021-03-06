﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using System.Collections.ObjectModel;
using PMSCommon;

namespace PMSClient.ViewModel
{
    public class RecordDeMoldVM : BaseViewModelPage
    {
        public RecordDeMoldVM()
        {
            searchVHPPlanLot = searchComposition = "";
            PageChanged = new RelayCommand(ActionPaging);

            RecordDeMolds = new ObservableCollection<DcRecordDeMold>();
            Add = new RelayCommand(ActionAdd, CanAdd);
            Edit = new RelayCommand<DcRecordDeMold>(ActionEdit, CanEdit);

            Search = new RelayCommand(ActionSearch);
            All = new RelayCommand(ActionAll);
            Label = new RelayCommand<DcRecordDeMold>(ActionLabel);
            SelectionChanged = new RelayCommand<DcRecordDeMold>(ActionSelectionChanged);
            Duplicate = new RelayCommand<DcRecordDeMold>(ActionDuplicate, CanDuplicate);
            TempRecordSheet = new RelayCommand(ActionTempRecordSheet);
            QuickAdd = new RelayCommand(ActionQuickAdd, CanQuickAdd);
            Compare = new RelayCommand<DcRecordDeMold>(ActionCompare, CanQuickAdd);

            Fail = new RelayCommand<DcRecordDeMold>(ActionFail, CanEdit);
            SetPageParametersWhenConditionChange();

        }

        private void ActionFail(DcRecordDeMold obj)
        {

            if (!PMSDialogService.ShowYesNo("请问", "确定要添加此记录到报废记录中吗？"))
            {
                return;
            }

            if (obj != null)
            {
                try
                {
                    using (var service = new FailureService.FailureServiceClient())
                    {
                        int check_exist_count = service.GetFailuresCountByProductID(obj.VHPPlanLot);

                        if (check_exist_count == 0)
                        {
                            var model = VMHelper.FailureVMHelper.GetNewFailure();
                            model.ProductID = obj.VHPPlanLot;
                            model.Stage = "取模";
                            model.Composition = obj.Composition;
                            model.Details = obj.PMINumber;
                            model.Remark = obj.Remark;
                            model.Problem = "取模不成功";
                            model.Process = "无";

                            service.AddFailure(model);
                            PMSDialogService.Show("添加成功");

                        }
                        else
                        {
                            PMSDialogService.ShowWarning("报废库已存在");
                        }
                    }
                }
                catch (Exception ex)
                {
                    PMSHelper.CurrentLog.Error(ex);
                }
            }
        }

        private void ActionCompare(DcRecordDeMold obj)
        {
            SearchComposition = obj.Composition.Trim();
            SetPageParametersWhenConditionChange();
        }

        private bool CanQuickAdd(DcRecordDeMold arg)
        {
            return true;
        }

        /// <summary>
        /// 综合查询
        /// </summary>
        /// <param name="vhpnumber"></param>
        public void SetSearch(string vhpnumber)
        {
            SearchComposition = "";
            SearchVHPPlanLot = vhpnumber;
            SetPageParametersWhenConditionChange();
        }

        private bool CanQuickAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditRecordDeMold);
        }

        private void ActionQuickAdd()
        {
            PMSHelper.ViewModels.PlanSelect.SetRequestView(PMSViews.RecordDeMold);
            PMSHelper.ViewModels.PlanSelect.RefreshData();
            NavigationService.GoTo(PMSViews.PlanSelect);
        }

        private void ActionSelectionChanged(DcRecordDeMold model)
        {
            if (model != null)
            {
                CurrentRecordDeMold = model;
            }
        }

        private void ActionTempRecordSheet()
        {
            try
            {
                var filePath = System.IO.Path.Combine(Environment.CurrentDirectory, "Resource", "DocTemplate", "Doc", "取模临时记录单.docx");
                var tempPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Resource", "DocTemplate", "Doc", "取模临时记录单_temp.docx");
                if (System.IO.File.Exists(filePath))
                {
                    if (System.IO.File.Exists(tempPath))
                    {
                        System.IO.File.Delete(tempPath);
                    }
                    System.IO.File.Copy(filePath, tempPath);
                    System.Diagnostics.Process.Start(tempPath);
                }

            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
        private void ActionLabel(DcRecordDeMold model)
        {
            if (model != null)
            {

                var sb = new StringBuilder();
                sb.Append(model.PlanType);
                sb.AppendLine(model.VHPPlanLot);
                sb.AppendLine(model.Composition);
                sb.Append("模具:");
                sb.AppendLine(model.CalculateDimension);
                sb.Append("产品:");
                sb.AppendLine(model.Dimension);
                sb.AppendLine();
                sb.AppendLine("++++++一般标签复制上面内容，样品标签复制下面内容+++++++");
                sb.AppendLine();
                sb.AppendLine(model.Composition);
                sb.AppendLine("样品      g");
                sb.AppendLine(model.VHPPlanLot);
                var mainContent = sb.ToString();

                //var pageTitle = "热压毛坯标签打印输出";
                //var tips = @"点击打开模板按钮，粘贴不同内容到模板合适位置，热压编号是自动生成的，可能不正确，请再自行修改，然后打印标签";
                //var template = "毛坯标签";
                //var helpimage = "productionlabel.png";
                //PMSHelper.ToolViewModels.LabelOutPut.SetAllParameters(PMSViews.RecordDeMold, pageTitle,
                //    tips, template, mainContent, helpimage);
                //NavigationService.GoTo(PMSViews.LabelOutPut);

                //2017-12-18
                PMSClient.ToolWindow.LabelCopyWindow lcw = new ToolWindow.LabelCopyWindow();
                lcw.LabelInformation = mainContent;
                lcw.Show();
            }
        }

        private bool CanDuplicate(DcRecordDeMold arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditRecordDeMold);
        }

        private bool CanEdit(DcRecordDeMold arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditRecordDeMold);
        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditRecordDeMold);
        }

        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }
        private void ActionAll()
        {
            SearchVHPPlanLot = SearchComposition = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }


        private static void GoToEditView()
        {
            NavigationService.GoTo(PMSViews.RecordDeMoldEdit);
        }

        private void ActionDuplicate(DcRecordDeMold model)
        {
            if (PMSDialogService.ShowYesNo("请问", "确定复用这条记录？"))
            {
                PMSHelper.ViewModels.RecordDeMoldEdit.SetByDuplicate(model);
                GoToEditView();
            }
        }
        private void ActionEdit(DcRecordDeMold model)
        {
            PMSHelper.ViewModels.RecordDeMoldEdit.SetEdit(model);
            GoToEditView();
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.RecordDeMoldEdit.SetNew();
            GoToEditView();
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 30;
            var service = new RecordDeMoldServiceClient();
            RecordCount = service.GetRecordDeMoldsCountByVHPPlanLot(SearchVHPPlanLot, SearchComposition);
            service.Close();
            ActionPaging();
        }
        private void ActionPaging()
        {

            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var service = new RecordDeMoldServiceClient();
            var models = service.GetRecordDeMoldsByVHPPlanLot(skip, take, SearchVHPPlanLot, SearchComposition);
            service.Close();
            RecordDeMolds.Clear();
            models.ToList().ForEach(o => RecordDeMolds.Add(o));
            CurrentRecordDeMold = RecordDeMolds.FirstOrDefault();
        }

        public RelayCommand<DcRecordDeMold> SelectionChanged { get; set; }

        private DcRecordDeMold currentRecordDeMold;

        public DcRecordDeMold CurrentRecordDeMold
        {
            get { return currentRecordDeMold; }
            set { currentRecordDeMold = value; RaisePropertyChanged(nameof(CurrentRecordDeMold)); }
        }


        private string searchVHPPlanLot;
        public string SearchVHPPlanLot
        {
            get { return searchVHPPlanLot; }
            set { searchVHPPlanLot = value; RaisePropertyChanged(nameof(SearchVHPPlanLot)); }
        }
        private string searchComposition;
        public string SearchComposition
        {
            get { return searchComposition; }
            set { searchComposition = value; RaisePropertyChanged(nameof(SearchComposition)); }
        }
        public ObservableCollection<DcRecordDeMold> RecordDeMolds { get; set; }

        public RelayCommand Add { get; set; }
        public RelayCommand<DcRecordDeMold> Edit { get; set; }
        public RelayCommand<DcRecordDeMold> Duplicate { get; set; }
        public RelayCommand<DcRecordDeMold> Label { get; set; }
        public RelayCommand<DcRecordDeMold> Fail { get; set; }
        public RelayCommand<DcRecordDeMold> Compare { get; set; }
        public RelayCommand TempRecordSheet { get; set; }
        public RelayCommand QuickAdd { get; set; }
    }
}
