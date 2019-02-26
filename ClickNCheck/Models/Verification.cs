using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Models
{
    public class Verification
    {
        public int ID { get; set; }
        public int JobProfileID { get; set; }
        public JobProfile JobProfile { get; set; }
    }
}
