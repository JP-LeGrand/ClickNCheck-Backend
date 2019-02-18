using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Models
{
    public class JobProfile_Vendor
    {
        public int JobProfileID { get; set; }
        public JobProfile JobProfile { get; set; }
        

        public int VendorId { get; set; }
        public Vendor Vendor { get; set; }

        public int Order { get; set; }
    }
}
