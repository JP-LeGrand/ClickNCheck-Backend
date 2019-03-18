using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.RefCheck.Models
{
    public class RefCheckLexisNexisCheckResponseV2
    {
        public string LexisNexisReference { get; set; }
        public bool IsSuccessful { get; set; }
        public List<RefCheckLexisNexisError> Errors { get; set; }
    }
}
