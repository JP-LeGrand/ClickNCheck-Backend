using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClickNCheck.Data;
using ClickNCheck.Models;
using ClickNCheck.Services;
using Newtonsoft.Json.Linq;

namespace checkStub
{
    public class Credit
    {
        // candidate to be checked credit report against.
        private readonly int candidateID;
        // list of selected credit vendor checks to run
        ClickNCheckContext _context;

        // results from the apis
        private JObject results;

        public Credit( ClickNCheckContext context, int candidateID )
        {
            _context = context;
            this.candidateID = candidateID;
       
            results = new JObject();
        }
        public async Task<JObject> runAllSelectedCreditChecks(JArray selectedCreditVendorServiceID)
        {
            foreach (int id in selectedCreditVendorServiceID)
            {
                Services serv = null;
                serv = await _context.Services.FindAsync(id);
                if (serv != null)
                    runCreditCheck(serv.APIType, serv.URL, serv.Name);
            }
            return getResults();
        }

        private JObject getResults()
        {
            if (results.Count > 0)
            {
                return results;
            }
            else return results;
        }

        private async void runCreditCheck(int apiType, string url, string supplierName)
        {
            try
            {
                ConnectToAPI apiConnection = new ConnectToAPI();
                Candidate cnd = await _context.Candidate.FindAsync(candidateID);
                string res = await apiConnection.runCheck(apiType, url, cnd);
                results.Add(supplierName, res);
            }
            catch(Exception) { /*connection problems*/ }
        }
        
    }
}