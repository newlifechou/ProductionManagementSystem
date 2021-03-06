﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMSCommon
{
    /// <summary>
    /// 订单状态类型
    /// deleted不显示
    /// paused是暂停但是显示
    /// umcompleted是未完成
    /// UnDetermined是未确定，所以暂不在除了管理端的其他地方显示
    /// completed是完成
    /// </summary>
    public enum OrderState
    {
        作废,
        未核验,
        未完成,
        生产完成,
        最终完成,
        暂停,
        取消
    }
}