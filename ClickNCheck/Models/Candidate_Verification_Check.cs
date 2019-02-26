using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Models
{
    public class Candidate_Verification_Check
    {
        public int ID { get; set; }

        public int Candidate_VerificationID { get; set; }
        public Candidate_Verification Candidate_Verification { get; set; }

        public int ServicesID { get; set; }
        public Services Services { get; set; }

        public int CheckStatusTypeID { get; set; }
        public CheckStatusType CheckStatusType { get; set; }
    }
}
