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

        private ClickNCheckContext _context;

        //-------------------------------------------------------------------------------------------------------------------#

        public VerificationChecking()
        {
            _context = new ClickNCheckContext();
        }

        //-------------------------------------------------------------------------------------------------------------------#

        public void AutomateChecks()
        {
            List<VerificationCheck> verificationcheck = _context.VerificationCheck.ToList();
            foreach (VerificationCheck ver in verificationcheck)
            {
                //isComplete isn't enough, we need another field
                //to indicate the status of the check, how do you
                //ensure you dont run a same verification check twice?

                if (ver.IsAuthorize && !ver.IsComplete)
                {
                    List<Candidate_Verification> jobVerCandidates = ver.Candidate_Verification.ToList();
                    List<Candidate> jobCandidates = new List<Candidate>();

                    foreach (Candidate_Verification jobVer in jobVerCandidates)
                        jobCandidates.Add(jobVer.Candidate);

                    Job newJobVerification = new Job(ver.ID, ver.JobProfile, jobCandidates);
                    //TODO
                    //run the methods you need from the Job object
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------#

        private class Job
        {

            private JobProfile jobProfileInstanceWithOrderedChecks;
            private int verificationCheckID;
            private List<Candidate> listOfCandidatesToCheckAgainst;
            private Queue<Models.Services> orderedJobProfileServices;

            //-------------------------------------------------------------------------------------------------------------------#

            public Job(int verificationCheckId, JobProfile jobProfWithOrderedServices, List<Candidate> candidates)
            {
                verificationCheckID = verificationCheckId;
                jobProfileInstanceWithOrderedChecks = jobProfWithOrderedServices;
                listOfCandidatesToCheckAgainst = new List<Candidate>(candidates);
                orderedJobProfileServices = new Queue<Models.Services>();
            }

            //-------------------------------------------------------------------------------------------------------------------#

            public Queue<Models.Services> fetchServicesQueue()
            {
                if (orderedJobProfileServices.Count() < 1)
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
}
