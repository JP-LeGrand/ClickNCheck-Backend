using System.ComponentModel.DataAnnotations;

namespace ClickNCheck.Models
{
    public class BillingAddress
    {
        public int ID { get; set; }
        [Required]
        public string Building { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string Suburb { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string Province { get; set; }
        public int OrganisationID { get; set; }
        public Organisation Organisation { get; set; }
    }
}