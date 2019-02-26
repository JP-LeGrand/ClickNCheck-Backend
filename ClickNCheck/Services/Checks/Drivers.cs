using Newtonsoft.Json.Linq;
using System;


namespace checkStub
{
    public class Drivers
    {
        // members of the criminal class to be checked
        private bool driversCheck;

        //
        private bool inProgress;

        //
        private bool checkRan;

        //
        private JObject results;

        public Drivers()
        {
            this.driversCheck = true;
            this.inProgress = false;
            this.checkRan = false;

            this.results = new JObject();
        }

        public void runCheck()
        {
            try
            {
                inProgress = true;
                if (driversCheck)
                    runDriversCheck();

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
            // you might want to use promises
            else throw new Exception("Either still in progress or not ran!");
        }

        //this method should be asychronous
        private void runDriversCheck()
        {
            // if not provided by 1 api, then instantiate connection;
            // otherwise use the existing connection made by the constructor
            if (true/*connection to API succeeded*/)
            {
                //then get results
                //wait results.criminalCheck = connection.getVerification();
                //if done set inProgress = false && checkRan = true;
                results.Add("driver","Code 10, exp: Jan 2021");
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