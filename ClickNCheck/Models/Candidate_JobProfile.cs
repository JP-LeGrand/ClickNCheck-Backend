using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Models
{
    public class Candidate_JobProfile
    {
        public int JobProfileId { get; set; }
        public JobProfile JobProfile { get; set; }

        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }


    }
}
