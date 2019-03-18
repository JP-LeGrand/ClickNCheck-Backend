using ClickNCheck.RefCheck.DocType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.RefCheck.Models
{
    public class CriminalCheckRefCheck
    {
        public int CheckKey { get; set; }
        public string Country { get; set; }
        public IdecoGenderCodes Gender { get; set; }
        public IdecoPopulationGroupCodes Population { get; set; }
        public IdecoSearchTypeCodes SearchTypes { get; set; }
        //Current residential address lines
        public string ResidentialAddress1 { get; set; }
        public string ResidentialAddress2 { get; set; }
        //Indicating where the finger prints where taken
        public string FingerPrintsTakenAt { get; set; }
        //If no charges exist then the value should be NO.
        public string PreviousCharges { get; set; }
    }
}
