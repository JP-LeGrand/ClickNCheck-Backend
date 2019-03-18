using ClickNCheck.RefCheck.DocType;
using System;

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
        public EmploymentTypeCode EmploymentTypeCode { get; set; }
        public string LastPositionHeld { get; set; }
        public string RefereeContactNumber { get; set; }
        public string RefereeName { get; set; }
        public Region Province { get; set; }
        public string StaffEmployeeNumber { get; set; }
    }
}
