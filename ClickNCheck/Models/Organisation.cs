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
        public string Name { get; set; }
        public string RegistrationNumber { get; set; }
        public string TaxNumber { get; set; }
        public AccountsPerson AccountsPerson { get; set; }
        public string ContractUrl { get; set; }
        public ContactPerson ContactPerson { get; set; }
        public Address PhysicalAddress { get; set; }
        public Address BillingAddress { get; set; }

    }
}
