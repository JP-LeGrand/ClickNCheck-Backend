using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.RefCheck.Models
{
    public class RefCheckACACheck
    {
        public string CertificateNumber { get; set; }
        public string CheckType { get; set; }
        public string Country { get; set; }
        public string DocTypeCode { get; set; }
        public string ExaminationNumber { get; set; }
        public string Institution { get; set; }
        public string NameOnCertificate { get; set; }
        public string ProvinceCode { get; set; }
        public string Qualification { get; set; }
        public string QualificationType { get; set; }
        public string YearCompleted  { get; set; }
    }
}
