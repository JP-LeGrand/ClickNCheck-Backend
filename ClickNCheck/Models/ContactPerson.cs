using System.ComponentModel.DataAnnotations;

namespace ClickNCheck.Models
{
    public class ContactPerson
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int OrganisationID { get; set; }
        [Required]
        public Organisation Organisation { get; set; }
    }
}