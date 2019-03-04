using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Models
{
    public class Results
    {
        public int ServicesID { get; set; }
        public Services Services { get; set; }
        public int CheckStatusID {get;set;}
        public CheckStatusType CheckStatus { get; set; }
        public string resultDescription { get; set; }
        public string resultFilesURL { get; set; } //This will be a URL to the files
    }
}
