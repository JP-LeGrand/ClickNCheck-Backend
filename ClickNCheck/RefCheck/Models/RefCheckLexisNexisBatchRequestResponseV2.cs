using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.RefCheck.Models
{
    public class RefCheckLexisNexisBatchRequestResponseV2
    {
        public List<string> BatchItems { get; set; }
        public bool IsSuccessful { get; set; }
        public List<RefCheckLexisNexisError> Errors { get; set; }
    }
}
