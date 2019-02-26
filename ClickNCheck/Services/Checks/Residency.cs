using System;

using Newtonsoft.Json.Linq;

namespace checkStub
{
    public class Residency
    {
        // possible checks under residency
        private bool physicalAddress;

        // turns true when then check is completed
        private bool checkRan;

        // truns true when a residency check is in progress
        private bool inProgress;

        // results from the apis
        private JObject results;

        // if all offered by 1 api, then use only one connection
        // instantiate it in the constructor

        public Residency()
        {
            this.physicalAddress = true;

            // tracking variables
            this.checkRan = false;
            this.inProgress = false;

            this.results = new JObject();
        }

        public void runCheck()
        {
            try
            {
                inProgress = true;

                if (physicalAddress)
                    runPhysicalAddressCheck();

                // you might wanna have a promise handler for results
            }
            catch { /*connection problems*/ }
        }

        public JObject getResults()
        {
            if (!inProgress && checkRan)
            {
                return results;
            }
            else throw new Exception("Check either still in progress or never ran!");
        }

        //make this an asychronous method
        private void runPhysicalAddressCheck()
        {
            // if not provided by 1 api, then instantiate connection;
            // otherwise use the existing connection made by the constructor
            if (true/*connection to API succeeded*/)
            {
                //then get results
                //wait results.physicallAddress = connection.getVerification();
                //turn ranCheck to true after
                results.Add("physicalAddress","42 Park Street Pretoria 0001");
                if (results.Count > 0)
                {
                    inProgress = false;
                    checkRan = true;
                }
            }
            else throw new Exception();
        }
    }
}