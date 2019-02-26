using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Models
{
    public class JobProfile_Checks
    {
        public int JobProfileID { get; set; }
        public JobProfile JobProfile { get; set; }
        

        public int ServicesID { get; set; }
        public Services Services { get; set; }

        public int Order { get; set; }
    }
}
