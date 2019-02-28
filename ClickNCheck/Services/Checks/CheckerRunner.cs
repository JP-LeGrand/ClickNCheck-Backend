using System;
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
        private bool checkAcademics;
        private bool checkAssociations;
        private bool checkCredit;
        private bool checkCriminal;
        private bool checkDrivers;
        private bool checkEmployment;
        private bool checkIdentity;
        private bool checkPersonal;
        private bool checkResidency;
        private JObject requestedChecks;
        private JObject candidateCridentials;

        private JObject results;
        private Candidate candidate;
        // database instance
        private ClickNCheckContext _context;

        public CheckerRunner(ClickNCheckContext context, JObject requestedChecks)
        {
            //expected input (json) requestedChecks = {'credentials':{'name':jabu}, {'surname':'mahlangu'} ...},'academic': {'required': true, 'params': {'highSchool': true, 'tatiary': true }, 'associations'... }
            _context = context;

            int id = (int) requestedChecks["candiateID"];
            this.candidate = _context.Candidate.Find(id);
            
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
            
            results = new JObject { };
        }

        public async Task<Object> startChecks()
        {
            if (checkAcademics)
            {
                //find out which ones under academic are true
                bool highSchool = (bool)requestedChecks["academic"]["subChecks"]["highSchool"];
                bool tatiary= (bool)requestedChecks["academic"]["subChecks"]["tatiary"];
                
                Academic academicCheck = new Academic(highSchool, tatiary);
                //run the check
                academicCheck.runCheck();
                //you might have to wait for some days for the results
                //request the results in JSON format
                JObject academicCheckResults = academicCheck.getResults();

                //send results back indepedently soon as they are available
                //TODO
                results.Add("academic", academicCheckResults);

            }
            if (checkAssociations)
            {
                Associations associationsCheck = new Associations();
                associationsCheck.runChecks();

                //you might have to wait for some days for the results
                //request the results in JSON format
                JObject associationsCheckResults = associationsCheck.getResults();

                //send results back indepedently soon as they are available
                //TODO
                results.Add(associationsCheckResults);
            }
            if (checkCredit)
            {
                //first find out which ones under credit are true
                
                var selectedCreditCheck = requestedChecks["credit"]["subChecks"] as JArray;
                if (selectedCreditCheck == null) return null;

                List<int> selectedCreditCheckIDs = new List<int>();
                foreach(int id in selectedCreditCheck)
                {
                    selectedCreditCheckIDs.Add(id);
                }

                Credit creditCheck = new Credit(_context, candidate);
                
                //you might have to wait for some days for the results
                //request the results in JSON format
                JObject creditCheckResults = await creditCheck.runAllSelectedCreditChecks(selectedCreditCheckIDs);

                results.Add("credit", creditCheckResults);
            }
            if (checkCriminal)
            {
                Criminal criminalCheck = new Criminal();
                criminalCheck.runCheck();

                //you might have to wait for some days for the results
                //request the results in JSON format
                JObject criminalCheckResults = criminalCheck.getResults();

                //send results back indepedently soon as they are available
                //TODO
                results.Add("criminal", criminalCheckResults);
            }
            if (checkDrivers)
            {
                Drivers driversCheck = new Drivers();
                driversCheck.runCheck();

                //you might have to wait for some days for the results
                //request the results in JSON format
                JObject driversCheckResults = driversCheck.getResults();

                //send results back indepedently soon as they are available
                //TODO
                results.Add("drivers", driversCheckResults);
                
            }
            if (checkEmployment)
            {
                Employment employmentCheck = new Employment();
                employmentCheck.runCheck();

                //you might have to wait for some days for the results
                //request the results in JSON format
                JObject employmentCheckResults = employmentCheck.getResults();

                //send results back indepedently soon as they are available
                //TODO
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

                //you might have to wait for some days for the results
                //request the results in JSON format
                JObject identityCheckResults = identityCheck.getResults();

                //send results back indepedently soon as they are available
                //TODO
                results.Add("identity",identityCheckResults);
            }
            if (checkPersonal)
            {
                Personal personalCheck = new Personal();
                personalCheck.runCheck();

                JObject personalCheckResults = personalCheck.getResults();

                //send results back indepedently soon as they are available
                //TODO
                results.Add("personal", personalCheckResults);
            }
            if (checkResidency)
            {
                //first find out which ones under residency are true
                Residency residencyCheck = new Residency();
                residencyCheck.runCheck();

                JObject residencyCheckResults = residencyCheck.getResults();

                //send results back indepedently soon as they are available
                //TODO
                results.Add("residency", residencyCheckResults);
            }
            return getResults();
        }
        
        public JObject getResults()
        {
            return results;
        }

        public void logAllResults()
        {
            foreach (var pair in results)
            {
                Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
            }

        }
        public void doWork() { }


    }
}