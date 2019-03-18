using ClickNCheck.RefCheck.DocType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.RefCheck.Models
{
    public class RefCheckCriminalCheck
    {
        public RefCheckCriminalCheck(int checkKey, string country, string gender, string population, string searchTypes, string residentialAddress1, string residentialAddress2, string fingerPrints, string previousCharges)
        {
            this.CheckKey = checkKey;
            this.Country = country;
            this.Gender = gender;
            this.Population = population;
            this.SearchTypes = searchTypes;
            this.ResidentialAddress1 = residentialAddress1;
            this.ResidentialAddress2 = residentialAddress2;
            this.FingerPrintsTakenAt = fingerPrints;
            this.PreviousCharges = previousCharges;

        }

        public int CheckKey { get; set; }
        public string Country { get; set; }
        //Ideco Gender Codes
        public string Gender { get; set; }
        //Ideco Population groups
        public string Population { get; set; }
        //Ideco Search Types codes
        public string SearchTypes { get; set; }
        //Current residential address lines
        public string ResidentialAddress1 { get; set; }
        public string ResidentialAddress2 { get; set; }
        //Indicating where the finger prints where taken
        public string FingerPrintsTakenAt { get; set; }
        //If no charges exist then the value should be NO.
        public string PreviousCharges { get; set; }
    }
}
