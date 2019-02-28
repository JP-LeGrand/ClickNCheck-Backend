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
    public class Candidate
    {
        public int ID { get; set; }
        
        public string Name { get; set; }
        
        public string ID_Passport { get; set; }
        
        public string ID_Type { get; set; }
        public string PictureUrl { get; set; }
        public string Surname { get; set; }
        public bool isVerified { get; set; }
        public string Maiden_Surname { get; set; }
        public string Email { get; set; }
        public bool HasConsented { get; set; }
        public string Phone { get; set; }
        
        [ForeignKey("OrganisationID")]
        public Organisation Organisation { get; set; }
        
        public ICollection<Recruiter_Candidate> Recruiter_Candidate { get; set; } = new List<Recruiter_Candidate>();
        
        public ICollection<Candidate_VerificationRequest> Candidate_JobProfile { get; } = new List<Candidate_VerificationRequest>();
    }
}
