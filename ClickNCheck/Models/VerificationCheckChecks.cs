using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Models
{
    public class VerificationCheckChecks
    {
        public int ID { get; set; }

        public int VerificationCheckID { get; set; }
        public VerificationCheck VerificationCheck { get; set; }
        public int ServicesID { get; set; }
        public Services Services { get; set; }
        public int Order { get; set; }
    }
}
