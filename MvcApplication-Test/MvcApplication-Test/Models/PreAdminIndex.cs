using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication_Test.Models
{
	public class PreAdminIndex
	{
        public int id { get; set; }
        public string PName { get; set; }
        public string PCity { get; set; }
        public string MchineNum { get; set; }
        public string Salesman { get; set; }
        public string StartTime { get; set; }
        public string SalesPhone { get; set; }
        public string SubmitTime { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public Nullable<int> IsDelete { get; set; }
        public Nullable<int> IsNow { get; set; }

	}
}