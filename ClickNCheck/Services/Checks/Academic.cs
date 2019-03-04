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

        //private JObject results;

        public Academic(ClickNCheckContext context, int candidateId)
        {
            _context = context;
            //results = new JObject();

            candidate = _context.Candidate.Find(candidateId);
            if (candidate == null)
                throw new Exception("Candidate not found in database");

        }

        public async Task<JObject> RunChecks(JArray selectedServiceID)
        {
            JObject response = new JObject();
            foreach (int id in selectedServiceID)
            {
                Services serv = null;
                serv = await _context.Services.FindAsync(id);

                if (serv != null)
                {
                    JObject res = await runAcademicCheck(serv.APIType, serv.URL, serv.Name);
                    response.Add(serv.Name, res);
                }
                
                else
                    throw new Exception("Service not found in database");
            }
            return response;
        }
        private async Task<JObject> runAcademicCheck(int apiType, string url, string supplierName)
        {
            ConnectToAPI apiConnection = new ConnectToAPI();
            string res = await apiConnection.runCheck(apiType, url, candidate);

            if (res != null)
            {
                JObject rs = new JObject();
                rs.Add(supplierName, res);
                return rs;
            }
                
            else
                return runAcademicStub(apiType, url, supplierName);
        }
        
        private JObject runAcademicStub(int apiType, string url, string servicename )
        {
            string servicetype = apiType == 0 ? "soap" : "rest";
            JObject rs = new JObject();
            rs.Add( servicename, $"from url: {url}. This is the response. The service type was {servicetype}." );
            return rs;
        }
    }
}