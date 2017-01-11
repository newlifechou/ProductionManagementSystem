﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMSWCFService.Models
{
    public class RoleDc
    {
        public Guid ID { get; set; }
        public string GroupName { get; set; }
        public string ExtraInformation { get; set; }
        public int State { get; set; }
        public DateTime CreateTime { get; set; }
    }
}