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

    public class User
    {
   

        public int ID { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int EmployeeNumber { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Otp { get; set; }
        public string PictureUrl { get; set; }
        public int ManagerID { get; set; }
        public int OrganisationID { get; set; }
        public Organisation Organisation { get; set; }
        public int LinkCodeID { get; set; }
        public LinkCode LinkCode { get; set; }
        
        public ICollection<Recruiter_JobProfile> Recruiter_JobProfile { get; set; } = new List<Recruiter_JobProfile>();
        
        public ICollection<Recruiter_Candidate> Recruiter_Candidate { get; set; } = new List<Recruiter_Candidate>();
        
        public ICollection<Roles> Roles { get; set; } = new List<Roles>();

    }
}
