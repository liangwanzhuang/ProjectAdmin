//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcApplication.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class machineinfo
    {
        public int id { get; set; }
        public string MachineName { get; set; }
        public Nullable<int> MachineNum { get; set; }
        public string MachineModel { get; set; }
        public string Remark { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public Nullable<int> ProId { get; set; }
        public Nullable<int> IsDelete { get; set; }
    }
}
