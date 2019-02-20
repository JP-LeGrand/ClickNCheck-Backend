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
using System.IO;
using System.Reflection;
using ClickNCheck.Services;

namespace ClickNCheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ClickNCheckContext _context;
        public UsersController(ClickNCheckContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.User.ToListAsync();
        }

        [HttpPost]
        [Route("PostRecruiters/recruiters")] // create organization
        public ActionResult<User> PostRecruiters(User [] recruiters)
        {
           
            for (int x = 0; x < recruiters.Length; x++)
            {
                CodeGenerator _codeGenerator = new CodeGenerator();
                EmailService _emailService = new EmailService();
                LinkCode _linkCode = new LinkCode();
                User _users = new User();
                string code = _codeGenerator.generateCode();
                _linkCode.Code = code;
                _linkCode.Used = false;
                recruiters[x].LinkCode = _linkCode;
        
                string emailBody = System.IO.File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Files\SignUpEmail.html"));
                emailBody = emailBody.Replace("href=\"#\" ", "href=\"https://localhost:44347/api/Users/signup/" + code + "\"");

               _emailService.SendMail(recruiters[x].Email, "Recruiter Signup", emailBody);



            }
            _context.User.AddRange(recruiters);
            _context.SaveChanges();
            
            return Ok("success");
        }

      

        [HttpGet]
        [Route("signup/{code}")]
        public ActionResult<string> checkCode(string code)
        {
            LinkCode _userCode = _context.LinkCodes.FirstOrDefault(x => x.Code == code);

            if (_userCode != null)
            {
         
              return Redirect("https://s3.amazonaws.com/clickncheck-frontend-tafara/Click-N-Check-Frontend/src/html/admin/admin_registration.html?code=" + code);

            }
            else
            {
                return Ok(code);
            }
  
        }

        [HttpPost]
        [Route("registration")]
        public ActionResult<string> registerUser([FromBody] string [] Password)
        {
            //  var code = Response.

            var pass = Password[0];
            var code = Password[1];

            var codeEntry = _context.LinkCodes.FirstOrDefault(x => x.Code == code);
            var codeID = codeEntry.ID;

            var userEntry = _context.User.FirstOrDefault(d => d.LinkCodeID == codeID);

            userEntry.Password = pass;
            _context.User.Update(userEntry);

            return Ok();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var User = await _context.User.FindAsync(id);

            if (User == null)
            {
                return NotFound();
            }

            return User;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User User)
        {
            if (id != User.ID)
            {
                return BadRequest();
            }

            _context.Entry(User).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [HttpPost]
        [Route("CreateUser")]
        public async Task<ActionResult<User>> PostUser(User User)
        {
            _context.User.Add(User);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = User.ID }, User);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var User = await _context.User.FindAsync(id);
            if (User == null)
            {
                return NotFound();
            }

            _context.User.Remove(User);
            await _context.SaveChangesAsync();

            return User;
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.ID == id);
        }

        private bool JobProfileExists(int id)
        {
            return _context.JobProfile.Any(e => e.ID == id);
        }

        [HttpPost()]
        [Route("{id}/AssignRecruiters")]
        public async Task<IActionResult> AssignRecruiters(int id, [FromBody]int[] ids)
        {
            int jobId = id;

            //find job profile
            var jobProfile = await _context.JobProfile.FindAsync(jobId);

            if (jobProfile == null)
            {
                return NotFound("This Job Profile does not exist");
            }
            EmailService emailService = new EmailService();
            string emailBody = emailService.RecruiterMail();
            
            //find recruiters
            for (int i = 0; i < ids.Length; i++)
            {
                var recruiter = await _context.User.FindAsync(ids[i]);

                if (recruiter == null)
                {
                    return NotFound("The recruiter "+recruiter.Name+recruiter.Surname + " does not exist");
                }
               //add recruiter to job ptofile
                jobProfile.Recruiter_JobProfile.Add(new Recruiter_JobProfile { JobProfile = jobProfile, Recruiter = recruiter });
                emailService.SendMail(recruiter.Email, "New Job Profile", emailBody);
                //emailService.SendMail(recruiter.Email,"New Job Profile Assignment",);
            }
            //save changes to job profile
            _context.Entry(jobProfile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = _context.User.Any(e => e.ID == jobId);
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

        // GET: api/Recruiters/GetAllRecruiters/5
        [HttpGet("GetAllRecruiters/{id}")]
        public IEnumerable<User> GetAllRecruiters(int id)
        {
            var recruiters = _context.User.FromSql($"SELECT * FROM User WHERE ID IN( SELECT UserID FROM Roles WHERE UserTypeID = 3) AND OrganisationID = {id}").ToList();
            return recruiters;
        }

        [HttpGet]
        [Route("userTypes")]
        public async Task<IEnumerable<UserType>> userTypesAsync()
        {
            var  userTypes = await _context.UserType.ToListAsync();
            return userTypes;
        }
    }

    
}
