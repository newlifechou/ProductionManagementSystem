﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    /// <summary>
    /// 热压设备
    /// </summary>
    public class BDVHPDevice
    {
        public Guid ID { get; set; }
        public string CodeName { get; set; }
        public string DeviceInformation { get; set; }
        public double HighestTemperature { get; set; }
        public double HighestPressure { get; set; }
        public double HighestDiameter { get; set; }
        public string State { get; set; }
        public string Manufacturer { get; set; }//制造商
        public string ReceiveTime { get; set; }//接受时间
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }


    }
}
