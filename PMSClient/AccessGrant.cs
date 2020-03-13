﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient
{
    public static class AccessGrant
    {
        public static string[] Sample
        {
            get
            {
                return new string[] { "管理员", "统筹组", "生产经理", "热压组", "制粉组", "加工组" };
            }

        }
        public static string[] SampleEdit
        {
            get
            {
                return new string[] { "管理员", "统筹组" };
            }

        }
        public static string[] ViewOutsideProcess
        {
            get
            {
                return new string[] { "管理员", "统筹组", "生产经理", "热压组" };
            }

        }
        public static string[] EditOutsideProcess
        {
            get
            {
                return new string[] { "管理员", "统筹组", "生产经理", "加工组", "发货组",
                    "主管", "测试组","热压组" };
            }
        }
        public static string[] ViewExpressTrackAtLogin
        {
            get
            {
                return new string[] { "管理员", "统筹组" };
            }
        }
        public static string[] ViewDeliveryItemList
        {
            get
            {
                return new string[] { "管理员", "统筹组", "生产经理", "发货组",
                    "主管", "测试组","热压组" };
            }
        }
    }
}
