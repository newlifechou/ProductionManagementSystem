﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.ServiceContracts;
using PMSWCFService.DataContracts;
using PMSDAL;
using AutoMapper;
using PMSCommon;

namespace PMSWCFService
{
    public partial class PMSService : IMissonService
    {
        public List<DcOrder> GetMissons(int skip, int take)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<PMSOrder, DcOrder>();
                        cfg.CreateMap<PMSPlanVHP, DcPlanVHP>();
                    });

                    var result = from o in dc.Orders
                                 where o.PolicyType.Contains("VHP")
                                 && (o.State == OrderState.未完成.ToString()
                                 || o.State == OrderState.暂停.ToString()
                                 || o.State == OrderState.完成.ToString())
                                 orderby o.CreateTime descending
                                 select o;

                    var missons = Mapper.Map<List<PMSOrder>, List<DcOrder>>(result.Skip(skip).Take(take).ToList());

                    return missons;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetMissonsCount()
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from o in dc.Orders
                                where o.PolicyType.Contains("VHP")
                                 && (o.State == OrderState.未完成.ToString()
                                 || o.State == OrderState.暂停.ToString()
                                 || o.State == OrderState.完成.ToString())
                                select o;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcOrder> GetMissonsBySearch(int skip, int take, string compostion, string pminumber)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<PMSOrder, DcOrder>();
                        cfg.CreateMap<PMSPlanVHP, DcPlanVHP>();
                    });

                    var result = from o in dc.Orders
                                 where o.PolicyType.Contains("VHP")
                                 && o.CompositionStandard.Contains(compostion)
                                 && o.PMINumber.Contains(pminumber)
                                 && (o.State == OrderState.未完成.ToString()
                                 || o.State == OrderState.暂停.ToString()
                                 || o.State == OrderState.完成.ToString())
                                 orderby o.CreateTime descending
                                 select o;

                    var missons = Mapper.Map<List<PMSOrder>, List<DcOrder>>(result.Skip(skip).Take(take).ToList());

                    return missons;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetMissonsCountBySearch(string compostion, string pminumber)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from o in dc.Orders
                                where o.PolicyType.Contains("VHP")
                                 && o.CompositionStandard.Contains(compostion)
                                 && o.PMINumber.Contains(pminumber)
                                 && (o.State == OrderState.未完成.ToString()
                                 || o.State == OrderState.暂停.ToString()
                                 || o.State == OrderState.完成.ToString())
                                select o;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
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
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetPlanWithMissonCount()
        {
            try
            {
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
                LocalService.CurrentLog.Error(ex);
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
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    var queryPlan = (from p in dc.VHPPlans
                                     where p.State == PMSCommon.CommonState.已核验.ToString()
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
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }
        public int GetPlanWithMissonCheckedCount()
        {
            try
            {
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
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcPlanWithMisson> GetPlanWithMissonCheckedByDateRange(int skip, int take, DateTime dateStart, DateTime dateEnd)
        {
            try
            {
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
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetPlanWithMissonCheckedCountByDateRange(DateTime dateStart, DateTime dateEnd)
        {
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
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 返回未完成订单的数目
        /// </summary>
        /// <returns></returns>
        public int GetMissonUnCompletedCount()
        {
            try
            {
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    var query = from o in dc.Orders
                                where o.State == PMSCommon.OrderState.未完成.ToString()
                                select o;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcPlanWithMisson> GetPlanWithMissonCheckedByDateRange2(int skip, int take, DateTime dateStart, DateTime dateEnd, string composition)
        {
            try
            {
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
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetPlanWithMissonCheckedCountByDateRange2(DateTime dateStart, DateTime dateEnd, string composition)
        {
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
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }

        }
    }
}