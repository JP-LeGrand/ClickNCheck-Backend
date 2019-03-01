using System;
using System.Threading.Tasks;
using ClickNCheck.Data;
using ClickNCheck.Models;
using ClickNCheck.Services;
using Newtonsoft.Json.Linq;

namespace checkStub
{
    public class Identity
    {
        private readonly int candidateID;
        ClickNCheckContext _context;
        // members of the criminal class to be checked
        private bool names;
        private bool idNumber;
        private bool maritalStatus;
        private bool deceaseStatus;

        //
        private bool inProgress;

        //
        private bool checkRan;

        //
        private JObject results;
        

        public Identity(ClickNCheckContext context, int candidateId)
        {
            _context = context;
            candidateID = candidateId;

            this.results = new JObject();
        }

        public async Task<JObject> runAllSelectedIdentityChecks(JArray selectedServiceID)
        {
            try
            {
                foreach (int id in selectedServiceID)
                {
                    Services serv = null;
                    serv = await _context.Services.FindAsync(id);
                    if (serv != null)
                        RunIdentityCheck(serv.APIType, serv.URL, serv.Name);
                    else { throw new Exception("Could not find that service in our database"); }
                }

            }
            catch (Exception)
            {
                stubIdentity();
            }
            return getResults();
        }

        public JObject getResults()
        {
            if (results.Count > 0)
            {
                return results;
            }
            // you might want to use promises
            else throw new Exception("Either still in progress or not ran!");
        }
        private async void RunIdentityCheck(int apiType, string url, string supplierName)
        {
            
            ConnectToAPI apiConnection = new ConnectToAPI();
            Candidate candidate = await _context.Candidate.FindAsync(candidateID);
            string res = await apiConnection.runCheck(apiType, url, candidate);
            results.Add(supplierName, res);
            
        }

        private void stubIdentity()
        {
            runNamesCheck();
            runIdNumberCheck();
            runMaritalStatusCheck();
            runDeceaseStatusCheck();
        }

        //this method should be asychronous
        private void runNamesCheck()
        {
            results.Add("names", "Njabulo Peter Zuma");
        }

        //this method should be asychronous
        private void runIdNumberCheck()
        {
            results.Add("id","880201256321021504");
        }

        //this method should be asychronous
        private void runMaritalStatusCheck()
        {
            results.Add("maritalStatus","Single");
        }

        //this method should be asychronous
        private void runDeceaseStatusCheck()
        {
            results.Add("deceaseStatus","Alive");
        }
    }
}
