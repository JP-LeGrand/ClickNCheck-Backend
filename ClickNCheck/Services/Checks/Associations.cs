using System;
using System.Threading.Tasks;
using ClickNCheck.Data;
using Newtonsoft.Json.Linq;

namespace checkStub
{
    public class Associations
    {
        private bool assoc;
        private JObject results;
        private bool checkRan;
        private bool inProgress;

        public Associations(ClickNCheckContext context, int candidateId)
        {
            assoc = true;
            results = new JObject();
            checkRan = false;
            inProgress = false;
        }
        public async Task<bool> RunChecks(JArray selectedServiceID)
        {
            try
            {
                inProgress = true;
                if (assoc)
                    runAssociationsChecks();
                return true;

            }
            catch { return true; };
        }

        private void runAssociationsChecks()
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