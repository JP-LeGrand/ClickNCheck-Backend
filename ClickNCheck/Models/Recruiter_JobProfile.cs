using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Models
{
    public class Recruiter_JobProfile
    {
        public int JobProfileId { get; set; }
        public JobProfile JobProfile { get; set; }

        public int RecruiterId { get; set; }
        public Recruiter Recruiter { get; set; }
    }
}
