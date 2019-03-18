using ClickNCheck.RefCheck.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.RefCheck.Models
{
    public class RefCheckITCCheck : IRefCheckCreditCheck
    {
        public string CheckKey { get; set; }
        public string EnquiryReasonCode { get; set; }
        public string EnquirerName { get; set; }
        public string EnquirerContactNumber { get; set; }

        public RefCheckITCCheck(string CheckKey, string EnquiryReasonCode, string EnquirerName, string EnquirerContactNumber)
        {
            this.CheckKey = CheckKey;
            this.EnquiryReasonCode = EnquiryReasonCode;
            this.EnquirerName = EnquirerName;
            this.EnquirerContactNumber = EnquirerContactNumber;
        }
    }
}
