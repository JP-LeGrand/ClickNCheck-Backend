using System;

using Newtonsoft.Json.Linq;

namespace checkStub
{
    public class Identity
    {
        // members of the criminal class to be checked
        private bool names;
        private bool idNumber;
        private bool maritalStatus;
        private bool deceaseStatus;

        //
        private bool inProgress;

        //
        private bool checkRan;

        //
        private JObject results;
        

        public Identity(bool names, bool idNumber, bool maritalStatus, bool deceaseStatus)
        {
            this.names = names;
            this.idNumber = idNumber;
            this.maritalStatus = maritalStatus;
            this.deceaseStatus = deceaseStatus;

            // flags. probably wont need them if we async
            this.inProgress = false;
            this.checkRan = false;

            this.results = new JObject();
        }

        public void runCheck()
        {
            try
            {
                inProgress = true;

                if (names)
                    runNamesCheck();
                if (idNumber)
                    runIdNumberCheck();
                if (maritalStatus)
                    runMaritalStatusCheck();
                if (deceaseStatus)
                    runDeceaseStatusCheck();

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
        private void runNamesCheck()
        {
            // if not provided by 1 api, then instantiate connection;
            // otherwise use the existing connection made by the constructor
            if (true/*connection to API succeeded*/)
            {
                //then get results
                //wait results.names = connection.getVerification();
                //if done set inProgress = false && checkRan = true;
                results.Add("names", "Njabulo Peter Zuma");

                if ((names && maritalStatus && deceaseStatus && results.Count == 4) ||
                    (!names && maritalStatus && deceaseStatus && results.Count == 3) ||
                    (names && !maritalStatus && deceaseStatus && results.Count == 3) ||
                    (names && maritalStatus && !deceaseStatus && results.Count == 3) ||
                    (!names && !maritalStatus && deceaseStatus && results.Count == 2) ||
                    (names && !maritalStatus && !deceaseStatus && results.Count == 2) ||
                    (!names && maritalStatus && !deceaseStatus && results.Count == 2) ||
                    (!names && !maritalStatus && !deceaseStatus && results.Count == 1))
                {
                    inProgress = false;
                    checkRan = true;
                }

            }
            else throw new Exception();
        }

        //this method should be asychronous
        private void runIdNumberCheck()
        {
            // if not provided by 1 api, then instantiate connection;
            // otherwise use the existing connection made by the constructor
            if (true/*connection to API succeeded*/)
            {
                //then get results
                //wait results.idNumber = connection.getVerification();
                //if done set inProgress = false && checkRan = true;
                results.Add("id","880201256321021504");

                if ((names && maritalStatus && deceaseStatus && results.Count == 4) ||
                    (!names && maritalStatus && deceaseStatus && results.Count == 3) ||
                    (names && !maritalStatus && deceaseStatus && results.Count == 3) ||
                    (names && maritalStatus && !deceaseStatus && results.Count == 3) ||
                    (!names && !maritalStatus && deceaseStatus && results.Count == 2) ||
                    (names && !maritalStatus && !deceaseStatus && results.Count == 2) ||
                    (!names && maritalStatus && !deceaseStatus && results.Count == 2) ||
                    (!names && !maritalStatus && !deceaseStatus && results.Count == 1))
                {
                    inProgress = false;
                    checkRan = true;
                }

            }
            else throw new Exception();
        }

        //this method should be asychronous
        private void runMaritalStatusCheck()
        {
            // if not provided by 1 api, then instantiate connection;
            // otherwise use the existing connection made by the constructor
            if (true/*connection to API succeeded*/)
            {
                //then get results
                //wait results.maritalStatus = connection.getVerification();
                //if done set inProgress = false && checkRan = true;
                results.Add("maritalStatus","Single");

                if ((names && idNumber && deceaseStatus && results.Count == 4) ||
                    (!names && idNumber && deceaseStatus && results.Count == 3) ||
                    (names && !idNumber && deceaseStatus && results.Count == 3) ||
                    (names && idNumber && !deceaseStatus && results.Count == 3) ||
                    (!names && !idNumber && deceaseStatus && results.Count == 2) ||
                    (names && !idNumber && !deceaseStatus && results.Count == 2) ||
                    (!names && idNumber && !deceaseStatus && results.Count == 2) ||
                    (!names && !idNumber && !deceaseStatus && results.Count == 1))
                {
                    inProgress = false;
                    checkRan = true;
                }

            }
            else throw new Exception();
        }

        //this method should be asychronous
        private void runDeceaseStatusCheck()
        {
            // if not provided by 1 api, then instantiate connection;
            // otherwise use the existing connection made by the constructor
            if (true/*connection to API succeeded*/)
            {
                /*then get results we dont wave ride, marry j blidge
                //wait results.highSchool = connection.getVerification();
                //turn ranCheck to true after, and inProgress to false*/
                results.Add("deceaseStatus","Alive");

                if ((names && idNumber && maritalStatus && results.Count == 4) || 
                    (!names && idNumber && maritalStatus && results.Count == 3)||
                    (names && !idNumber && maritalStatus && results.Count == 3)||
                    (names && idNumber && !maritalStatus && results.Count == 3)||
                    (!names && !idNumber && maritalStatus && results.Count == 2)||
                    (names && !idNumber && !maritalStatus && results.Count == 2)||
                    (!names && idNumber && !maritalStatus && results.Count == 2)||
                    (!names && !idNumber && !maritalStatus && results.Count == 1))
                {
                    inProgress = false;
                    checkRan = true;
                }

            }
            else throw new Exception();
        }
    }
}
