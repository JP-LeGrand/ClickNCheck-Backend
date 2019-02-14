using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Models
{
    public class Checks
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string TurnaraoundTime { get; set; }

        
        public ICollection<JobProfile_Checks> JobProfile_Checks { get; } = new List<JobProfile_Checks>();
    }
}
