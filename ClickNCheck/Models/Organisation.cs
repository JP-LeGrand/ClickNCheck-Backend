using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Models
{
    public class Organisation
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string RegistrationNumber { get; set; }
        [Required]
        public string TaxNumber { get; set; }
        [Required]
        public ContactPerson ContactPerson { get; set; }
        
        [Required]
        public PhysicalAddress PhysicalAddress { get; set; }
        [Required]

        public BillingAddress BillingAddress { get; set; }

    }
}
