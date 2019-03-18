using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.RefCheck.Models
{
    public class RefCheckLexisNexisBatchConfirmationV2
    {
        public string SessionKey { get; set; }
        public string BatchId { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
