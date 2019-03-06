using ClickNCheck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Services
{
    public class VerificationCheck
    {
        private readonly JobProfile jobProfileInstanceWithOrderedChecks;
        private readonly Queue<Candidate> listOfCandidates;
        private readonly Queue<Models.Services> orderedJobProfileServices;

        public VerificationCheck(JobProfile jobProfWithOrderedServices, Queue<Candidate> candidates)
        {
            jobProfileInstanceWithOrderedChecks = jobProfWithOrderedServices;
            listOfCandidates = candidates;
        }

        public bool AddToCandidatesQueue()
        {
            /*
             * if successfully added to the que then return true,
             * otherwise return false.
             */
            return true;
        }
    }
}
