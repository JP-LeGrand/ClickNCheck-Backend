using System;
using System.Threading.Tasks;
using ClickNCheck.Data;
using ClickNCheck.Models;
using ClickNCheck.Services;
using Newtonsoft.Json.Linq;

namespace checkStub
{
    public class Academic
    {
        private ClickNCheckContext _context;
        private readonly int candidateID;

        // results from the apis
        private JObject results;

        public Academic(ClickNCheckContext context, int candidateId)
        {
            _context = context;
            candidateID = candidateId;
            
            this.results = new JObject();
        }
        public async Task<JObject> runAllSelectedAcademicChecks(JArray selectedServiceID)
        {
            foreach (int id in selectedServiceID)
            {
                Services serv = null;
                serv = await _context.Services.FindAsync(id);
                if (serv != null)
                    RunAcademicCheck(serv.APIType, serv.URL, serv.Name);
                else { throw new Exception("Could not find that service in our database"); }
            }
            return getResults();
        }

        private async void RunAcademicCheck(int apiType, string url, string supplierName)
        {
            try
            {
                ConnectToAPI apiConnection = new ConnectToAPI();
                Candidate candidate = await _context.Candidate.FindAsync(candidateID);
                string res = await apiConnection.runCheck(apiType, url, candidate);
                results.Add(supplierName, res);
            }
            catch (Exception)
            {
                stubAcademic();   
            }
        }

        private void stubAcademic()
        {
            runHighSchoolCheck();
            runTatiaryCheck();
        }

        public JObject getResults()
        {
            if (results.Count > 0)
            {
                return results;
            }
            else throw new Exception("Acedemic check either still in progress or never ran!");
        }

        //make this an asychronous method
        private void runHighSchoolCheck()
        {
            results.Add("highSchool", "NSC, 2005, Pretoria Boys High School");
        }

        //make this an asychronous method
        private void runTatiaryCheck()
        {
            results.Add("tatiary", "BSc Applied Mathematics, 2009, University of Pretoria");
        }
        
    }
}