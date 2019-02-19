using System.ComponentModel.DataAnnotations;

namespace ClickNCheck.Models
{
    public class ContactPerson
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; } 
        public string Email { get; set; }       
    }
}