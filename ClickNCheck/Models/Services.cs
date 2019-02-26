using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Models
{
    public class Services
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Cost  { get; set; }
        public string TurnaroundTime { get; set; }
        public string URL { get; set; }
        public bool isAvailable { get; set; }
        public int APIType { get; set; }
        public string APIUserName { get; set; }
        public int VendorID { get; set; }
        public Vendor Vendor { get; set; }
        public string APIPassword { get; set; }
        public int CheckCategoryID { get; set; }
        public CheckCategory CheckCategory { get; set; }
    }
}
