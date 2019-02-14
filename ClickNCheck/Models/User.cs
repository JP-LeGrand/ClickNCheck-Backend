using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Models
{
    public enum UserTypes
    {
        Administrator, Recruiter, Manager
    }


    public class User
    {
        
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public int EmployeeNumber { get; set; }
     
        public string Password { get; set; }
       
        public string Salt { get; set; }
        [Required]
        public UserTypes UserType { get; set; }

        public int Otp { get; set; }

        
        public int ManagerID { get; set; }

        public User user { get; set; }
        public int OrganisationID { get; set; }
        public Organisation Organisation { get; set; }

        public ICollection<Recruiter_JobProfile> Recruiter_JobProfile { get; set; } = new List<Recruiter_JobProfile>();
    }
}
