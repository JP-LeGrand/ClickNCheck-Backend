using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.RefCheck.Models
{
    public class RefCheckEmploymentHistoryCheck
    {
        public string CheckKey { get; set; }
        public string ContactNumber { get; set; }
        public string ContactPerson { get; set; }
        public string Country { get; set; }
        public bool CurrentlyEmployed { get; set; }
        public string DepartmentRegionName { get; set; }
        public string Employer { get; set; }
        public DateTime EmploymentEndDate { get; set; }
        public DateTime EmploymentStartDate { get; set; }
        //public EmploymentTypeCode { get; set; }
    }
}
