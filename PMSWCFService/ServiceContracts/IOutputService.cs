﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PMSWCFService.DataContracts;

namespace PMSWCFService.ServiceContracts
{
    /// <summary>
    /// 用于数据输出
    /// </summary>
    [ServiceContract]
    public interface IOutputService
    {
        [OperationContract]
        [Obsolete]
        List<PMS230DataModel> GetAll230Data(int s, int t);

        [OperationContract]
        [Obsolete]
        int GetAll230DataCount();


        [OperationContract]
        List<DcOutputSpecialFor230Model> GetAll230DataByYearMonth(int s, int t, int year_start, int month_start, int year_end, int month_end);

        [OperationContract]
        int GetAll230DataByYearMonthCount(int year_start, int month_start, int year_end, int month_end);


        [OperationContract]
        List<DcOrder> GetOrderByYearMonth(int s, int t, int year_start, int month_start, int year_end, int month_end);
        [OperationContract]
        int GetOrderByYearMonthCount(int year_start, int month_start, int year_end, int month_end);


        [OperationContract]
        List<DcPlanExtra> GetPlanByYearMonth(int s, int t, int year_start, int month_start, int year_end, int month_end);
        [OperationContract]
        int GetPlanByYearMonthCount(int year_start, int month_start, int year_end, int month_end);


        [OperationContract]
        List<DcPlanTrace> GetPlanTrace(int s, int t, int year_start, int month_start, int year_end, int month_end);
        [OperationContract]
        int GetPlanTraceCount(int year_start, int month_start, int year_end, int month_end);


        [OperationContract]
        List<DcMaterialOrderItemExtra> GetMaterialOrderItemsByYearMonth(int s, int t, int year_start, int month_start, int year_end, int month_end);
        [OperationContract]
        int GetMaterialOrderItemsByYearMonthCount(int year_start, int month_start, int year_end, int month_end);


        [OperationContract]
        List<DcConsumableInventory> GetConsumableInventoryByYearMonth(int s, int t);
        [OperationContract]
        int GetConsumableInventoryByYearMonthCount();

        [OperationContract]
        List<DcConsumablePurchase> GetConsumablePurchaseByYearMonth(int s, int t, int year_start, int month_start, int year_end, int month_end);
        [OperationContract]
        int GetConsumablePurchaseByYearMonthCount(int year_start, int month_start, int year_end, int month_end);


        [OperationContract]
        List<DcRecordBonding> GetRecordBondingByYearMonth(int s, int t, int year_start, int month_start, int year_end, int month_end);
        [OperationContract]
        int GetRecordBondingCountByYearMonth(int year_start, int month_start, int year_end, int month_end);

        [OperationContract]
        List<DcRecordTest> GetRecordTestByYearMonth(int s, int t, int year_start, int month_start, int year_end, int month_end);
        [OperationContract]
        int GetRecordTestCountByYearMonth(int year_start, int month_start, int year_end, int month_end);


        [OperationContract]
        List<DcDelivery> GetDeliveries(int year_start, int month_start, int year_end, int month_end);

    }
}
