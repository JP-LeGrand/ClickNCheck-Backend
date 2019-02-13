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

        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public int Phone { get; set; }
        public string Password { get; set; }
        public int Org_ID { get; set; }

        public Organisation Organisation { get; set; }
    }
}
