using System;

using Newtonsoft.Json.Linq;

namespace checkStubClasses
{
    public class Criminal
    {
        // members of the criminal class to be checked
        private bool criminalCheck;

        //
        private bool inProgress;

        //
        private bool checkRan;

        //
        private JObject results;

        public Criminal()
        {
            this.criminalCheck = true;
            this.inProgress = false;
            this.checkRan = false;

            this.results = new JObject();
        }

        public void runCheck()
        {
            try
            {
                inProgress = true;
                if (criminalCheck)
                    runCriminalCheck();
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
        private void runCriminalCheck()
        {
            // if not provided by 1 api, then instantiate connection;
            // otherwise use the existing connection made by the constructor
            if (true/*connection to API succeeded*/)
            {
                //then get results
                //wait results.criminalCheck = connection.getVerification();
                //if done set inProgress = false && checkRan = true;
                results.Add("crime","Offence, Assult charged 2001");
                if(results.Count > 0)
                {
                    inProgress = false;
                    checkRan = true;
                }

            }
            else throw new Exception();
        }
    }
}