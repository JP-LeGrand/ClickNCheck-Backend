using System;
using Newtonsoft.Json.Linq;

namespace checkStub
{
    public class Associations
    {
        private bool assoc;
        private JObject results;
        private bool checkRan;
        private bool inProgress;

        public Associations()
        {
            assoc = true;
            results = new JObject();
            checkRan = false;
            inProgress = false;
        }
        public void runChecks()
        {
            try
            {
                inProgress = true;
                if (assoc)
                    runAssociationsChecks();  
            }
            catch { /*"Associations check failed to run."*/};
        }

        public void runAssociationsChecks()
        {
            if (true/*connection succeeded*/)
            {
                results.Add("registed","ACE, SASSA, ACFE");
                if(results.Count > 0)
                {
                    checkRan = true;
                    inProgress = false;
                }
            }
            else throw new Exception("Associations check failed to run.");
        }

        public JObject getResults()
        {
            if (!inProgress && checkRan )
            {
                return results;
            }
            throw new Exception("Associations check either still in progress or have not started!");
        }
    }
}