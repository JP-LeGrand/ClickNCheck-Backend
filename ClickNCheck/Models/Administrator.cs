using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Models
{
    public class Administrator
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
        public string Password { get; set; }

        public int OrganisationID { get; set; }
        public Organisation Organisation { get; set; }

        [ForeignKey("ManagerID")]
        public Manager Manager { get; set; }
    }
}
