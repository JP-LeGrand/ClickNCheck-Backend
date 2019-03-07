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
        private Job job;
        private int verificationCheckID;

        //-------------------------------------------------------------------------------------------------------------------#

        public VerificationChecking(int verificationCheckId=3)
        {
            _context = new ClickNCheckContext();
            job = new Job(new Queue<Models.Services>(), new List<Candidate>());
            verificationCheckID = verificationCheckId;
        }

        //-------------------------------------------------------------------------------------------------------------------#

        public void doStuff()
        {
            var vc = _context.VerificationCheck.Find(this.verificationCheckID);
            var cv = _context.Candidate_Verification.Where(x => x.VerificationCheckID == vc.ID).ToList();
            vc.Candidate_Verification = cv;
            List<Candidate_Verification_Check> cvc = new List<Candidate_Verification_Check>();
            List<object> objList = new List<object>();

            //var _lst2 = from s in _context

            for (int x = 0; x < cv.Count; x++)
            {
                var _lst = (from i in _context.Candidate_Verification_Check
                            join j in _context.Candidate_Verification on i.Candidate_VerificationID equals j.ID
                            join k in _context.Candidate_Verification_Check on j.ID equals k.ID
                            where i.Candidate_Verification.VerificationCheckID == cv[x].VerificationCheckID
                            //group i by i.Order
                            select new
                            {
                                j.VerificationCheckID,
                                j.CandidateID,
                                j.Candidate.Name,
                                j.Candidate.Surname,
                                j.Candidate.Maiden_Surname,
                                j.Candidate.ID_Passport,
                                j.Candidate.ID_Type,
                                j.Candidate.HasConsented,
                                j.Candidate.Phone,
                                j.Candidate.Email,
                                CandidateVerificationID = i.ID,
                                i.ServicesID,
                                i.Order
                            }).ToList<object>().Distinct();
                objList.AddRange(_lst);
            }
        }
        //-------------------------------------------------------------------------------------------------------------------#

        private class Job
        {
            //---------------------------------------------------------------------------------------------------------------#
            
            private List<Candidate> listOfCandidatesToCheckAgainst;
            private Queue<Models.Services> orderedJobProfileServices;

            //---------------------------------------------------------------------------------------------------------------#

            private List<int> clearedCandidateID, possibleIssuesCandidateID, failedCandidateID, inProgressCandidateID, notStartedCandidateID;

            //---------------------------------------------------------------------------------------------------------------#

            public Job(Queue<Models.Services> jobProfileServices, List<Candidate> candidates)
            {
                listOfCandidatesToCheckAgainst = new List<Candidate>(candidates);
                orderedJobProfileServices = new Queue<Models.Services>(jobProfileServices);
            }

            //---------------------------------------------------------------------------------------------------------------#

            public void AutomateChecks()
            {
                while (orderedJobProfileServices.Count > 0)
                {
                    runSingleCheckPerAllCandidates( orderedJobProfileServices.Dequeue(), listOfCandidatesToCheckAgainst );
                }
            }

            //----------------------------------------------------------------------------------------------------------------#

            private void runSingleCheckPerAllCandidates(Models.Services service, List<Candidate> listOfCandidatesToCheckAgainst)
            {
                string serviceName = service.Name;

                switch (service.APIType)
                {
                    case 0:
                        //SOAP ASYNC
                        // 1 - call Chuba dummy servirce endpoint using serviceName as route;
                        // 2 - on return call Xolani's IResponseHandler;
                        break;
                    case 1:
                        //REST ASYNC
                        //1 - call Mpinane's service api endpoint using serviceName as route;
                        //2 - on return call Fiwa's IResponseHandler;
                        break;
                    case 2:
                        //LONG MAIL
                        // 1 - call Tafara's dummy servirce endpoint using serviceName as route;
                        // 2 - on return call KB IResponseHandler;
                        break;
                    case 3:
                        //LONG ENDPOINT
                        // 1 - call Chuba dummy servirce endpoint using serviceName as route;
                        // 2 - on return call Xolani's IResponseHandler;
                        break;
                    default:break;
                }
            }

            //----------------------------------------------------------------------------------------------------------------#

            public Queue<Models.Services> FetchServicesQueue()
            {
                return orderedJobProfileServices;
            }

            //----------------------------------------------------------------------------------------------------------------#

            private List<Candidate> getCandidatesList()
            {
                //gets you the list of candidates still on the que for this job profile
                return listOfCandidatesToCheckAgainst;
            }

            //----------------------------------------------------------------------------------------------------------------#

            private Models.Services GetNextService()
            {
                if (orderedJobProfileServices.Count < 1)
                    return null;
                return orderedJobProfileServices.Dequeue();
            }

            //-----------------------------------------------------------------------------------------------------------------#
        }
    }
}
