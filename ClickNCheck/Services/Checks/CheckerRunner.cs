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

        private JObject results;
        private readonly int candidateID;
        // database instance
        private ClickNCheckContext _context;

        public CheckerRunner(ClickNCheckContext context, JObject requestedChecks)
        {
            _context = context;

            this.candidateID = (int)requestedChecks["candiateID"];

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

        public async void StartChecks()
        {
            if (checkAcademics)
            {
                JArray selectedAcademicCheckIDs = requestedChecks["academic"]["subChecks"] as JArray;
                if (selectedAcademicCheckIDs == null) throw new Exception("Academic it selected but selectedAcademicCheckIDs are EMPTY!");

                Academic academicCheck = new Academic(_context, candidateID);

                try
                {
                    JObject academicCheckResults = await academicCheck.runAllSelectedAcademicChecks(selectedAcademicCheckIDs);
                    results.Add("academic", academicCheckResults);
                }
                catch (Exception) { }


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
                results.Add("associations", associationsCheckResults);
            }
            if (checkCredit)
            {
                //first find out which ones under credit are true
                
                JArray selectedCreditCheck = requestedChecks["credit"]["subChecks"] as JArray;
                if (selectedCreditCheck == null) throw new Exception("Credit it selected but selectedCreditCheck is EMPTY!");
                
                Credit creditCheck = new Credit(_context, candidateID);

                //you might have to wait for some days for the results
                //request the results in JSON format
                try
                {
                    JObject creditCheckResults = await creditCheck.runAllSelectedCreditChecks(selectedCreditCheck);

                    results.Add("credit", creditCheckResults);
                }
                catch (Exception) { }
                
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
        }
        
        public JObject getResults()
        {
            return this.results;
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