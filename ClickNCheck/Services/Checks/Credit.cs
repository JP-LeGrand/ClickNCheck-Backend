using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClickNCheck.Data;
using ClickNCheck.Models;
using ClickNCheck.Services;
using Newtonsoft.Json.Linq;

namespace checkStubClasses
{
    public class Credit
    {
        // candidate to be checked credit report against.
        private Candidate candidate;
        // list of selected credit vendor checks to run
        private List<int> selectedCreditVendorID;

        // results from the apis
        private JObject results;

        // if all offered by 1 api, then use only one connection
        // instantiate it in the constructor
        // Connection connection;

        public Credit(Candidate candidate, List<int> creditVendorID)
        {
            this.candidate = candidate;
            selectedCreditVendorID = creditVendorID;

            results = new JObject();
        }

        public async Task<JObject> runCreditCheck()
        {
            try
            {
                foreach (int id in selectedCreditVendorID)
                {
                    //fetch vendor name with the id
                    //string name = creditVendorID.get
                    //vendor credit-services, thats where you will find the url
                    int apiType; string vendorName; string url;

                    ConnectToAPI apiConnection = new ConnectToAPI();
                    string res = await apiConnection.runCheck(apiType, url, candidate);
                    results.Add(vendorName, res);
                }
                return getResults();
            }
            catch(Exception e) { return new JObject { { "Failed", $"Connection problems. {e.Source}" } };/*connection problems*/ }
        }

        private JObject getResults()
        {
            if (results.Count > 0)
            {
                return results;
            }
            else throw new Exception("Credit check either still in progress or never ran!");
        }
        
    }
}