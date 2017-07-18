using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication.Model
{
    public class machineinfo
    {
        public int id { get; set; }
        public string machineName { get; set; }
        public Nullable<int> machineNum { get; set; }
        public string test { get; set; }
        public string other { get; set; }
        public Nullable<System.DateTime> createtime { get; set; }
        public Nullable<System.DateTime> fixtime { get; set; }
        public Nullable<int> proID { get; set; }
    }
}