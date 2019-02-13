using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Models
{
    public class JobProfile
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool isCompleted { get; set; }

        [ForeignKey("OrganisationID")]
        public Organisation Organisation  { get; set; }
        public ICollection<Recruiter_JobProfile> Recruiter_JobProfile { get; set; } = new List<Recruiter_JobProfile>();
        public ICollection<JobProfile_Checks> JobProfile_Checks { get; set; } = new List<JobProfile_Checks>();
        public ICollection<Candidate_JobProfile> Candidate_JobProfile { get; set; } = new List<Candidate_JobProfile>();
    }
}
