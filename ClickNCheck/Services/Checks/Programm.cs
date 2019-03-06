using ClickNCheck.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace checkStub
{
    class Programm
    {
        static void mMain()
        {
            JObject obj = new JObject
            {
                {
                    "credentials",
                    new JObject
                    {
                        { "name", "jabu" }, {"surname","mahlangu"}
                    }
                },

                {
                    "academic",
                    new JObject
                    {
                        { "required","true" }, {"subChecks", new JObject{ {"highSchool", "true" }, {"tatiary","true"} } }
                    }
                },

                {
                    "association",
                    new JObject
                    {
                        { "required","false" }, {"subChecks", new JObject{} }
                    }
                },

                {
                    "credit",
                    new JObject
                    {
                        { "required","true" }, {"subChecks", new JArray{ 1, 3, 4} } 
                    }
                },

                {
                    "criminal",
                    new JObject
                    {
                        { "required","false" }, {"subChecks", new JArray{ 1 } }
                    }
                },

                {
                    "drivers",
                    new JObject
                    {
                        { "required","true" }, {"subChecks", new JArray{ 1 } }
                    }
                },

                {
                    "employment",
                    new JObject
                    {
                        { "required","true" }, {"subChecks", new JArray{ 1 } }
                    }
                },

                {
                    "identity",
                    new JObject
                    {
                        { "required","true" }, {"subChecks", new JObject{ { "names", "true" }, { "idNumber", "false" },{ "maritalStatus", "true" },{ "deceaseStatus", "true" } } }
                    }
                },

                {
                    "personal",
                    new JObject
                    {
                        { "required","true" }, {"subChecks", new JObject{} }
                    }
                },

                {
                    "residency",
                    new JObject
                    {
                        { "required", "true" }, {"subChecks", new JObject{} }
                    }
                }
                
            };
            /*Candidate c = new Candidate();
       
            CheckerRunner runIt = new CheckerRunner(c, obj);
            runIt.startChecks();
            runIt.logAllResults();
            */
            //_____________________

            List<string>  sercices = new List<string>();
            sercices.Add("Account Verification Service Real-Time (AVSR)");
            sercices.Add("Account Verification Service (AVS)");
            sercices.Add("Affordability Assessment Service");
            sercices.Add("Load Default Service");
            sercices.Add("Employer Confidence Index (ECI)");
            sercices.Add("Consumer Contact Information Service");
            sercices.Add("Person Status Service");
            sercices.Add("Person Verification Service (PVS)");
            sercices.Add("PurQ Service - Qualification & Enrolment Verification");
            sercices.Add("KYC Service");
            sercices.Add("GetScore Service");
            sercices.Add("CheckScore Service");
            sercices.Add("Contact Verification Service");

            List<int> cats = new List<int> { 1, 3 };
            //Vendor compuscan = new Vendor(434.68, 1, "compuscan", cats, sercices, prices);
        }
    }
}
