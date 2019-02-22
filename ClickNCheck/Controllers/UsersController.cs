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

using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Collections;

namespace ClickNCheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private ClickNCheckContext _context;
        EmailService mailS = new EmailService();
        LinkCode _model = new LinkCode();
        User _userModel = new User();
        Roles _role = new Roles();

        public UsersController(ClickNCheckContext context)
        {
            _context = context;
        }

        [HttpPost()]
        [Route("signUp")]
        public ActionResult<User> regAdmin(User[] administrators)
        {
            _context.User.AddRange(administrators);
            _context.SaveChanges();

            return Ok("yes");
        }


        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.User.ToListAsync();
        }

        [HttpPost]
        [Route("PostUsers/{id}")]
        public ActionResult<User> PostUsers(User[] users, int id)
        {

            var _entryType = _context.UserType.FirstOrDefault(x => x.ID == id);
            

            for (int x = 0; x < users.Length; x++)
            {
                CodeGenerator _codeGenerator = new CodeGenerator();
                EmailService _emailService = new EmailService();
                LinkCode _linkCode = new LinkCode();
                string code = _codeGenerator.generateCode();
                _linkCode.Code = code;
                _linkCode.Used = false;
                users[x].LinkCode = _linkCode;

                users[x].Roles.Add(new Roles { User = users[x], UserType = _entryType });
                if (_entryType.Type == "Recruiter")
                {
                    string emailBody = System.IO.File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Files\SignUpEmail.html"));
                    emailBody = emailBody.Replace("href=\"#\" ", "href=\"https://localhost:44347/api/Users/signup/" + code + "\"");

                    _emailService.SendMail(users[x].Email, "Recruiter Signup", emailBody);
                }
                else if (_entryType.Type == "Manager")

                {
                    string emailBody = System.IO.File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Files\SignUpEmail.html"));
                    emailBody = emailBody.Replace("href=\"#\" ", "href=\"https://localhost:44347/api/Users/signup/" + code + "\"");

                    _emailService.SendMail(users[x].Email, "Manager Signup", emailBody);

                }



            }
            _context.User.AddRange(users);
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
                User user = _context.User.FirstOrDefault(u => u.LinkCodeID == _userCode.ID);
                var recruiters = _context.Roles.Where(x => x.UserTypeId == 3).Select(x => x.UserId).ToList();
                // var Users.Where(x => x.orgid ={ id} && roles.contains(x.roleid))
                var admins = _context.Roles.Where(x => x.UserTypeId == 1).Select(x => x.UserId).ToList();
                // var admins = _context.User.FromSql($"SELECT * FROM User WHERE ID IN( SELECT UserID FROM Roles WHERE UserTypeID = 1)").ToList();

                if (recruiters.Contains(user.ID))
                {
                    return Redirect("https://s3.amazonaws.com/clickncheck-frontend-tafara/components/recruiter/recruiterRegistration/recruter_registration.html?code=" + user.ID);
                }
                else if (admins.Contains(user.ID))
                {
                    return Redirect("https://s3.amazonaws.com/clickncheck-frontend-tafara/components/admin/adminRegistration/admin_registration.html");
                }

                return Unauthorized(code);

            }
            else
            {
                return Unauthorized(code);
            }

        }

        [HttpPost]
        [Route("registration")]
        public ActionResult<string> registerUser([FromBody] string[] Password)
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


        [HttpPost]
        [Route("register")]
        public ActionResult<string> registerRecruiter([FromBody] string[] id_pass_manager)
        {
            //  var code = Response.

            int recruiter_id = Convert.ToInt32(id_pass_manager[0]);
            string pass = id_pass_manager[1];
            int manager_id = -1;

            if (id_pass_manager.Length > 2)
            {
                manager_id = Convert.ToInt32(id_pass_manager[2]);
            }
            

           // var codeEntry = _context.LinkCodes.FirstOrDefault(x => x.Code == code);
            //var codeID = codeEntry.ID;

            User user = _context.User.FirstOrDefault(d => d.ID == recruiter_id);

            
            
            var recruiters = _context.Roles.Where(x => x.UserTypeId == 3).Select(x => x.UserId).ToList();
            // var Users.Where(x => x.orgid ={ id} && roles.contains(x.roleid))
            var admins = _context.Roles.Where(x => x.UserTypeId == 1).Select(x => x.UserId).ToList();
            // var admins = _context.User.FromSql($"SELECT * FROM User WHERE ID IN( SELECT UserID FROM Roles WHERE UserTypeID = 1)").ToList();

            if (recruiters.Contains(user.ID))
            {
                user.Password = pass;
                user.ManagerID = manager_id;
                _context.User.Update(user);
                _context.SaveChangesAsync();
                return Ok("recruiter");
            }
            else if (admins.Contains(user.ID))
            {
                user.Password = pass;
                _context.User.Update(user);
                _context.SaveChangesAsync();
                return Ok("admin");
            }

            return BadRequest();
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

        // POST: api/Users/CreateUser
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
        // POST: api/Users/5/AssignRecruiters
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
               //add recruiter to job profile
                jobProfile.Recruiter_JobProfile.Add(new Recruiter_JobProfile { JobProfile = jobProfile, Recruiter = recruiter });
                emailService.SendMail(recruiter.Email, "New Job Profile", emailBody);
                //emailService.SendMail(recruiter.Email,"New Job Profile Assignment",);
                //ghost
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


        // GET: api/Users/GetAllRecruiters/5
        [HttpGet("recruiter/organisation/managers/{recruiter_id}")]
        public IEnumerable GetRecruiterOrganisationManagers(int recruiter_id)
        {
            var all_managers = _context.Roles.Where(x => x.UserTypeId == 4).Select(x => x.UserId);
            var organisation_id = _context.User.Find(recruiter_id).OrganisationID;

            var org_users = _context.User.Where(x => x.OrganisationID == organisation_id).ToList();

            List<User> managers = new List<User>();

            for(int i = 0; i < org_users.Count; i++)
            {
                if(all_managers.Contains(org_users.ElementAt(i).ID))
                {
                    managers.Add(org_users.ElementAt(i));
                }
            }
           // var managers = org_users.Intersect(all_managers).ToList();

            return managers;
        }

        // GET: api/Users/GetAllRecruiters/5
        [HttpGet("Organization/{id}/recruiters")]
        public IEnumerable<User> GetAllRecruiters(int id)
        {
            var roles = _context.Roles.Where(x => x.UserTypeId == 3).Select(x => x.User).ToList();
            //var recruiters = _contextUsers.Where(x => x.orgid ={ id} && roles.contains(x.roleid))
            var recruiters = _context.User.FromSql($"SELECT * FROM User WHERE ID IN( SELECT UserID FROM Roles WHERE UserTypeID = 3) AND OrganisationID = {id}").ToList();
            return recruiters;
        }

        [HttpGet]
        [Route("userTypes")]
        public async Task<IEnumerable<UserType>> userTypesAsync()
        {
            var  userTypes = await _context.UserType.Where(u => u.Type != "Administrator" && u.Type != "SuperAdmin").ToListAsync();
            return userTypes;
        }
    }

    
}
