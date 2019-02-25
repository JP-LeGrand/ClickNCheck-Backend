using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ClickNCheck.Models
{
    public class UserType
    {
        public int ID { get; set; }
        public string Type { get; set; }

        
        public ICollection<Roles> Roles { get; set; } = new List<Roles>();
    }
}