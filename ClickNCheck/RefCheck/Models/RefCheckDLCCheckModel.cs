using ClickNCheck.Models;
using ClickNCheck.RefCheck.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.RefCheck.Models
{
    public class RefCheckDLCCheckModel : IDriversCheck
    {
        public string CheckType { get; set; }
        public Candidate candidate { get; set; }
    }
}
