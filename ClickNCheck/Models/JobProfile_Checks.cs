using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Models
{
    public class JobProfile_Checks
    {
        public int JobProfileId { get; set; }
        public JobProfile JobProfile { get; set; }
        

        public int ChecksId { get; set; }
        public Checks Checks { get; set; }

        public int order { get; set; }
    }
}
