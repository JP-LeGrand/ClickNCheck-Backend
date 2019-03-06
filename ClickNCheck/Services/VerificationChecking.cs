using ClickNCheck.Data;
using ClickNCheck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Services
{
    public class VerificationChecking
    {
        //-------------------------------------------------------------------------------------------------------------------#

        private int verificationCheckID;
        private ClickNCheckContext _context;

        //-------------------------------------------------------------------------------------------------------------------#

        public VerificationChecking(int id = 0)
        {
            _context = new ClickNCheckContext();
            verificationCheckID = id;
        }

        //-------------------------------------------------------------------------------------------------------------------#

        public void AutomateChecks()
        {
            if (verificationCheckID == 0)
            {
               //0 _context.VerificationCheck.
            }
            else
            {
                VerificationCheck(verificationCheckID);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------#

        public bool VerificationCheck(int id)
        {
            VerificationCheck newCheck = _context.VerificationCheck.Find(id);

            //newCheck.JobProfile;
            //id =  verification id
            return false;
        }
        
        //-------------------------------------------------------------------------------------------------------------------#

        public void SetVerificationCheckID(int id)
        {
            verificationCheckID = id;
        }

        //-------------------------------------------------------------------------------------------------------------------#



















        //-------------------------------------------------------------------------------------------------------------------#

        private readonly JobProfile jobProfileInstanceWithOrderedChecks;
        //private readonly int verificationCheckID;
        private List<Candidate> listOfCandidatesToCheckAgainst;
        private Queue<Models.Services> orderedJobProfileServices;

        //-------------------------------------------------------------------------------------------------------------------#

        public VerificationChecking(int verificationCheckId, JobProfile jobProfWithOrderedServices, List<Candidate> candidates)
        {
            verificationCheckID = verificationCheckId;
            jobProfileInstanceWithOrderedChecks = jobProfWithOrderedServices;
            listOfCandidatesToCheckAgainst = new List<Candidate> ( candidates );
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

        public List<Candidate> getCandidatesQueue()
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
