﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClickNCheck.Data;
//using checkStub;
using ClickNCheck.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//using ClinkNCheck.Models;
namespace checkStub
{
    public class CheckerRunner
    {
        private readonly bool checkAcademics;
        private readonly bool checkAssociations;
        private readonly bool checkCredit;
        private readonly bool checkCriminal;
        private readonly bool checkDrivers;
        private readonly bool checkEmployment;
        private readonly bool checkIdentity;
        private readonly bool checkPersonal;
        private readonly bool checkResidency;
        private readonly JObject requestedChecks;
        private readonly int candidateID;

        //private JObject results;
        // system instance
        private ClickNCheckContext _context;

        public CheckerRunner(ClickNCheckContext context, JObject requestedChecks)
        {
            _context = context;
            
            candidateID = (int)requestedChecks["candidateID"];

            checkAcademics = (bool)requestedChecks["academic"]["required"];
            checkAssociations = (bool)requestedChecks["association"]["required"];
            checkCredit = (bool)requestedChecks["credit"]["required"]; ;
            checkCriminal = (bool)requestedChecks["criminal"]["required"];
            checkDrivers = (bool)requestedChecks["drivers"]["required"];
            checkEmployment = (bool)requestedChecks["employment"]["required"];
            checkIdentity = (bool)requestedChecks["identity"]["required"];
            checkPersonal = (bool)requestedChecks["personal"]["required"];
            checkResidency = (bool)requestedChecks["residency"]["required"];

            this.requestedChecks = requestedChecks;
            
            //results = new JObject{};
        }

        public async Task<JObject> StartChecks()
        {
            try
            {
                JObject results = new JObject();
                if (checkAcademics)
                {
                    string category = "academic";
                    //find out which ones under academic are true
                    if (!(requestedChecks["academic"]["serviceid"] is JArray selectedServiceID) || selectedServiceID.Count < 1)
                        throw new Exception("No selected services under academic!");

                    JArray academicCheckResults = null;
                    Academic academicCheck = new Academic(_context, candidateID);
                    //run the check
                    
                    academicCheckResults = await academicCheck.RunChecks(selectedServiceID);

                    //return academicCheckResults;
                    //pass the results to X's method( candidateID, category, selectedServirceID, academicCheckResults );
                    //do not store it anywhere;
                    results.Add(category, academicCheckResults);
                }
                if (checkAssociations)
                {
                    //find out which ones under academic are true
                    if (!(requestedChecks["association"]["serviceid"] is JArray selectedServiceID) || selectedServiceID.Count < 1)
                        throw new Exception("No selected services under association!");

                    Associations associationsCheck = new Associations(_context, candidateID);
                    //run the check
                    await associationsCheck.RunChecks(selectedServiceID);

                    JObject associationsCheckResults = associationsCheck.getResults();
                    results.Add("associations", associationsCheckResults);
                }
                if (checkCredit)
                {
                    //first find out which ones under credit are true
                    if (!(requestedChecks["credit"]["serviceid"] is JArray selectedServiceID) || selectedServiceID.Count < 1)
                        throw new Exception("No selected services under credit!");

                    JArray creditCheckResults = null;
                    Credit creditCheck = new Credit(_context, candidateID);
                    
                    creditCheckResults = await creditCheck.RunChecks(selectedServiceID);

                    results.Add("credit", creditCheckResults);
                }
                if (checkCriminal)
                {
                    Criminal criminalCheck = new Criminal();
                    criminalCheck.runCheck();
                    
                    JObject criminalCheckResults = criminalCheck.getResults();
                    results.Add("criminal", criminalCheckResults);
                }
                if (checkDrivers)
                {
                    Drivers driversCheck = new Drivers();
                    driversCheck.runCheck();
                    
                    JObject driversCheckResults = driversCheck.getResults();
                    results.Add("drivers", driversCheckResults);
                }
                if (checkEmployment)
                {
                    Employment employmentCheck = new Employment();
                    employmentCheck.runCheck();
                    
                    JObject employmentCheckResults = employmentCheck.getResults();
                    results.Add("employment", employmentCheckResults);
                }
                if (checkIdentity)
                {
                    //first find out which ones under academic are true
                    bool names = (bool)requestedChecks["identity"]["subChecks"]["names"];
                    bool idNumber = (bool)requestedChecks["identity"]["subChecks"]["idNumber"];
                    bool maritalStatus = (bool)requestedChecks["identity"]["subChecks"]["maritalStatus"];
                    bool deceaseStatus = (bool)requestedChecks["identity"]["subChecks"]["deceaseStatus"];

                    Identity identityCheck = new Identity(names, idNumber, maritalStatus, deceaseStatus);
                    identityCheck.runCheck();

                    JObject identityCheckResults = identityCheck.getResults();
                    results.Add("identity", identityCheckResults);
                }
                if (checkPersonal)
                {
                    Personal personalCheck = new Personal();
                    personalCheck.runCheck();

                    JObject personalCheckResults = personalCheck.getResults();
                    results.Add("personal", personalCheckResults);
                }
                if (checkResidency)
                {
                    Residency residencyCheck = new Residency();
                    residencyCheck.runCheck();

                    JObject residencyCheckResults = residencyCheck.getResults();
                    results.Add("residency", residencyCheckResults);
                }
                return results;
            }
            catch (Exception e) { return new JObject { "error: ", e.Source }; }
        }

    }
}