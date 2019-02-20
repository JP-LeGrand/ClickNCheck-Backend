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
       
        EmailService mailS = new EmailService();

        public OrganizationController(ClickNCheckContext context)
        {

            _context = context;

        }

        [HttpPost]
        [Route("createOrganisation")] // create organization
        public async Task<ActionResult<Organisation>> PostOrganisation([FromBody]Organisation organisation)        {
            _context.Organisation.Add(organisation);
            var result = await _context.SaveChangesAsync();

            var user = await _context.Organisation.LastAsync();
            int id  = user.ID;
           
            return Ok(id);
        }

        [HttpPost]
        [Route("createJP")] // creatin a job profile
        public async Task<ActionResult<JobProfile>> PostJP([FromBody] JobProfile jobProfile)
        {
            _context.JobProfile.Add(jobProfile);
            await _context.SaveChangesAsync();
            return Ok();
        }

       
      
    }
}
 