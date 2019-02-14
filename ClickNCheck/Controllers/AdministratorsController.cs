using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClickNCheck.Data;
using ClickNCheck.Models;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Hosting.Server;

namespace ClickNCheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministratorsController : ControllerBase
    {
        private readonly ClickNCheckContext _context;

        public AdministratorsController(ClickNCheckContext context)
        {
            _context = context;
        }

        // GET: api/Administrators
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Administrator>>> GetAdministrator()
        {
            return await _context.Administrator.ToListAsync();
        }

        // GET: api/Administrators/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Administrator>> GetAdministrator(int id)
        {
            var administrator = await _context.Administrator.FindAsync(id);

            if (administrator == null)
            {
                return NotFound();
            }

            return administrator;
        }

        // PUT: api/Administrators/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdministrator(int id, Administrator administrator)
        {
            if (id != administrator.ID)
            {
                return BadRequest();
            }

            _context.Entry(administrator).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdministratorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Administrators
        [HttpPost]
        public async Task<ActionResult<Administrator>> PostAdministrator(Administrator administrator)
        {
            _context.Administrator.Add(administrator);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdministrator", new { id = administrator.ID }, administrator);
        }

        // DELETE: api/Administrators/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Administrator>> DeleteAdministrator(int id)
        {
            var administrator = await _context.Administrator.FindAsync(id);
            if (administrator == null)
            {
                return NotFound();
            }

            _context.Administrator.Remove(administrator);
            await _context.SaveChangesAsync();

            return administrator;
        }

        private bool AdministratorExists(int id)
        {
            return _context.Administrator.Any(e => e.ID == id);
        }

        private bool JobProfileExists(int id)
        {
            return _context.JobProfile.Any(e => e.ID == id);
        }

        [HttpGet()]
        [Route("AddRecruiterToJob")]
        public async Task<IActionResult> AddRecruiterToJob([FromBody]int[] ids)
        {
            int jobId = ids[0];

            //find job profile
            var jobProfile = await _context.JobProfile.FindAsync(jobId);

            if (jobProfile == null)
            {
                return NotFound("This Job Profile does not exist");
            }

            //find recruiters
            for (int i = 1; i < ids.Length; i++)
            {
                var recruiter = await _context.Recruiter.FindAsync(ids[i]);

                if (recruiter == null)
                {
                    return NotFound("The recruiter "+recruiter.Name+recruiter.Surname + " does not exist");
                }
               //add recruiter to job ptofile
                jobProfile.Recruiter_JobProfile.Add(new Recruiter_JobProfile { JobProfile = jobProfile, Recruiter = recruiter });
                  
            }
            //save changes to job profile
            _context.Entry(jobProfile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = _context.Administrator.Any(e => e.ID == jobId);
                if (!JobProfileExists(jobId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            
            return Ok(jobProfile);
        }

        /*
         * Generating and sending links
         * 
         */

        [HttpPost()]
        [Route("sendMail")] //check if you need this routes
        public ActionResult sendMail(string person, string email)
        {
            string code = generateCode();
            LinkCode _model = new LinkCode();
            string emailBody = System.IO.File.ReadAllText("C:\\Users\\Mpinane Mohale\\Desktop\\Standard Bank\\Mine\\RegistrationEmail\\index.html");

            emailBody = emailBody.Replace("href=\"#\" ", "href=\"https://localhost:44347/api/" + person + "/signup/" + code + "\"");
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

            _model.Code = code;
            _model.Valid = true;
            _context.LinkCodes.Add(_model);

            EmailService mailS = new EmailService();
            mailS.SendMail(email, "nane", emailBody);
            _context.SaveChanges();
            // return Ok(email);
            return Ok();
        }

        public string generateCode()
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random rand = new Random();

            string code = new string(Enumerable.Repeat(characters, 10).Select(s => s[rand.Next(s.Length)]).ToArray());

            while (_context.LinkCodes.FirstOrDefault(c => c.Code == code) != null)
            {
                code = new string(Enumerable.Repeat(characters, 10).Select(s => s[rand.Next(s.Length)]).ToArray());

            }

            return code;
        }

        [Route("signup/{code}")]
        public ActionResult<string> checkCode(string code)
        {
            LinkCode our_code = _context.LinkCodes.FirstOrDefault(x => x.Code == code);

            if (our_code != null && our_code.Valid == true)
            {
                return "Yey, You can register";
            }
            else
            {
                return "Link Error: This link has either been used or is invalid";
            }
        }
    }

    
}
