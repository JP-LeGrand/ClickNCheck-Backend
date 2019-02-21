using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Models
{
    public class Organisation
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string RegistrationNumber { get; set; }
        public string CostCentreNumber { get; set; }
        public string TaxNumber { get; set; }
        [ForeignKey("AccountsPersonID")]
        public AccountsPerson AccountsPerson { get; set; }
        public string ContractUrl { get; set; }
        public ContactPerson ContactPerson { get; set; }
        [ForeignKey("PhysicalAddressID")]
        public Address PhysicalAddress { get; set; }
        [ForeignKey("BillingAddressID")]
        public Address BillingAddress { get; set; }

    }
}
