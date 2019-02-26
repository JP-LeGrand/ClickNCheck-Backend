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
        private Candidate candidate;
        // list of selected credit vendor checks to run
        ClickNCheckContext _context;

        // results from the apis
        private JObject results;

        public Credit( ClickNCheckContext context, Candidate candidate )
        {
            _context = context;
            this.candidate = candidate;
       
            results = new JObject();
        }
        public async Task<JObject> runAllSelectedCreditChecks(List<int> selectedCreditVendorServiceID)
        {
            foreach (int id in selectedCreditVendorServiceID)
            {
                var serv = await _context.Services.FindAsync(id);
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
            else throw new Exception("Credit check either still in progress or never ran!");
        }

        private async void runCreditCheck(int apiType, string url, string supplierName)
        {
            try
            {
                ConnectToAPI apiConnection = new ConnectToAPI();
                string res = await apiConnection.runCheck(apiType, url, candidate);
                results.Add(supplierName, res);
            }
            catch(Exception) { /*connection problems*/ }
        }
        
    }
}