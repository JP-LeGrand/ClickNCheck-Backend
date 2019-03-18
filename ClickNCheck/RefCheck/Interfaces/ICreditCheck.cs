using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.RefCheck.Interfaces
{
    interface IRefCheckCreditCheck
    {
        string CheckKey { get; set; }
        string EnquiryReasonCode { get; set; }
        string EnquirerName { get; set; }
        string EnquirerContactNumber { get; set; }
    }
}
