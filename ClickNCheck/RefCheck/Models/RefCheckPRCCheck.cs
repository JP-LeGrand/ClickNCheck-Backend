using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.RefCheck.Models
{
    public class RefCheckPRCCheck
    {
        public string CheckKey { get; set; }
        public string PasswportNumber { get; set; }
        public string PermitNumber { get; set; }

        public RefCheckPRCCheck(string CheckKey, string PasswportNumber, string PermitNumber)
        {
            this.CheckKey = CheckKey;
            this.PasswportNumber = PasswportNumber;
            this.PermitNumber = PermitNumber;
        }
    }
}
