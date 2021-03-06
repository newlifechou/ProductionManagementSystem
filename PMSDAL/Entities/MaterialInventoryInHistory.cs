﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    public class MaterialInventoryInHistory
    {
        public Guid Id { get; set; }
        public string State { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public string MaterialLot { get; set; }
        public string PMINumber { get; set; }
        public string Composition { get; set; }
        public string Purity { get; set; }
        public string Supplier { get; set; }
        public double Weight { get; set; }
        public string Remark { get; set; }
        public string QuickRemark { get; set; }
        public string MaterialSource { get; set; }
        public string SupplierPO { get; set; }
        public string GDMS { get; set; }
        public string ICPOES { get; set; }

        //操作者和操作时间
        [Key]
        public Guid HistoryID { get; set; }
        public string Operator { get; set; }
        public DateTime OperateTime { get; set; }
    }
}
