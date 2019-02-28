using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Models
{
    public class Candidate_JobProfile
    {
        public int CandidateID { get; set; }
        public Candidate Candidate { get; set; }

        public int JobProfileID { get; set; }
        public JobProfile JobProfile { get; set; }
    }
}
