﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.Express
{
    public class Request
    {
        public Request()
        {

        }
        public Request(string ordercode, string shippercode, string logisticode)
        {
            OrderCode = ordercode;
            ShipperCode = shippercode;
            LogisticCode = logisticode;
            CustomerName = "";
        }
        public Request(string ordercode, string customername, string shippercode, string logisticode)
        {
            OrderCode = ordercode;
            ShipperCode = shippercode;
            LogisticCode = logisticode;
            CustomerName = customername;
        }
        public string OrderCode { get; set; }
        public string CustomerName { get; set; }
        public string ShipperCode { get; set; }
        public string LogisticCode { get; set; }

    }
}
