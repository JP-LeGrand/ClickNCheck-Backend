﻿using System;
using Newtonsoft.Json.Linq;

namespace checkStub
{
    public class Academic
    {
        // possible checks under residency
        private bool highSchool;
        private bool tatiary;
        
        // results from the apis
        private JObject results;

        // results from the apis class defination
        // or instead of a struct you can define a JSON
        // JSON with keys highSchool & tatiary
        // turns true when check is completed
        private bool checkRan;

        // turns true when then check is completed
        private bool inProgress;

        // if all offered by 1 api, then use only one connection
        // instantiate it in the constructor
        // Connection connection;

        public Academic(bool highSchool, bool tatiary)
        {
            this.highSchool = highSchool;
            this.tatiary = tatiary;

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

                if (highSchool)
                    runHighSchoolCheck();

                if (tatiary)
                    runTatiaryCheck();

                // you might wanna have a promise handler for results
                
            }
            catch(Exception) { /*connection problems*/ }
        }

        public JObject getResults()
        {
            if (!inProgress && checkRan)
            {
                return results;
            }
            else throw new Exception("Acedemic check either still in progress or never ran!");
        }

        //make this an asychronous method
        private void runHighSchoolCheck()
        {
            // if not provided by 1 api, then instantiate connection;
            // otherwise use the existing connection made by the constructor
            if (true/*connection to API succeeded*/)
            {
                /*then get results we dont wave ride, marry j blidge
                //wait results.highSchool = connection.getVerification();
                //turn ranCheck to true after, and inProgress to false*/
                results.Add( "highSchool", "NSC, 2005, Pretoria Boys High School" );
                
                if ((tatiary && results.Count == 2) || (!tatiary && results.Count == 1))
                {
                    inProgress = false;
                    checkRan = true;
                }

            }
            else throw new Exception();
        }

        //make this an asychronous method
        private void runTatiaryCheck()
        {
            // if not provided by 1 api, then instantiate connection;
            // otherwise use the existing connection made by the constructor
            if (true/*connection to API succeeded*/)
            {
                //then get results
                //wait results.tatiary = connection.getVerification();
                //turn ranCheck to true after
                results.Add("tatiary","BSc Applied Mathematics, 2009, University of Pretoria");

                if ((highSchool && results.Count == 2) || (!highSchool && results.Count == 1))
                {
                    inProgress = false;
                    checkRan = true;
                }
            }
            else throw new Exception();
        }

        
    }
}