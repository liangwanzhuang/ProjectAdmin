using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication.Model
{
    public class machineinfo
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