using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Models
{
    public class VerificationCheck
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public bool IsComplete { get; set; }
        public bool IsAuthorize { get; set; }
        public int RecruiterID { get; set; }
        public int JobProfileID { get; set; }
        public JobProfile JobProfile { get; set; }
        public ICollection<Candidate_Verification> Candidate_Verification { get; set; } = new List<Candidate_Verification>();
    }
}
