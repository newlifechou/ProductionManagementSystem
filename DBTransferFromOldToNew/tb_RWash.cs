//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBTransferFromOldToNew
{
    using System;
    using System.Collections.Generic;
    
    public partial class tb_RWash
    {
        public int PlanID { get; set; }
        public int WashID { get; set; }
        public Nullable<System.DateTime> WD_W { get; set; }
        public string WM_W { get; set; }
        public Nullable<System.DateTime> WD_MR { get; set; }
        public string WM_MR { get; set; }
        public string DR1 { get; set; }
        public string DR2 { get; set; }
        public string DR3 { get; set; }
        public string DR4 { get; set; }
        public string WM_D { get; set; }
        public Nullable<System.DateTime> WD_D { get; set; }
        public string UW1 { get; set; }
        public string UW2 { get; set; }
        public string UDensityT { get; set; }
        public string DW1 { get; set; }
        public string DW2 { get; set; }
        public string DDensityT { get; set; }
    
        public virtual tb_Plan tb_Plan { get; set; }
    }
}
