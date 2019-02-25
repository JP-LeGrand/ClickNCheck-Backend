using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Models
{
    public class VerificationRequest
    {
        public int ID { get; set; }
        public DateTime DateStarted { get; set; }
        public DateTime DateEnded { get; set; }
        public int JobProfileID { get; set; }
        public JobProfile JobProfile { get; set; }
        public ICollection<Candidate_VerificationRequest> Candidate_VerificationRequest { get; set; } = new List<Candidate_VerificationRequest>();
    }
}
