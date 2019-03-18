using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.RefCheck.Models
{
    public class RefCheckPRCCheck
    {
        public string CheckKey { get; set; }
        public string PassportNumber { get; set; }
        public string PermitNumber { get; set; }

        public RefCheckPRCCheck(string CheckKey, string PassportNumber, string PermitNumber)
        {
            this.CheckKey = CheckKey;
            this.PassportNumber = PassportNumber;
            this.PermitNumber = PermitNumber;
        }
    }
}
