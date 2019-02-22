using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ClickNCheck.Models
{
    public class CheckCategory
    {
        public int ID { get; set; }
        public string Category { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Vendor_Category> Vendor_Category { get; } = new List<Vendor_Category>();
    }
}
