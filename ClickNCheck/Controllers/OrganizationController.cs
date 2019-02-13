using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using ClickNCheck;
using ClickNCheck.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClickNCheck.Data;

namespace ClickNCheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {// GET api/values

        private ClickNCheckContext _context;
        private LinkCode _model;
        EmailService mailS = new EmailService();

        public OrganizationController(ClickNCheckContext context)
        {

            _context = context;

        }

        [HttpPost]
        [Route("createOrganisation")] // create organization
        public async Task<ActionResult<Organisation>> PostOrganisation([FromBody]Organisation organisation)
        {
            _context.Organisation.Add(organisation);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("createJP")] // creatin a job profile
        public async Task<ActionResult<JobProfile>> PostJP([FromBody] JobProfile jobProfile)
        {
            _context.JobProfile.Add(jobProfile);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{id}")]
        [Route("searchRecruiters")]


        [HttpGet()]
        [Route("sendMail")] //check if you need this routes
        public ActionResult sendMail()
        {
            _model = new LinkCode();
            string emailBody = System.IO.File.ReadAllText("C:\\Users\\Mpinane Mohale\\Desktop\\Standard Bank\\Mine\\RegistrationEmail\\index.html"); ;
            //try removing these 2 variables
            /*  var list = Recruiters.ReadDetails("C:\\testData.csv");
               var email = Recruiters.getEmails(list);
               foreach (var item in email) {
                   string code = mailS.generateCode();
                   _model.Code = code;
                   _model.Used = false;
                   _context.LinkCodes.Add(_model);

                   mailS.SendMail(item, "testing", $"C:\\Users\\Mpinane Mohale\\Desktop\\Standard Bank\\Mine\\RegistrationEmail\\index.html");
               }*/
            string code = mailS.generateCode();
            _model.Code = code;
            _model.Valid = false;
            _context.LinkCodes.Add(_model);
            mailS.SendMail("mpinanemohale@gmail.com","nane", emailBody, code);
            _context.SaveChanges();
            // return Ok(email);
            return Ok();
        }
       

        [HttpGet()]
        [Route("signup/{code}")] //recruiter reg page 
        public ActionResult<string> getCode(string code)
        {
        
            var entry = _context.LinkCodes.FirstOrDefault(x => x.Code == code);
            string please = entry.Code;
            bool valid = entry.Valid;

            if (valid == true)
            {


                return "reguster grand";
                //entry.Valid = false;
            }
            else {

                return "link expired, request new one";
            }


        }
      
    }
}
 