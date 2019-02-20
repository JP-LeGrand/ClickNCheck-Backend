using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Models
{
    public class Vendor
    {
        public int ID { get; set; }
        public string Name { get; set; } 
        

        
        public ICollection<JobProfile_Vendor> JobProfile_Vendor { set; get; } = new List<JobProfile_Vendor>();
        public ICollection<Vendor_Category> Vendor_Category { set; get; } = new List<Vendor_Category>();
    }
}
