﻿using System;
using System.Threading.Tasks;
using ClickNCheck.Data;
using ClickNCheck.Models;
using ClickNCheck.Services;
using Newtonsoft.Json.Linq;

namespace checkStub
{
    public class Academic
    {
        private readonly Candidate candidate;
        private ClickNCheckContext _context;

        private JObject results;

        public Academic(ClickNCheckContext context, int candidateId)
        {
            _context = context;
            results = new JObject();

            candidate = _context.Candidate.Find(candidateId);
            if (candidate == null)
                throw new Exception("Candidate not found in database");

        }

        public async Task<bool> RunChecks(JArray selectedServiceID)
        {
            foreach (int id in selectedServiceID)
            {
                Services serv = null;
                serv = await _context.Services.FindAsync(id);

                if (serv != null)
                    runAcademicCheck(serv.APIType, serv.URL, serv.Name);
                else
                    throw new Exception("Service not found in database");
            }
            return true;
        }
        private async void runAcademicCheck(int apiType, string url, string supplierName)
        {
            ConnectToAPI apiConnection = new ConnectToAPI();
            string res = await apiConnection.runCheck(apiType, url, candidate);

            if (res != null)
                results.Add(supplierName, res);
            else
                runAcademicStub(apiType, url, supplierName);
        }
        
        private void runAcademicStub(int apiType, string url, string servicename )
        {
            string servicetype = apiType == 0 ? "soap" : "rest";
            results.Add(servicename, $"from url: {url}. This is the response. The service type was {servicetype}.");
        }

        public JObject getResults()
        {
            if (results.Count > 0)
            {
                return results;
            }
            else throw new Exception("Acedemic results empty!");
        }

    }
}