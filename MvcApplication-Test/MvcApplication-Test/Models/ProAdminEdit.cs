using MvcApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication_Test.Models
{
	public class ProAdminEdit
	{
        public ProAdminEdit()
        {
            Pro = new PreAdminIndex();
            List = new List<machineinfo>();
            Machine = new machineinfo();
        }
        public PreAdminIndex Pro { get; set; }
        public List<machineinfo> List { get; set; }
        public machineinfo Machine { get; set; }
	}
}