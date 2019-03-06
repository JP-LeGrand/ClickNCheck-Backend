using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Models
{
    public class Candidate_Verification
    {
        public int ID { get; set; }

        public int CandidateID { get; set; }
        public Candidate Candidate { get; set; }

        public int VerificationID { get; set; }
        public VerificationCheck VerificationCheck { get; set; } 
    }
}
