using ClickNCheck.Data;
using ClickNCheck.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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

        public void ExecuteCheck(int candidateID, Models.Services service)
        {
            int apiType = service.APIType;
            string apiURL = service.URL;
            var candidate = _context.Candidate.Find(candidateID);
            if (apiType == 0) //SOAP
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(apiURL);
                webRequest.ContentType = "application/json";
                webRequest.Method = "POST";
                webRequest.KeepAlive = true;


                byte[] body = Encoding.UTF8.GetBytes(candidate.Name + " " + candidate.Surname);
                Stream newStream = webRequest.GetRequestStream();
                webRequest.Accept = "application/xrds+xml";
                webRequest.ContentLength = body.Length;
                newStream.Write(body, 0, body.Length);
                newStream.Close();
                SOAPResponse soapResponse = new SOAPResponse();
                WebResponse response = webRequest.GetResponse();
                WebHeaderCollection header = response.Headers;
                
                using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    string responseXml = reader.ReadToEnd();
                    soapResponse.Process(responseXml);
                }
                
            }
            else if (apiType == 1) //REST
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(apiURL);
                webRequest.ContentType = "application/json";
                webRequest.Method = "POST";
                webRequest.KeepAlive = true;

                dynamic candidateDetails = new JObject();
                candidateDetails.name = candidate.Name;
                candidateDetails.surname = candidate.Surname;
                candidateDetails.id = candidate.ID;


                byte[] body = Encoding.UTF8.GetBytes(candidateDetails.ToString());
                Stream newStream = webRequest.GetRequestStream();
                webRequest.Accept = "application/json";
                webRequest.ContentLength = body.Length;
                newStream.Write(body, 0, body.Length);
                newStream.Close();
                RestResponseService restResponse = new RestResponseService();
                WebResponse response = webRequest.GetResponse();
                WebHeaderCollection header = response.Headers;

                using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    JObject responseJson = JObject.Parse(reader.ReadToEnd());
                    restResponse.Process(responseJson);
                }
            }
            else if (apiType == 3) //LONG RUNNING
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(apiURL);
                webRequest.Method = "POST";
                webRequest.KeepAlive = true;
                webRequest.GetResponse();
            }
            else if (apiType == 2) //LONG RUNNING
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(apiURL);
                webRequest.Method = "GET";
                webRequest.KeepAlive = true;
                webRequest.GetResponse();
            }
        }

        public void AutomateChecks()
        {/*
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
            }*/
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
