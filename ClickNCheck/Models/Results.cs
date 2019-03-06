using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Models
{
    public class Results
    {
        public int ID { get; set; }
        public int CheckID { get; set; }
        public string resultStatus {get;set;}
        public string resultDescription { get; set; }
        public string resultFilesURL { get; set; } //This will be a URL to the files
    }
}
