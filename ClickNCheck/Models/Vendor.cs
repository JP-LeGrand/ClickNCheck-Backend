using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ClickNCheck.Models
{
    public class Vendor
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ServicesID { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<JobProfile_Vendor> JobProfile_Vendor { get; set; } = new List<JobProfile_Vendor>();
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Vendor_Category> Vendor_Category { get; set; } = new List<Vendor_Category>();


    }
}
