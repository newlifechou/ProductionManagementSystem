﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.ServiceContracts;
using PMSWCFService.DataContracts;
using PMSDAL;
using AutoMapper;
using PMSCommon;
using System.Data.Entity;

namespace PMSWCFService
{
    /// <summary>
    /// 2019-12-4 添加了对取消订单的过滤
    /// </summary>
    public partial class PMSService : IMissonService
    {
        public List<DcOrder> GetMissons(int skip, int take)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<PMSOrder, DcOrder>();
                        cfg.CreateMap<PMSPlanVHP, DcPlanVHP>();
                    });

                    var result = from o in dc.Orders
                                 where o.PolicyType == PMSCommon.OrderPolicyType.VHP.ToString()
                                 && (o.State == OrderState.未完成.ToString() || o.State == OrderState.取消.ToString()
                                 || o.State == OrderState.暂停.ToString()
                                 || o.State == OrderState.最终完成.ToString())
                                 orderby o.CreateTime descending
                                 select o;

                    var missons = Mapper.Map<List<PMSOrder>, List<DcOrder>>(result.Skip(skip).Take(take).ToList());

                    return missons;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetMissonsCount()
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var query = from o in dc.Orders
                                where o.PolicyType == PMSCommon.OrderPolicyType.VHP.ToString()
                                 && (o.State == OrderState.未完成.ToString() || o.State == OrderState.取消.ToString()
                                 || o.State == OrderState.暂停.ToString()
                                 || o.State == OrderState.生产完成.ToString()
                                 || o.State == OrderState.最终完成.ToString())
                                select o;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcOrder> GetMissonsBySearch(int skip, int take, string composition, string pminumber)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<PMSOrder, DcOrder>();
                        cfg.CreateMap<PMSPlanVHP, DcPlanVHP>();
                    });

                    var result = from o in dc.Orders
                                 where o.PolicyType == PMSCommon.OrderPolicyType.VHP.ToString()
                                 && (o.State == OrderState.未完成.ToString() || o.State == OrderState.取消.ToString()
                                 || o.State == OrderState.暂停.ToString()
                                 || o.State == OrderState.生产完成.ToString()
                                 || o.State == OrderState.最终完成.ToString())
                                 && o.CompositionStandard.Contains(composition)
                                 && o.PMINumber.Contains(pminumber)
                                 orderby o.CreateTime descending
                                 select o;

                    var missons = Mapper.Map<List<PMSOrder>, List<DcOrder>>(result.Skip(skip).Take(take).ToList());

                    return missons;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetMissonsCountBySearch(string composition, string pminumber)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var query = from o in dc.Orders
                                where o.PolicyType == PMSCommon.OrderPolicyType.VHP.ToString()
                                 && o.CompositionStandard.Contains(composition)
                                 && o.PMINumber.Contains(pminumber)
                                 && (o.State == OrderState.未完成.ToString() || o.State == OrderState.取消.ToString()
                                 || o.State == OrderState.暂停.ToString()
                                 || o.State == OrderState.生产完成.ToString()
                                 || o.State == OrderState.最终完成.ToString())
                                select o;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcOrder> GetMissonUnCompleted(int skip, int take, string composition, string pminumber)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<PMSOrder, DcOrder>();
                        cfg.CreateMap<PMSPlanVHP, DcPlanVHP>();
                    });

                    var result = from o in dc.Orders
                                 where o.PolicyType == PMSCommon.OrderPolicyType.VHP.ToString()
                                 && (o.State == OrderState.未完成.ToString())
                                 && o.CompositionStandard.Contains(composition)
                                 && o.PMINumber.Contains(pminumber)
                                 orderby o.Priority, o.CreateTime
                                 select o;

                    var missons = Mapper.Map<List<PMSOrder>, List<DcOrder>>(result.Skip(skip).Take(take).ToList());

                    return missons;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetMissonUnCompletedCount(string composition, string pminumber)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var query = from o in dc.Orders
                                where o.PolicyType == PMSCommon.OrderPolicyType.VHP.ToString()
                                 && o.CompositionStandard.Contains(composition)
                                 && o.PMINumber.Contains(pminumber)
                                 && (o.State == OrderState.未完成.ToString())
                                select o;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcOrder> GetMissonUnCompletedSample(int skip, int take)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<PMSOrder, DcOrder>();
                        cfg.CreateMap<PMSPlanVHP, DcPlanVHP>();
                    });

                    var result = from o in dc.Orders
                                 where o.PolicyType == PMSCommon.OrderPolicyType.VHP.ToString()
                                 && (o.State == OrderState.未完成.ToString()
                                 || o.State == OrderState.暂停.ToString())
                                 && o.SampleNeed!="无需样品"
                                 orderby o.Priority, o.CreateTime
                                 select o;

                    var missons = Mapper.Map<List<PMSOrder>, List<DcOrder>>(result.Skip(skip).Take(take).ToList());

                    return missons;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetMissonUnCompletedCountSample()
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var query = from o in dc.Orders
                                where o.PolicyType == PMSCommon.OrderPolicyType.VHP.ToString()
                                 && o.SampleNeed != "无需样品"
                                 && (o.State == OrderState.未完成.ToString()
                                 || o.State == OrderState.暂停.ToString())
                                select o;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetMissonUnCompletedCount()
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var query = from o in dc.Orders
                                where o.PolicyType == PMSCommon.OrderPolicyType.VHP.ToString()
                                 && o.State == OrderState.未完成.ToString()
                                select o;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public List<DcPlanWithMisson> GetPlanWithMisson(int skip, int take)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    var queryPlan = (from p in dc.VHPPlans
                                     where p.State != PMSCommon.CommonState.作废.ToString()
                                     orderby p.PlanDate descending
                                     select p).Skip(skip).Take(take);
                    var queryResult = from p in queryPlan
                                      join o in dc.Orders
                                      on p.OrderID equals o.ID
                                      select new PMSPlanWithMissonModel() { Plan = p, Misson = o };
                    var final = queryResult.ToList();
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<PMSPlanWithMissonModel, DcPlanWithMisson>();
                        cfg.CreateMap<PMSOrder, DcOrder>();
                        cfg.CreateMap<PMSPlanVHP, DcPlanVHP>();
                    });

                    var result = Mapper.Map<List<PMSPlanWithMissonModel>, List<DcPlanWithMisson>>(final);

                    return result;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetPlanWithMissonCount()
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    var queryPlan = (from p in dc.VHPPlans
                                     where p.State != PMSCommon.CommonState.作废.ToString()
                                     orderby p.PlanDate descending
                                     select p);
                    var queryResult = from p in queryPlan
                                      join o in dc.Orders
                                      on p.OrderID equals o.ID
                                      select new PMSPlanWithMissonModel() { Plan = p, Misson = o };
                    return queryResult.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 分页获取Checked状态的
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public List<DcPlanWithMisson> GetPlanWithMissonChecked(int skip, int take)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    var queryPlan = (from p in dc.VHPPlans
                                     where p.State == PMSCommon.CommonState.已核验.ToString()
                                     orderby p.PlanDate descending, p.VHPDeviceCode descending
                                     select p).Skip(skip).Take(take);
                    var queryResult = from p in queryPlan
                                      join o in dc.Orders
                                      on p.OrderID equals o.ID
                                      select new PMSPlanWithMissonModel() { Plan = p, Misson = o };
                    var final = queryResult.ToList();
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<PMSPlanWithMissonModel, DcPlanWithMisson>();
                        cfg.CreateMap<PMSOrder, DcOrder>();
                        cfg.CreateMap<PMSPlanVHP, DcPlanVHP>();
                    });

                    var result = Mapper.Map<List<PMSPlanWithMissonModel>, List<DcPlanWithMisson>>(final);

                    return result;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }
        public int GetPlanWithMissonCheckedCount()
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    var queryPlan = (from p in dc.VHPPlans
                                     where p.State == PMSCommon.CommonState.已核验.ToString()
                                     orderby p.PlanDate descending
                                     select p);
                    var queryResult = from p in queryPlan
                                      join o in dc.Orders
                                      on p.OrderID equals o.ID
                                      select new PMSPlanWithMissonModel() { Plan = p, Misson = o };
                    return queryResult.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcPlanWithMisson> GetPlanWithMissonCheckedByDateRange(int skip, int take, DateTime dateStart, DateTime dateEnd)
        {
            try
            {
                XS.RunLog();
                var startDate = dateStart.Date;
                var endDate = dateEnd.Date;
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    var queryPlan = (from p in dc.VHPPlans
                                     where p.State == PMSCommon.CommonState.已核验.ToString()
                                     && p.PlanDate >= startDate
                                     && p.PlanDate <= endDate
                                     orderby p.PlanDate descending
                                     select p).Skip(skip).Take(take);
                    var queryResult = from p in queryPlan
                                      join o in dc.Orders
                                      on p.OrderID equals o.ID
                                      select new PMSPlanWithMissonModel() { Plan = p, Misson = o };
                    var final = queryResult.ToList();
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<PMSPlanWithMissonModel, DcPlanWithMisson>();
                        cfg.CreateMap<PMSOrder, DcOrder>();
                        cfg.CreateMap<PMSPlanVHP, DcPlanVHP>();
                    });

                    var result = Mapper.Map<List<PMSPlanWithMissonModel>, List<DcPlanWithMisson>>(final);

                    return result;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetPlanWithMissonCheckedCountByDateRange(DateTime dateStart, DateTime dateEnd)
        {
                XS.RunLog();
            var startDate = dateStart.Date;
            var endDate = dateEnd.Date;
            try
            {
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    var queryPlan = (from p in dc.VHPPlans
                                     where p.State == PMSCommon.CommonState.已核验.ToString()
                                     && p.PlanDate >= startDate
                                     && p.PlanDate <= endDate
                                     orderby p.PlanDate descending
                                     select p);
                    var queryResult = from p in queryPlan
                                      join o in dc.Orders
                                      on p.OrderID equals o.ID
                                      select new PMSPlanWithMissonModel() { Plan = p, Misson = o };
                    return queryResult.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcPlanWithMisson> GetPlanWithMissonCheckedByDateRange2(int skip, int take, DateTime dateStart, DateTime dateEnd, string composition)
        {
            try
            {
                XS.RunLog();
                var startDate = dateStart.Date;
                var endDate = dateEnd.Date;
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    var queryPlan = (from p in dc.VHPPlans
                                     where p.State == PMSCommon.CommonState.已核验.ToString()
                                     && p.PlanDate >= startDate
                                     && p.PlanDate <= endDate
                                     orderby p.PlanDate descending
                                     select p).Skip(skip).Take(take);
                    var queryResult = from p in queryPlan
                                      join o in dc.Orders
                                      on p.OrderID equals o.ID
                                      where o.CompositionStandard.Contains(composition)
                                      select new PMSPlanWithMissonModel() { Plan = p, Misson = o };
                    var final = queryResult.ToList();
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<PMSPlanWithMissonModel, DcPlanWithMisson>();
                        cfg.CreateMap<PMSOrder, DcOrder>();
                        cfg.CreateMap<PMSPlanVHP, DcPlanVHP>();
                    });

                    var result = Mapper.Map<List<PMSPlanWithMissonModel>, List<DcPlanWithMisson>>(final);

                    return result;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetPlanWithMissonCheckedCountByDateRange2(DateTime dateStart, DateTime dateEnd, string composition)
        {
                XS.RunLog();
            var startDate = dateStart.Date;
            var endDate = dateEnd.Date;
            try
            {
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    var queryPlan = (from p in dc.VHPPlans
                                     where p.State == PMSCommon.CommonState.已核验.ToString()
                                     && p.PlanDate >= startDate
                                     && p.PlanDate <= endDate
                                     orderby p.PlanDate descending
                                     select p);
                    var queryResult = from p in queryPlan
                                      join o in dc.Orders
                                      on p.OrderID equals o.ID
                                      where o.CompositionStandard.Contains(composition)
                                      select new PMSPlanWithMissonModel() { Plan = p, Misson = o };
                    return queryResult.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }

        }

        public List<DcPlanWithMisson> GetPlanExtra(int skip, int take, string searchCode, string composition)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    var query = from p in dc.VHPPlans
                                join o in dc.Orders on p.OrderID equals o.ID
                                where p.State == PMSCommon.CommonState.已核验.ToString()
                                     && p.SearchCode.Contains(searchCode)
                                     && o.CompositionStandard.Contains(composition)
                                orderby DbFunctions.TruncateTime(p.PlanDate) descending, p.PlanLot descending, p.VHPDeviceCode descending, DbFunctions.TruncateTime(p.CreateTime) descending
                                select new PMSPlanWithMissonModel() { Plan = p, Misson = o };

                    var final = query.Skip(skip).Take(take).ToList();
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<PMSPlanWithMissonModel, DcPlanWithMisson>();
                        cfg.CreateMap<PMSOrder, DcOrder>();
                        cfg.CreateMap<PMSPlanVHP, DcPlanVHP>();
                    });

                    var result = Mapper.Map<List<PMSPlanWithMissonModel>, List<DcPlanWithMisson>>(final);

                    return result;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetPlanExtraCount(string searchCode, string composition)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    var query = from p in dc.VHPPlans
                                join o in dc.Orders on p.OrderID equals o.ID
                                where p.State == PMSCommon.CommonState.已核验.ToString()
                                     && p.SearchCode.Contains(searchCode)
                                     && o.CompositionStandard.Contains(composition)
                                select new PMSPlanWithMissonModel() { Plan = p, Misson = o };
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcPlanWithMisson> GetPlanExtraForProduct(int skip, int take, string searchCode, string composition)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    var query = from p in dc.VHPPlans
                                join o in dc.Orders on p.OrderID equals o.ID
                                where p.State == PMSCommon.CommonState.已核验.ToString()
                                && (p.PlanType=="加工"||p.PlanType=="其他"
                                        ||p.PlanType=="外协" || p.PlanType == "代工" || p.PlanType == "发货")
                                     && p.SearchCode.Contains(searchCode)
                                     && o.CompositionStandard.Contains(composition)
                                orderby DbFunctions.TruncateTime(p.PlanDate) descending, p.PlanLot descending, p.VHPDeviceCode descending, DbFunctions.TruncateTime(p.CreateTime) descending
                                select new PMSPlanWithMissonModel() { Plan = p, Misson = o };

                    var final = query.Skip(skip).Take(take).ToList();
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<PMSPlanWithMissonModel, DcPlanWithMisson>();
                        cfg.CreateMap<PMSOrder, DcOrder>();
                        cfg.CreateMap<PMSPlanVHP, DcPlanVHP>();
                    });

                    var result = Mapper.Map<List<PMSPlanWithMissonModel>, List<DcPlanWithMisson>>(final);

                    return result;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetPlanExtraForProductCount(string searchCode, string composition)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    var query = from p in dc.VHPPlans
                                join o in dc.Orders on p.OrderID equals o.ID
                                where p.State == PMSCommon.CommonState.已核验.ToString()
                                & (p.PlanType == "加工" || p.PlanType == "其他" 
                                || p.PlanType == "外协" || p.PlanType == "代工" || p.PlanType == "发货")
                                     && p.SearchCode.Contains(searchCode)
                                     && o.CompositionStandard.Contains(composition)
                                select new PMSPlanWithMissonModel() { Plan = p, Misson = o };
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public double GetUnVHPTargetCount()
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var query = from o in dc.Orders
                                where o.PolicyType == PMSCommon.OrderPolicyType.VHP.ToString()
                                 && o.State == OrderState.未完成.ToString()
                                 && o.ProductType=="靶材"
                                select o;
                    return query.Sum(i=>i.Quantity);
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcPlanWithMisson> GetPlanExtra2(int skip, int take, string searchCode, string composition, string pminumber)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    var query = from p in dc.VHPPlans
                                join o in dc.Orders on p.OrderID equals o.ID
                                where p.State == PMSCommon.CommonState.已核验.ToString()
                                     && p.SearchCode.Contains(searchCode)
                                     && o.CompositionStandard.Contains(composition)
                                     && o.PMINumber.Contains(pminumber)
                                orderby DbFunctions.TruncateTime(p.PlanDate) descending, p.PlanLot descending, p.VHPDeviceCode descending, DbFunctions.TruncateTime(p.CreateTime) descending
                                select new PMSPlanWithMissonModel() { Plan = p, Misson = o };

                    var final = query.Skip(skip).Take(take).ToList();
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<PMSPlanWithMissonModel, DcPlanWithMisson>();
                        cfg.CreateMap<PMSOrder, DcOrder>();
                        cfg.CreateMap<PMSPlanVHP, DcPlanVHP>();
                    });

                    var result = Mapper.Map<List<PMSPlanWithMissonModel>, List<DcPlanWithMisson>>(final);

                    return result;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetPlanExtraCount2(string searchCode, string composition, string pminumber)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    var query = from p in dc.VHPPlans
                                join o in dc.Orders on p.OrderID equals o.ID
                                where p.State == PMSCommon.CommonState.已核验.ToString()
                                     && p.SearchCode.Contains(searchCode)
                                     && o.CompositionStandard.Contains(composition)
                                     && o.PMINumber.Contains(pminumber)
                                select new PMSPlanWithMissonModel() { Plan = p, Misson = o };
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcPlanWithMisson> GetPlanExtraForProduct2(int skip, int take, string searchCode, string composition, string pminumber)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    var query = from p in dc.VHPPlans
                                join o in dc.Orders on p.OrderID equals o.ID
                                where p.State == PMSCommon.CommonState.已核验.ToString()
                                && (p.PlanType == "加工" || p.PlanType == "其他"
                                        || p.PlanType == "外协" || p.PlanType == "代工" || p.PlanType == "发货")
                                     && p.SearchCode.Contains(searchCode)
                                     && o.CompositionStandard.Contains(composition)
                                     && o.PMINumber.Contains(pminumber)
                                orderby DbFunctions.TruncateTime(p.PlanDate) descending, p.PlanLot descending, p.VHPDeviceCode descending, DbFunctions.TruncateTime(p.CreateTime) descending
                                select new PMSPlanWithMissonModel() { Plan = p, Misson = o };

                    var final = query.Skip(skip).Take(take).ToList();
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<PMSPlanWithMissonModel, DcPlanWithMisson>();
                        cfg.CreateMap<PMSOrder, DcOrder>();
                        cfg.CreateMap<PMSPlanVHP, DcPlanVHP>();
                    });

                    var result = Mapper.Map<List<PMSPlanWithMissonModel>, List<DcPlanWithMisson>>(final);

                    return result;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetPlanExtraForProductCount2(string searchCode, string composition, string pminumber)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    var query = from p in dc.VHPPlans
                                join o in dc.Orders on p.OrderID equals o.ID
                                where p.State == PMSCommon.CommonState.已核验.ToString()
                                & (p.PlanType == "加工" || p.PlanType == "其他"
                                || p.PlanType == "外协" || p.PlanType == "代工" || p.PlanType == "发货")
                                     && p.SearchCode.Contains(searchCode)
                                     && o.CompositionStandard.Contains(composition)
                                     && o.PMINumber.Contains(pminumber)
                                select new PMSPlanWithMissonModel() { Plan = p, Misson = o };
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetEmergencyOrderCount()
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    return dc.Orders.Where(o => o.Priority.Contains("紧急")
                    && o.State == OrderState.未完成.ToString()).Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }
    }
}