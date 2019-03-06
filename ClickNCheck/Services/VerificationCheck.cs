using ClickNCheck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Services
{
    public class VerificationCheck
    {
        //-------------------------------------------------------------------------------------------------------------------#

        private readonly JobProfile jobProfileInstanceWithOrderedChecks;
        private readonly int verificationCheckID;
        private Queue<Candidate> listOfCandidatesToCheckAgainst;
        private Queue<Models.Services> orderedJobProfileServices;

        //-------------------------------------------------------------------------------------------------------------------#

        public VerificationCheck(int verificationCheckId, JobProfile jobProfWithOrderedServices, Queue<Candidate> candidates)
        {
            verificationCheckID = verificationCheckId;
            jobProfileInstanceWithOrderedChecks = jobProfWithOrderedServices;
            listOfCandidatesToCheckAgainst = new Queue<Candidate> ( candidates );
            orderedJobProfileServices = new Queue<Models.Services>();
        }

        //-------------------------------------------------------------------------------------------------------------------#

        public Queue<Models.Services> fetchServicesQueue()
        {
            if(orderedJobProfileServices.Count() <1)
            {
                ICollection<JobProfile_Checks> ne = jobProfileInstanceWithOrderedChecks.JobProfile_Check;

                foreach (JobProfile_Checks check in ne)
                    orderedJobProfileServices.Enqueue(check.Services);
            }
            return orderedJobProfileServices;
        }

        //-------------------------------------------------------------------------------------------------------------------#

        public Queue<Candidate> getCandidatesQueue()
        {
            //gets you the list of candidates still on the que for this job profile
            return listOfCandidatesToCheckAgainst;
        }

        //-------------------------------------------------------------------------------------------------------------------#

        public Models.Services GetNextService()
        {
            return orderedJobProfileServices.Dequeue();
        }

        //-------------------------------------------------------------------------------------------------------------------#

        public int getVerificationCheckID()
        {
            return verificationCheckID;
        }

        //-------------------------------------------------------------------------------------------------------------------#
    }
}
