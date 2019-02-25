using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ClickNCheck.Models
{
    public class JobProfile
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string JobCode { get; set; }
        public bool isCompleted { get; set; }
        public bool checksNeedVerification { get; set; }
        [ForeignKey("OrganisationID")]
        public Organisation Organisation  { get; set; }
        
        public ICollection<Recruiter_JobProfile> Recruiter_JobProfile { get; set; } = new List<Recruiter_JobProfile>();
        public ICollection<JobProfile_Vendor> JobProfile_Vendor { get; set; } = new List<JobProfile_Vendor>();
        public ICollection<VerificationRequest> VerificationRequest { get; set; } = new List<VerificationRequest>();
    }
}
