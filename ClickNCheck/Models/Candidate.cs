using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Models
{
    public class Candidate
    {
        public int ID { get; set; }
        
        public string Name { get; set; }
        
        public string ID_Passport { get; set; }
        
        public string ID_Type { get; set; }
        
        public string Surname { get; set; }
        
        public string Maiden_Surname { get; set; }
        
        public string Email { get; set; }
        
        public string Phone { get; set; }

        [ForeignKey("OrganisationID")]
        public Organisation Organisation { get; set; }


        public int RecruiterID { get; set; }

        public ICollection<Candidate_JobProfile> Candidate_JobProfile { get; } = new List<Candidate_JobProfile>();
    }
}
