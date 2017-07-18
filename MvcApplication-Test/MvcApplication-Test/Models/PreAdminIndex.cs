using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication_Test.Models
{
	public class PreAdminIndex
	{
        public int id { get; set; }
        public string PjName { get; set; }
        public string pjCity { get; set; }
        public string PjXSName { get; set; }
        public string MchineNum { get; set; }
        public string DateTime { get; set; }
        public string Phone { get; set; }
         public int? IsDealer { get; set; }
        public int? IsNow { get; set; }

	}
}