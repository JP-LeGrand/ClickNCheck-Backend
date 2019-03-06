using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Models
{
    public class CheckStatus
    {
        enum Status
        {
            Cleared,
            PossibleIssues,
            Failed,
            InProgress,
            NotStarted
        }
    }
}
