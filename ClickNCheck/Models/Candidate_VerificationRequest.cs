using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Models
{
    public class Candidate_VerificationRequest
    {
        public int VerificationRequestId { get; set; }
        public VerificationRequest VerificationRequest { get; set; }

        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }


    }
}
