using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClickNCheck.Models;

namespace ClickNCheck.RefCheck.Interfaces
{
    interface IDriversCheck
    {
       string CheckType { get; set; }
       Candidate candidate { get; set; }
    }
}
