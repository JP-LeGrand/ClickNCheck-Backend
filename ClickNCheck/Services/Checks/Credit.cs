﻿using System;
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
        private readonly Candidate candidate;
        // connections to the system.
        ClickNCheckContext _context;

        // results from the apis
        private JObject results;

        public Credit( ClickNCheckContext context, int candidateId )
        {
            _context = context;
            candidate = _context.Candidate.Find(candidateId);
       
            results = new JObject();
        }
        public async Task<bool> RunChecks(JArray selectedServiceID)
        {
            foreach (int id in selectedServiceID)
            {
                Services serv = null;
                serv = await _context.Services.FindAsync(id);

                if (serv != null)
                    runCreditCheck(serv.APIType, serv.URL, serv.Name);
                else
                    throw new Exception("Service not found in database");
            }
            return true;
        }
        private async void runCreditCheck(int apiType, string url, string supplierName)
        {
            ConnectToAPI apiConnection = new ConnectToAPI();
            string res = await apiConnection.runCheck(apiType, url, candidate);

            if(res != null)
                results.Add(supplierName, res);
            else
                runCreditStub(apiType, url, supplierName);
        }

        private void runCreditStub(int aPIType, string uRL, string servicename)
        {
            string servicetype = aPIType == 0 ? "soap" : "rest";
            results.Add(servicename, $"from url: {uRL}. This is the response. The service type was {servicetype}.");
        }

        public JObject getResults()
        {
            if (results.Count > 0)
            {
                return results;
            }
            else throw new Exception("Cradit results is empty!");
        }
        
    }
}