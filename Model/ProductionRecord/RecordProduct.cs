﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 产品库存表
    /// </summary>
    public class Product
    {
        public Guid ID { get; set; }
        public string Lot { get; set; }
        public string Composition { get; set; }
        public string CompositionAbbr { get; set; }
        public string Size { get; set; }
        public string Customer { get; set; }
        public string PO { get; set; }
        public string Density { get; set; }
        public string Weight { get; set; }
        public string XRFComposition { get; set; }
        public string ExtraInformation { get; set; }

        public string Dimension { get; set; }

        public string Position { get; set; }//入库库房编号，位置编号，unknown，成品库房 产品架A
        public string CurrentStatus { get; set; }//在库房，已发货，报废


        //当前记录状态，有效，无效
        public int CurrentState { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
