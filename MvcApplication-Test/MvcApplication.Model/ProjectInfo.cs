using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication.Model
{
    public class ProjectInfo
    {
        public int id { get; set; }
        public string pname { get; set; }
        public string pjCity { get; set; }
        public string machineNum { get; set; }
        public Nullable<int> isNow { get; set; }
        public string pjXSName { get; set; }
        public Nullable<int> isDealer { get; set; }
        public string phone { get; set; }
        public string other { get; set; }
        public Nullable<System.DateTime> createtime { get; set; }
        public Nullable<System.DateTime> fixtime { get; set; }
    }
}