using ClickNCheck.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Services
{
    public class verficationCandidateLogic
    {
        //assuming i know the queue table values
        public void executeCheck()
        {
            ChecksController compCheck = new ChecksController();
             compCheck.Umalusi();
        }
        
        public void checkResult()
        {


        }
        

    }
}
